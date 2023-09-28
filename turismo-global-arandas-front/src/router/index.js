import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home/Home'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Login',
    name: 'Login',
    component: () => import('../views/Authenticate/AuthenticateUser'),
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/Empleados',
    name: 'Employees',
    component: () => import('../views/Employees/EmployeesList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Empleados/:EmployeeId',
    name: 'Edit-Employees',
    component: () => import('../views/Employees/EmployeesEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Usuarios',
    name: 'Users',
    component: () => import('../views/Users/UserList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Usuarios/:UserId',
    name: 'Edit-User',
    component: () => import('../views/Users/UserEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Clientes',
    name: 'Customers',
    component: () => import('../views/Customers/CustomersList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Clientes/:CustomerId',
    name: 'Edit-Customer',
    component: () => import('../views/Customers/CustomersEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Proveedores',
    name: 'Providers',
    component: () => import('../views/Providers/ProviderList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Proveedores/:ProviderId',
    name: 'Edit-Provider',
    component: () => import('../views/Providers/ProviderEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Destinos',
    name: 'Destinations',
    component: () => import('../views/Destinations/DestinationList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Destinos/:DestinationId',
    name: 'Edit-Destination',
    component: () => import('../views/Destinations/DestinationEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Hoteles',
    name: 'Hotels',
    component: () => import('../views/Hotels/HotelsList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Hoteles/:HotelId',
    name: 'Edit-Hotel',
    component: () => import('../views/Hotels/HotelsEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Roles',
    name: 'Roles',
    component: () => import('../views/Roles/RolesList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Roles/:roleName',
    name: 'Edit-Role',
    component: () => import('../views/Roles/RoleEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesHoteleria',
    name: 'ReservationHotels',
    component: () => import('../views/Reservations/ReservationHotels/ReservationHotelsList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesHoteleria/:ReservationHotelId',
    name: 'Edit-ReservationHotels',
    component: () => import('../views/Reservations/ReservationHotels/ReservationHotelsEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesHoteleria/Nueva-Reservacion',
    name: 'ReservacionesHoteleria-AddNew',
    component: () => import('../views/Reservations/ReservationHotels/ReservationHotelsAddNew'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TiposReservaciones',
    name: 'TypeReservations',
    component: () => import('../views/TypeReservations/TypeReservationList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TiposReservaciones/:TypeReservationId',
    name: 'Edit-TypeReservation',
    component: () => import('../views/TypeReservations/TypeReservationEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/CategoriaReservaciones',
    name: 'CategoryReservations',
    component: () => import('../views/CategoryReservations/CategoryReservationList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/CategoriaReservaciones/:CategoryReservationId',
    name: 'Edit-CategoryReservation',
    component: () => import('../views/CategoryReservations/CategoryReservationEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Comisiones',
    name: 'Commissions',
    component: () => import('../views/Commissions/CommissionList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Comisiones/:CommissionId',
    name: 'Edit-Commission',
    component: () => import('../views/Commissions/CommissionEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesVuelos',
    name: 'ReservationFlight',
    component: () => import('../views/Reservations/ReservationFlights/ReservationFlightList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesVuelos/:FlightId',
    name: 'Edit-ReservationFlight',
    component: () => import('../views/Reservations/ReservationFlights/ReservationFlightEdit'),
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
