<template>
  <router-view />
</template>

<script setup>
import { onMounted } from 'vue';
import { checkToken } from './Services/JWTService';

onMounted(() => {
  checkToken(); 
});

</script>