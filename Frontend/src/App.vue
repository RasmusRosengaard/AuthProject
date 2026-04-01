<template>
  <router-view />
</template>

<script setup>
import { onMounted } from 'vue';
import { checkToken } from './Services/JWTService';
import { useRouter } from 'vue-router';

const router = useRouter(); 

onMounted(() => {
  checkToken(router); 
});

</script>