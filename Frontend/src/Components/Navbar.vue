<template>
  <nav>
    <!-- Left: main navigation -->
    <ul class="nav-left">
      <li><router-link to="/">Front page</router-link></li>
      <template v-if="loggedIn">
        <li><router-link to="/dashboard">Dashboard</router-link></li>
      </template>
    </ul>

    <!-- Right: auth links -->
    <ul class="nav-right">
      <template v-if="loggedIn">
        <li><a href="#" @click.prevent="logout">Logout</a></li>
      </template>
      <template v-else>
        <li><router-link to="/login">Login</router-link></li>
        <li><router-link to="/register">Register</router-link></li>
      </template>
    </ul>
  </nav>
</template>

<script setup>
import { ref, onMounted, watchEffect } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const loggedIn = ref(!!localStorage.getItem('token'));


watchEffect(() => {
  loggedIn.value = !!localStorage.getItem('token');
});

function logout() {
  localStorage.removeItem('token');
  router.replace('/'); 
  loggedIn.value = false;
}
</script>

<style>
nav {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #333;
  padding: 10px 20px;
}

nav ul {
  list-style: none;
  display: flex;
  margin: 0;
  padding: 0;
}

nav li {
  margin-right: 20px;
}

nav li:last-child {
  margin-right: 0;
}

nav a {
  color: white;
  text-decoration: none;
  font-weight: bold;
}

nav a:hover {
  text-decoration: underline;
}

/* Optional: smaller spacing on right-side links */
.nav-right li {
  margin-left: 20px;
  margin-right: 0;
}
</style>