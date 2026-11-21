import {
    HubConnectionBuilder,
    HubConnection,
    HubConnectionState,
    LogLevel
} from "@microsoft/signalr";

const SIGNALR_URL = "http://127.0.0.1:5284/hubs/telemetry";

export class TelemetryConnection {
    private connection: HubConnection;

    connectionStatus = $state("Disconnected");
    isConnected = $state(false);
    latestMessage = $state("Waiting for messages...");

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(SIGNALR_URL)
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        this.registerHandlers();
    }

    private registerHandlers(): void {
        this.connection.on("ReceiveMessage", (message: string) => {
            this.latestMessage = message;
            console.log("Received from backend:", message);
        });

        this.connection.onreconnecting(() => {
            this.connectionStatus = "Reconnecting...";
            this.isConnected = false;
        });

        this.connection.onreconnected(() => {
            this.connectionStatus = "Connected";
            this.isConnected = true;
        });

        this.connection.onclose(() => {
            this.connectionStatus = "Disconnected";
            this.isConnected = false;
        });
    }

    async start(): Promise<void> {
        if (this.connection.state !== HubConnectionState.Disconnected) {
            return;
        }

        this.connectionStatus = "Connecting...";

        try {
            await this.connection.start();

            this.connectionStatus = "Connected";
            this.isConnected = true;

            console.log("SignalR connected successfully!");
        } catch (error) {
            this.connectionStatus = "Connection failed";
            this.isConnected = false;

            console.error("SignalR connection error:", error);
        }
    }

    async stop(): Promise<void> {
        if (this.connection.state !== HubConnectionState.Disconnected) {
            await this.connection.stop();
        }
    }
}
