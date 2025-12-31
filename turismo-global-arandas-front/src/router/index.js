import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home/Home'),
    meta: {
      requiresAuth: true,
      noPadding: false
    }
  },
  {
    path: '/Unauthorized',
    name: 'Unauthorized',
    component: () => import('../views/Unauthorized/Unauthorized'),
    meta: {
      requiresAuth: true,
      noPadding: false
    }
  },
  {
    path: '/Login',
    name: 'Login',
    component: () => import('../views/Authenticate/AuthenticateUser'),
    meta: {
      requiresAuth: false,
      noPadding: true
    }
  },
  {
    path: '/Empleados',
    name: 'Employees',
    component: () => import('../views/Employees/EmployeesList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL'],
      noPadding: false
    }
  },
  {
    path: '/Empleados/:EmployeeId',
    name: 'Edit-Employees',
    component: () => import('../views/Employees/EmployeesEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesEmpleados',
    name: 'ReservationsByEmpleados',
    component: () => import('../views/Employees/ReservationsByEmployee/ReservationByEmployeeList'),
    meta: {
      requiresAuth: true,
      noPadding: false
    }
  },
  {
    path: '/Usuarios',
    name: 'Users',
    component: () => import('../views/Users/UserList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL'],
      noPadding: false
    }
  },
  {
    path: '/Usuarios/:UserId',
    name: 'Edit-User',
    component: () => import('../views/Users/UserEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL'],
      noPadding: false
    }
  },
  {
    path: '/Clientes',
    name: 'Customers',
    component: () => import('../views/Customers/CustomersList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Clientes/:CustomerId',
    name: 'Edit-Customer',
    component: () => import('../views/Customers/CustomersEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Proveedores',
    name: 'Providers',
    component: () => import('../views/Providers/ProviderList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Proveedores/:ProviderId',
    name: 'Edit-Provider',
    component: () => import('../views/Providers/ProviderEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Destinos',
    name: 'Destinations',
    component: () => import('../views/Destinations/DestinationList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Destinos/:DestinationId',
    name: 'Edit-Destination',
    component: () => import('../views/Destinations/DestinationEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Hoteles',
    name: 'Hotels',
    component: () => import('../views/Hotels/HotelsList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Hoteles/:HotelId',
    name: 'Edit-Hotel',
    component: () => import('../views/Hotels/HotelsEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Roles',
    name: 'Roles',
    component: () => import('../views/Roles/RolesList'),
    meta: {
      requiresAuth: true,
      noPadding: false
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
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesHoteleria/:ReservationHotelId',
    name: 'Edit-ReservationHotels',
    component: () => import('../views/Reservations/ReservationHotels/ReservationHotelsEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesHoteleria/Nueva-Reservacion',
    name: 'ReservacionesHoteleria-AddNew',
    component: () => import('../views/Reservations/ReservationHotels/ReservationHotelsAddNew'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/TiposReservaciones',
    name: 'TypeReservations',
    component: () => import('../views/TypeReservations/TypeReservationList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/TiposReservaciones/:TypeReservationId',
    name: 'Edit-TypeReservation',
    component: () => import('../views/TypeReservations/TypeReservationEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/CategoriaReservaciones',
    name: 'CategoryReservations',
    component: () => import('../views/CategoryReservations/CategoryReservationList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/CategoriaReservaciones/:CategoryReservationId',
    name: 'Edit-CategoryReservation',
    component: () => import('../views/CategoryReservations/CategoryReservationEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ServiciosProveedores',
    name: 'ServicesProvider',
    component: () => import('../views/ServicesProviders/ServiceProviderList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ServiciosProveedores/:ServiceId',
    name: 'Edit-ServicesProvider',
    component: () => import('../views/ServicesProviders/ServiceProviderEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesVuelos',
    name: 'ReservationFlight',
    component: () => import('../views/Reservations/ReservationFlights/ReservationFlightList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesVuelos/:FlightId',
    name: 'Edit-ReservationFlight',
    component: () => import('../views/Reservations/ReservationFlights/ReservationFlightEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesVuelosHoteleria',
    name: 'ReservationFlight-ReservationHotel',
    component: () => import('../views/Reservations/ReservationFlights/ReservationFlightReservadedByReservationHotelList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/TiposReservacionesGrupales',
    name: 'TypeReservationsGroup',
    component: () => import('../views/TypeReservationsGroup/TypeReservationsGroupList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/TiposReservacionesGrupales/:TypeReservationGrupalId',
    name: 'Edit-TypeReservationsGroup',
    component: () => import('../views/TypeReservationsGroup/TypeReservationsGroupEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/RelacionPagosReservacionHotel/:ReservationHotelId',
    name: 'PaymentsRelationReservatioHotel',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/RelacionPagosReservacionTour/:ReservationTourId',
    name: 'PaymentsRelationReservatioTour',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/RelacionPagosReservacionVehiculo/:ReservationVehicleId',
    name: 'PaymentsRelationReservatioVehicle',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/RelacionPagosReservacionVuelos/:ReservationFlightId',
    name: 'PaymentsRelationReservatioFlight',
    component: () => import('../views/Reservations/PaymentsRelationReservations'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesTours',
    name: 'ReservationsTour',
    component: () => import('../views/Reservations/ReservationTours/ReservationTourList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesTours/NuevaReserva',
    name: 'ReservationsTour-AddNew',
    component: () => import('../views/Reservations/ReservationTours/ReservationTourAddNew'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesTours/:ReservationTourId',
    name: 'ReservationsTour-Edit',
    component: () => import('../views/Reservations/ReservationTours/ReservationTourEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/MetodosPago',
    name: 'PaymentMethods',
    component: () => import('../views/PaymentMethods/PaymentMethodsList.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/MetodosPago/:PaymentMethodId',
    name: 'Edit-PaymentMethods',
    component: () => import('../views/PaymentMethods/PaymentMethodsEdit.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ConceptosPago',
    name: 'Paymentconcepts',
    component: () => import('../views/PaymentConcepts/PaymentConceptsList.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ConceptosPago/:PaymentConceptId',
    name: 'Edit-Paymentconcepts',
    component: () => import('../views/PaymentConcepts/PaymentConceptsEdit.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Rutas',
    name: 'Routes',
    component: () => import('../views/Routes/RoutesList.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Rutas/:RouteId',
    name: 'Edit-Routes',
    component: () => import('../views/Routes/RoutesEdit.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesVehiculos',
    name: 'ReservationVehicles',
    component: () => import('../views/Reservations/ReservationVehicles/ReservationVehiclesList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ReservacionesVehiculos/:ReservationVehicleId',
    name: 'Edit-ReservationVehicles',
    component: () => import('../views/Reservations/ReservationVehicles/ReservationVehiclesEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ServiciosAdicionales',
    name: 'AditionalServices',
    component: () => import('../views/AditionalServices/AditionalServicesList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ServiciosAdicionales/:AditionalServiceId',
    name: 'Edit-AditionalServices',
    component: () => import('../views/AditionalServices/AditionalServiceEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/GastosEventuales',
    name: 'ExpensesEventual',
    component: () => import('../views/ExpensesEventual/ExpensesEventualList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/GastosEventuales/:ExpenseId',
    name: 'Edit-ExpensesEventual',
    component: () => import('../views/ExpensesEventual/ExpensesEventualEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/GastosFijos',
    name: 'ExpensesFixed',
    component: () => import('../views/ExpensesFixed/ExpensesFixedList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/GastosFijos/:ExpenseFixedId',
    name: 'Edit-ExpensesFixed',
    component: () => import('../views/ExpensesFixed/ExpensesFixedEdit'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor',
    name: 'PaymentProvider',
    component: () => import('../views/PaymentProviders/PaymentProviderList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/:PaymentProviderId',
    name: 'PaymentProvider-Details',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionHotel/:ReservationHotelId',
    name: 'PaymentProvider-Details-ReservationHotel',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionTour/:ReservationTourId',
    name: 'PaymentProvider-Details-ReservationTour',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionVuelo/:ReservationFlightId',
    name: 'PaymentProvider-Details-ReservationFlight',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor/DetallesPago/ReservacionVehiculo/:ReservationVehicleId',
    name: 'PaymentProvider-Details-ReservationVehicle',
    component: () => import('../views/PaymentProviders/PaymentDetails'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/PagosProveedor/Documentos/:PaymentProviderId',
    name: 'PaymentProvider-Documents',
    component: () => import('../views/PaymentProviders/PaymentProviderDocuments'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Aerolineas',
    name: 'Airlines',
    component: () => import('../views/Airlines/AirlineList.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Aerolineas/:AirlineId',
    name: 'Edit-Airlines',
    component: () => import('../views/Airlines/AirlineEdit.vue'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/ResumenVentas',
    name: 'Resume',
    component: () => import('../components/FinancialSummary'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
    }
  },
  {
    path: '/Empleados/Comisiones',
    name: 'CommissionsEmployee',
    component: () => import('../views/CommissionsEmployee/CommissionsEmployeeList'),
    meta: {
      requiresAuth: true,
      roles: ['administrador', 'GERENTE GENERAL', 'AGENTE DE VENTAS', 'CONTABLE ADMINISTRATIVO'],
      noPadding: false
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
  const role = window.sessionStorage.getItem('Role')
  return role ? [role] : []
}

export default router
