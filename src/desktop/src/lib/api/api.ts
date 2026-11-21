const API_BASE_URL = "http://localhost:5284";

export interface HealthResponse {
    status: string;
    application: string;
}

export async function getHealth(): Promise<HealthResponse> {
    const response = await fetch(`${API_BASE_URL}/health`);

    if (!response.ok) {
        throw new Error(
            `API request failed: ${response.status} ${response.statusText}`
        );
    }

    return await response.json() as HealthResponse;
}
