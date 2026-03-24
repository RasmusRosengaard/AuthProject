import { createRouter, createWebHistory } from 'vue-router';
import Dashboard from '../Pages/Dashboard.vue';
import Login from '../Pages/Login.vue';
import Register from '@/Pages/Register.vue';
import FrontPage from '@/Pages/FrontPage.vue';



const routes = [
    { path: '/dashboard', name: 'Dashboard', component: Dashboard },
    { path: '/login', name: 'Login', component: Login },
    { path: '/register', name: 'Register', component: Register },
    { path: '/', name: 'FrontPage', component: FrontPage }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
    const loggedIn = !!localStorage.getItem('user');

    // Pages that should be blocked if logged in
    const guestPages = ['/login', '/register'];

    if (loggedIn && guestPages.includes(to.path)) {
        // Redirect logged-in users away from login/register
        next('/dashboard');
    } else if (to.path === '/dashboard' && !loggedIn) {
        // Redirect non-logged-in users away from dashboard
        next('/'); // or '/login'
    } else {
        next(); // allow navigation
    }
});

export default router;