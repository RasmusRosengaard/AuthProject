const API_BASE = import.meta.env.VITE_API_URL || "http://localhost:8080/api"; 
// In Docker Compose, API calls should use: "http://api-dev:8080/api"

export async function LoginAPI(email, password) {
    const response = await fetch(`${API_BASE}/auth/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
    });

    if (!response.ok) {
        const text = await response.text(); // get backend error message
        throw new Error(`Login failed: ${text}`);
    }

    return await response.json();
}

export async function RegisterAPI(email, password) {
    const response = await fetch(`${API_BASE}/auth/create`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
    });

    if (!response.ok) {
        const text = await response.text();
        throw new Error(`Registration failed: ${text}`);
    }

    return await response.json();
}