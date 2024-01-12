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
    path: '/ReservacionesEmpleados',
    name: 'ReservationsByEmpleados',
    component: () => import('../views/Employees/ReservationsByEmployee/ReservationByEmployeeList'),
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
    path: '/ServiciosProveedores',
    name: 'ServicesProvider',
    component: () => import('../views/ServicesProviders/ServiceProviderList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ServiciosProveedores/:ServiceId',
    name: 'Edit-ServicesProvider',
    component: () => import('../views/ServicesProviders/ServiceProviderEdit'),
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
  },
  {
    path: '/ReservacionesVuelosHoteleria',
    name: 'ReservationFlight-ReservationHotel',
    component: () => import('../views/Reservations/ReservationFlights/ReservationFlightReservadedByReservationHotelList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TiposReservacionesGrupales',
    name: 'TypeReservationsGroup',
    component: () => import('../views/TypeReservationsGroup/TypeReservationsGroupList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TiposReservacionesGrupales/:TypeReservationGrupalId',
    name: 'Edit-TypeReservationsGroup',
    component: () => import('../views/TypeReservationsGroup/TypeReservationsGroupEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/RelacionPagosReservacionHotel/:ReservationHotelId',
    name: 'PaymentsRelationReservatioHotel',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/RelacionPagosReservacionTour/:ReservationTourId',
    name: 'PaymentsRelationReservatioTour',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/RelacionPagosReservacionVehiculo/:ReservationVehicleId',
    name: 'PaymentsRelationReservatioVehicle',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/RelacionPagosReservacionVuelos/:ReservationFlightId',
    name: 'PaymentsRelationReservatioFlight',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesTours',
    name: 'ReservationsTour',
    component: () => import('../views/Reservations/ReservationTours/ReservationTourList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesTours/NuevaReserva',
    name: 'ReservationsTour-AddNew',
    component: () => import('../views/Reservations/ReservationTours/ReservationTourAddNew'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesTours/:ReservationTourId',
    name: 'ReservationsTour-Edit',
    component: () => import('../views/Reservations/ReservationTours/ReservationTourEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/MetodosPago',
    name: 'PaymentMethods',
    component: () => import('../views/PaymentMethods/PaymentMethodsList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/MetodosPago/:PaymentMethodId',
    name: 'Edit-PaymentMethods',
    component: () => import('../views/PaymentMethods/PaymentMethodsEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ConceptosPago',
    name: 'Paymentconcepts',
    component: () => import('../views/PaymentConcepts/PaymentConceptsList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ConceptosPago/:PaymentConceptId',
    name: 'Edit-Paymentconcepts',
    component: () => import('../views/PaymentConcepts/PaymentConceptsEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Rutas',
    name: 'Routes',
    component: () => import('../views/Routes/RoutesList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Rutas/:RouteId',
    name: 'Edit-Routes',
    component: () => import('../views/Routes/RoutesEdit.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesVehiculos',
    name: 'ReservationVehicles',
    component: () => import('../views/Reservations/ReservationVehicles/ReservationVehiclesList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ReservacionesVehiculos/:ReservationVehicleId',
    name: 'Edit-ReservationVehicles',
    component: () => import('../views/Reservations/ReservationVehicles/ReservationVehiclesEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ServiciosAdicionales',
    name: 'AditionalServices',
    component: () => import('../views/AditionalServices/AditionalServicesList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ServiciosAdicionales/:AditionalServiceId',
    name: 'Edit-AditionalServices',
    component: () => import('../views/AditionalServices/AditionalServiceEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/GastosEventuales',
    name: 'ExpensesEventual',
    component: () => import('../views/ExpensesEventual/ExpensesEventualList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/GastosEventuales/:ExpenseId',
    name: 'Edit-ExpensesEventual',
    component: () => import('../views/ExpensesEventual/ExpensesEventualEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/GastosFijos',
    name: 'ExpensesFixed',
    component: () => import('../views/ExpensesFixed/ExpensesFixedList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/GastosFijos/:ExpenseFixedId',
    name: 'Edit-ExpensesFixed',
    component: () => import('../views/ExpensesFixed/ExpensesFixedEdit'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/PagosProveedor',
    name: 'PaymentProvider',
    component: () => import('../views/PaymentProviders/PaymentProviderList'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/:PaymentProviderId',
    name: 'PaymentProvider-Details',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionHotel/:ReservationHotelId',
    name: 'PaymentProvider-Details-ReservationHotel',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionTour/:ReservationTourId',
    name: 'PaymentProvider-Details-ReservationTour',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionVuelo/:ReservationFlightId',
    name: 'PaymentProvider-Details-ReservationFlight',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionVehiculo/:ReservationVehicleId',
    name: 'PaymentProvider-Details-ReservationVehicle',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Aerolineas',
    name: 'Airlines',
    component: () => import('../views/Airlines/AirlineList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Aerolineas/:AirlineId',
    name: 'Edit-Airlines',
    component: () => import('../views/Airlines/AirlineEdit.vue'),
    meta: {
      requiresAuth: true,
      roles: ['ADMINISTRADOR']
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
  } else if (to.meta.roles) {
    // Verifica si se requieren roles para la ruta
    const userRoles = obtenerRolesUsuario() // Implementa la función para obtener roles del usuario

    if (userRoles.some(role => to.meta.roles.includes(role))) {
      // El usuario tiene al menos uno de los roles requeridos
      next()
    } else {
      // El usuario no tiene los roles necesarios
      // Redirige o muestra un mensaje de error según tu lógica
      next('/Unauthorized')
    }
  } else {
    // Rutas sin restricciones de roles
    next()
  }
})

// Función de ejemplo para obtener roles del usuario
function obtenerRolesUsuario () {
  // Obtén los roles del usuario desde donde sea que los almacenes (localStorage, sessionStorage, etc.)
  // Devuelve un array de roles (por ejemplo, ['Admin', 'User'])
  const roles = window.sessionStorage.getItem('Roles')
  return roles ? JSON.parse(roles) : []
}

export default router
