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
  },
  {
    path: '/Usuarios',
    name: 'Users',
    component: () => import('../views/Users/UserList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Usuarios/:UserId',
    name: 'Edit-User',
    component: () => import('../views/Users/UserEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Clientes',
    name: 'Customers',
    component: () => import('../views/Customers/CustomersList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Clientes/:CustomerId',
    name: 'Edit-Customer',
    component: () => import('../views/Customers/CustomersEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Proveedores',
    name: 'Providers',
    component: () => import('../views/Providers/ProviderList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Proveedores/:ProviderId',
    name: 'Edit-Provider',
    component: () => import('../views/Providers/ProviderEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Destinos',
    name: 'Destinations',
    component: () => import('../views/Destinations/DestinationList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Destinos/:DestinationId',
    name: 'Edit-Destination',
    component: () => import('../views/Destinations/DestinationEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Hoteles',
    name: 'Hotels',
    component: () => import('../views/Hotels/HotelsList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Hoteles/:HotelId',
    name: 'Edit-Hotel',
    component: () => import('../views/Hotels/HotelsEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Roles',
    name: 'Roles',
    component: () => import('../views/Roles/RolesList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Roles/:roleName',
    name: 'Edit-Role',
    component: () => import('../views/Roles/RoleEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Reservaciones',
    name: 'Reservations',
    component: () => import('../views/Reservations/ReservationsList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Reservaciones/Nueva-Reservacion',
    name: 'Reservations-AddNew',
    component: () => import('../views/Reservations/ReservationAddNew.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TiposReservaciones',
    name: 'TypeReservations',
    component: () => import('../views/TypeReservations/TypeReservationList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TiposReservaciones/:TypeReservationId',
    name: 'Edit-TypeReservation',
    component: () => import('../views/TypeReservations/TypeReservationEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/CategoriaReservaciones',
    name: 'CategoryReservations',
    component: () => import('../views/CategoryReservations/CategoryReservationList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/CategoriaReservaciones/:CategoryReservationId',
    name: 'Edit-CategoryReservation',
    component: () => import('../views/CategoryReservations/CategoryReservationEdit.vue'),
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
