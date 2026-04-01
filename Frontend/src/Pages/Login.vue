<template>
    <Navbar></Navbar>
    <div class="login-form">
        <h1>Login</h1>
        <input v-model="email" type="text" placeholder="Email" />
        <input v-model="password" type="password" placeholder="Password" />

        <button @click="login">Login</button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { LoginAPI } from '../API/Backend';
import { useRouter } from 'vue-router';
import { parseJwt } from '../Services/JWTService';
import Navbar from '@/Components/Navbar.vue';

const email = ref('');
const password = ref('');
const router = useRouter();

function login() {
    LoginAPI(email.value, password.value)
        .then(data => {
            if (data?.token) {
                localStorage.setItem('token', data.token);

                // Optional: extract user info from token
                const user = parseJwt(data.token);
                console.log('Logged-in user:', user);

                router.push('/dashboard');
            }
        })
        .catch(err => {
            console.error('Login failed:', err);
        });
}
</script>