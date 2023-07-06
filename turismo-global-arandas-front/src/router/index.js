import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home/Home.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Login',
    name: 'Login',
    component: () => import('../views/Authenticate/AuthenticateUser.vue'),
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/Empleados',
    name: 'Employees',
    component: () => import('../views/Employees/EmployeesList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Empleados/:EmployeeId',
    name: 'Edit-Employees',
    component: () => import('../views/Employees/EmployeesEdit.vue'),
    meta: {
      requiresAuth: true
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})
const Token = window.sessionStorage.getItem('Token')
router.beforeEach(async (to, from, next) => {
  if (['Login'].includes(to.name) && Token) {
    next({ name: 'Home' })
  } else if (to.meta.requiresAuth && !Token) {
    next({
      path: '/Login',
      query: { redirect: to.fullPath }
    })
  } else {
    next()
  }
})
export default router
