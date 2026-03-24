<template>
    <Navbar></Navbar>
    <div class="register-form">
        <h1>Register</h1>
        <input v-model="email"  type="text" placeholder="Email" />
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
        .then(() => {
            console.log('Registration successful');
            LoginAPI(email.value, password.value)
                .then(data => {
                    if (data) {
                        localStorage.setItem('user', JSON.stringify(data));
                        router.push('/dashboard');
                    }
                })
                .catch(err => {
                    console.error('Login after registration failed:', err);
                });
            router.push('/dashboard');
        })
        .catch((error) => {
            
            console.error('Registration failed:', error);
        });
}


</script>