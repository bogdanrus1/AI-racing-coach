
<script lang="ts">
  import { onMount } from "svelte";
  import { getHealth } from "$lib/api/api";
  import { TelemetryConnection } from "$lib/signalr/telemetryConnection.svelte";

  // HTTP API state
  let apiStatus = $state("Checking...");
  let apiHealthy = $state(false);

  // SignalR service
  const telemetryConnection = new TelemetryConnection();

  onMount(() => {
    async function checkBackend() {
      try {
        const health = await getHealth();

        apiStatus = `${health.status} - ${health.application}`;
        apiHealthy = health.status === "Healthy";
      } catch (error) {
        apiStatus = "Unavailable";
        apiHealthy = false;

        console.error("Backend health check failed:", error);
      }
    }

    void checkBackend();
    void telemetryConnection.start();

    return () => {
      void telemetryConnection.stop();
    };
  });
</script>

<svelte:head>
  <title>AI Racing Coach</title>
</svelte:head>

<main class="flex min-h-screen flex-col items-center justify-center gap-5 bg-[#101216] px-6 py-10 text-[#f3f4f6] font-sans">

  <h1 class="mb-4 text-3xl font-bold tracking-tight">
    AI Racing Coach
  </h1>

  <!-- HTTP API -->
  <section class="w-full max-w-[368px] rounded-xl border border-[#303641] bg-[#1e222a] p-6 shadow-lg">
    <h2 class="mb-5 text-lg font-semibold">
      HTTP API
    </h2>

    <div class="flex items-center gap-3">
      <span
        class={`h-3 w-3 shrink-0 rounded-full ${
          apiHealthy ? "bg-green-500" : apiStatus === "Checking..." ? "bg-amber-400 animate-pulse" : "bg-red-500"
        }`}
      ></span>

      <span class="text-sm text-[#d1d5db]">
        {apiStatus}
      </span>
    </div>
  </section>

  <!-- SignalR -->
  <section class="w-full max-w-[368px] rounded-xl border border-[#303641] bg-[#1e222a] p-6 shadow-lg">
    <h2 class="mb-5 text-lg font-semibold">
      SignalR Connection
    </h2>

    <div class="flex items-center gap-3">
      <span
        class={`h-3 w-3 shrink-0 rounded-full ${
          telemetryConnection.isConnected
            ? "bg-green-500"
            : telemetryConnection.connectionStatus === "Connecting..." ||
                telemetryConnection.connectionStatus === "Reconnecting..."
              ? "bg-amber-400 animate-pulse"
              : "bg-red-500"
        }`}
      ></span>

      <span class="text-sm text-[#d1d5db]">
        {telemetryConnection.connectionStatus}
      </span>
    </div>

    <p class="mt-4 text-xs text-[#929aa8]">
      SignalR · WebSocket · Local backend
    </p>
  </section>

  <!-- Backend Message -->
  <section class="w-full max-w-[368px] rounded-xl border border-[#303641] bg-[#1e222a] p-6 shadow-lg">
    <h2 class="mb-4 text-lg font-semibold">
      Backend Message
    </h2>

    <p class="text-sm text-[#929aa8]">
      {telemetryConnection.latestMessage}
    </p>
  </section>

</main>
