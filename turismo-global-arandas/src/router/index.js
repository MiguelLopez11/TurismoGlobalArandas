import { createRouter, createWebHistory } from 'vue-router';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/views/Home/Home.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
    },
  },
  {
    path: '/Authenticate',
    name: 'Login',
    component: () => import('@/views/Authenticate/AuthenticateUser.vue'),
    meta: {
      requiresAuth: false,
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});
router.beforeEach(async (to, from, next) => {
  const Token = window.window.sessionStorage.getItem('Token');
  if (['Login'].includes(to.name) && Token) {
    next({ name: 'Home' });
  } else if (to.meta.requiresAuth && !Token) {
    next({
      path: '/Authenticate',
      query: { redirect: to.fullPath },
    });
  } else {
    next();
  }
});
export default router;
