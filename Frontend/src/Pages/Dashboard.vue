<template>
  <Navbar></Navbar>
  <h1>Dashboard</h1>
  <p v-if="email">Logged in as: {{ email }}</p>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import Navbar from '@/Components/Navbar.vue';

const email = ref('');


onMounted(() => {
  const userJson = localStorage.getItem('user');
  if (userJson) {
    const user = JSON.parse(userJson);
    email.value = user.email; 
  }
});
</script>