<template>
    <Navbar></Navbar>
    <div class="register-form">
        <h1>Register</h1>
        <input v-model="email" type="text" placeholder="Email" />
        <input v-model="password" type="password" placeholder="Password" />

        <button @click="register">Register</button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { LoginAPI, RegisterAPI } from '../API/Backend';
import { useRouter } from 'vue-router';
import Navbar from '@/Components/Navbar.vue';

const email = ref('');
const password = ref('');
const router = useRouter();

function register() {
    RegisterAPI(email.value, password.value)
        .then(() => LoginAPI(email.value, password.value)) 
        .then(data => {
            if (data?.token) {
                localStorage.setItem('token', data.token);
                router.push('/dashboard');
            }
        })
        .catch(err => {
            console.error('Registration/Login failed:', err);
        });
}
</script>