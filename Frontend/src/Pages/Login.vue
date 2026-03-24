<template>
    <Navbar></Navbar>
    <div class="login-form">
        <h1>Login</h1>
        <input v-model="email"  type="text" placeholder="Email" />
        <input v-model="password" type="password" placeholder="Password" />

        <button @click="login">Login</button>
    </div>

</template>

<script setup>
import { ref } from 'vue';
import { LoginAPI } from '../API/Backend';
import { useRouter } from 'vue-router';
import Navbar from '@/Components/Navbar.vue';

const email = ref('');
const password = ref('');
const router = useRouter();



function login() {
    LoginAPI(email.value, password.value)
        .then(data => {
            if (data) {
                localStorage.setItem('user', JSON.stringify(data));
                router.push('/dashboard');

            }
        })
        .catch(err => {
            console.error('Login failed:', err);
        });
}


</script>