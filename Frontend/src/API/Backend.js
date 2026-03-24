export async function LoginAPI(email, password) {
    const response = await fetch('http://localhost:8080/api/auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ 
            email: email, 
            password:password 
        })
    });

    if (!response.ok) {
        throw new Error(`Login failed: ${response.status}`);
    }

    return await response.json();
}

export function RegisterAPI(email, password) {
    return fetch('http://localhost:8080/api/auth/create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email: email,   
            password: password
        })
    })
    .then(response => response.json());
}