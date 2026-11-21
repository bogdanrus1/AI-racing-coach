use std::thread;
use std::time::{Duration, Instant};
use std::path::PathBuf;
use std::process::{Child, Command, Stdio};
use std::sync::Mutex;

use tauri::{Manager, RunEvent};

struct BackendProcess(Mutex<Option<Child>>);

fn wait_for_backend(child: &mut Child) -> Result<(), String> {
    let client = reqwest::blocking::Client::new();

    let health_url = "http://127.0.0.1:5284/health";
    let timeout = Duration::from_secs(30);
    let start = Instant::now();

    while start.elapsed() < timeout {
        // Check whether the backend process is still running.
        match child.try_wait() {
            Ok(Some(status)) => {
                return Err(format!(
                    "Backend process exited unexpectedly: {}",
                    status
                ));
            }
            Ok(None) => {}
            Err(error) => {
                return Err(format!("Failed to check backend process: {}", error));
            }
        }

        // Check whether the backend is responding.
        if let Ok(response) = client.get(health_url).send() {
            if response.status().is_success() {
                println!("AI Racing Coach backend is ready.");
                return Ok(());
            }
        }

        thread::sleep(Duration::from_millis(500));
    }

    Err("Backend failed to become ready within 30 seconds.".to_string())
}

#[tauri::command]
fn greet(name: &str) -> String {
    format!("Hello, {}! You've been greeted from Rust!", name)
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    let app = tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![greet])
        .setup(|app| {
            // Resolve the backend project directory relative to src-tauri.
            let backend_dir = PathBuf::from(env!("CARGO_MANIFEST_DIR"))
                .join("../../backend/RacingCoach.API");

            let backend_project = backend_dir.join("RacingCoach.API.csproj");

            // Start the ASP.NET Core backend.
            let mut child = Command::new("dotnet")
                .args(["run", "--no-launch-profile", "--project"])
                .arg(backend_project)
                .args(["--urls", "http://127.0.0.1:5284"])
                .current_dir(&backend_dir)
                .stdout(Stdio::inherit())
                .stderr(Stdio::inherit())
                .spawn()?;

            println!("AI Racing Coach backend started.");

            if let Err(error) = wait_for_backend(&mut child) {
                let _ = child.kill();
                let _ = child.wait();

                return Err(std::io::Error::other(error).into());
            }

            // Keep the process handle so it can be stopped on application exit.
            app.manage(BackendProcess(Mutex::new(Some(child))));

            Ok(())
        })
        .build(tauri::generate_context!())
        .expect("error while building Tauri application");

    app.run(|app_handle, event| {
        if let RunEvent::Exit = event {
            let state = app_handle.state::<BackendProcess>();

            if let Ok(mut process) = state.0.lock() {
                if let Some(child) = process.take() {
                    let pid = child.id().to_string();

                    #[cfg(target_os = "windows")]
                    {
                        let _ = Command::new("taskkill")
                            .args(["/PID", &pid, "/T", "/F"])
                            .stdout(Stdio::null())
                            .stderr(Stdio::null())
                            .status();
                    }

                    #[cfg(not(target_os = "windows"))]
                    {
                        let mut child = child;
                        let _ = child.kill();
                        let _ = child.wait();
                    }

                    println!("AI Racing Coach backend stopped.");
                }
            }
        }
    });
}
