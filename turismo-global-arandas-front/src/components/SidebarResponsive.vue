<template>
  <el-menu
    background-color="#FFF"
    text-color="#85838D"
    active-text-color="#FFF"
    class="menu"
  >
    <el-header>
      <el-row align="middle" :gutter="30">
        <el-col :span="6">
          <el-image style="width: 40px; height: 40px" :src="logo" />
        </el-col>
        <el-col :span="18">
          <p>Turismo Global Arandas</p>
        </el-col>
      </el-row>
    </el-header>
    <el-divider class="m-0"></el-divider>
    <template v-for="item in menuItems" :key="item.index">
      <el-sub-menu v-if="item.submenu" :index="item.index">
        <template #title>
          <i :class="item.icon" class="menu-icon"></i>
          <span class="menu-title">{{ item.title }}</span>
        </template>
        <template v-for="subItem in item.submenu">
          <el-sub-menu
            v-if="subItem.submenu"
            :index="subItem.index"
            :key="subItem.index"
          >
            <template #title>
              <i :class="subItem.icon" class="sub-menu-icon"></i>
              <span class="sub-menu-title">{{ subItem.title }}</span>
            </template>
            <el-menu-item
              v-for="nestedItem in subItem.submenu"
              :index="nestedItem.index"
              :key="nestedItem.index"
              @click="handleMenuItemClick(nestedItem.path)"
            >
              <i :class="nestedItem.icon" class="sub-menu-icon"></i>
              <span class="sub-menu-title">{{ nestedItem.title }}</span>
            </el-menu-item>
          </el-sub-menu>
          <el-menu-item
            v-else
            :index="subItem.index"
            :key="subItem.index"
            @click="handleMenuItemClick(subItem.path)"
          >
            <i :class="subItem.icon" class="sub-menu-icon"></i>
            <span class="sub-menu-title">{{ subItem.title }}</span>
          </el-menu-item>
        </template>
      </el-sub-menu>
      <el-menu-item
        v-else
        :index="item.index"
        :key="item"
        @click="handleMenuItemClick(item.path)"
      >
        <i :class="item.icon" class="menu-icon"></i>
        <span class="menu-title">{{ item.title }}</span>
      </el-menu-item>
    </template>
  </el-menu>
</template>

<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Logo from '@/Images/logo.png'

export default {
  setup () {
    const logo = ref(Logo)
    const menuItems = ref([
      {
        index: '1',
        title: 'Inicio',
        path: '/',
        icon: 'bi bi-house'
      },
      {
        index: '2',
        title: 'Gestión de Usuarios',
        icon: 'bi bi-card-checklist',
        submenu: [
          {
            index: '2-1',
            title: 'Usuarios',
            path: '/Usuarios',
            icon: 'bi bi-person'
          },
          {
            index: '2-2',
            title: 'Roles',
            path: '/Roles',
            icon: 'bi bi-person-lock'
          }
        ]
      },
      {
        index: '3',
        title: 'Gestión de Empleados',
        icon: 'bi bi-person-fill-lock',
        submenu: [
          {
            index: '3-1',
            title: 'Empleados',
            path: '/Empleados',
            icon: 'bi bi-file-person'
          },
          {
            index: '3-2',
            title: 'Reservaciones del empleado',
            path: '/ReservacionesEmpleados',
            icon: 'bi bi-bar-chart-steps'
          }
        ]
      },
      {
        index: '4',
        title: 'Gestión operacional',
        icon: 'bi bi-clipboard-data',
        submenu: [
          {
            index: '4-1',
            title: 'Clientes',
            path: '/Clientes',
            icon: 'bi bi-people'
          },
          {
            index: '4-2',
            title: 'Proveedores',
            path: '/Proveedores',
            icon: 'bi bi-arrow-left-right'
          },
          {
            index: '4-3',
            title: 'Destinos',
            path: '/Destinos',
            icon: 'bi bi-geo'
          },
          {
            index: '4-4',
            title: 'Hoteles',
            path: '/Hoteles',
            icon: 'bi bi-building'
          },
          {
            index: '4-5',
            title: 'Servicios de proveedores',
            path: '/ServiciosProveedores',
            icon: 'bi bi-cash-stack'
          },
          {
            index: '4-6',
            title: 'Rutas',
            path: '/Rutas',
            icon: 'bi bi-sign-turn-right'
          },
          {
            index: '4-7',
            title: 'Aerolineas',
            path: '/Aerolineas',
            icon: 'bi bi-airplane'
          }
        ]
      },
      {
        index: '5',
        title: 'Gestion de reservaciones',
        icon: 'bi bi-cash',
        submenu: [
          {
            index: '5-1',
            title: 'Reservaciones',
            icon: 'bi bi-journal-text',
            submenu: [
              {
                index: '5-1-1',
                title: 'Reservaciones Hotelería',
                path: '/ReservacionesHoteleria',
                icon: 'bi bi-buildings'
              },
              {
                index: '5-1-2',
                title: 'Reservaciones Vuelos',
                path: '/ReservacionesVuelos',
                icon: 'bi bi-airplane-engines'
              },
              {
                index: '5-1-3',
                title: 'Reservaciones Vuelos de hoteleria',
                path: '/ReservacionesVuelosHoteleria',
                icon: 'bi bi-airplane-engines'
              },
              {
                index: '5-1-4',
                title: 'Reservaciones Tours',
                path: '/ReservacionesTours',
                icon: 'bi bi-bus-front'
              },
              {
                index: '5-1-5',
                title: 'Reservaciones Vehiculos',
                path: '/ReservacionesVehiculos',
                icon: 'bi bi-truck'
              }
            ]
          },
          {
            index: '5-2',
            title: 'Tipos de Reservaciones',
            path: '/TiposReservaciones',
            icon: 'bi bi-bezier2'
          },
          {
            index: '5-3',
            title: 'Tipos de Reservaciones grupales',
            path: '/TiposReservacionesGrupales',
            icon: 'bi bi-bezier'
          },
          {
            index: '5-4',
            title: 'Servicios adicionales',
            path: '/ServiciosAdicionales',
            icon: 'bi bi-cloud-haze'
          }
        ]
      },
      {
        index: '6',
        title: 'Gestor de contabilidad',
        icon: 'bi bi-arrow-left-right',
        submenu: [
          {
            index: '6-1',
            title: 'Metodos de pago',
            path: '/MetodosPago',
            icon: 'bi bi-book-half'
          },
          {
            index: '6-2',
            title: 'Conceptos de pago',
            path: '/ConceptosPago',
            icon: 'bi bi-cash-coin'
          },
          {
            index: '6-3',
            title: 'Gastos eventuales',
            path: '/GastosEventuales',
            icon: 'bi bi-wallet2'
          },
          {
            index: '6-4',
            title: 'Gastos fijos',
            path: '/GastosFijos',
            icon: 'bi bi-credit-card-2-front'
          },
          {
            index: '6-5',
            title: 'Pagos proveedores',
            path: '/PagosProveedor',
            icon: 'bi bi-currency-dollar'
          }
        ]
      }
    ])

    const router = useRouter()

    const handleMenuItemClick = path => {
      router.push(path)
    }

    return {
      menuItems,
      logo,
      handleMenuItemClick
    }
  }
}
</script>

<style scoped>
.sidebar {
  transition: width 0.5s;
  overflow: auto;
}
.el-menu {
  padding: 5%;
}
.el-menu-item {
  border-radius: 5px;
}
.el-menu-item:hover {
  background-color: #f3f2f8 !important;
}
.menu-icon {
  margin-right: 5%;
  font-size: 17px;
  letter-spacing: 0.15px;
}

.menu-title {
  font-size: 16px;
  letter-spacing: 0.15px;
}

.sub-menu-icon {
  margin-right: 5%;
  font-size: 18px;
  letter-spacing: 0.15px;
}

.sub-menu-title {
  font-size: 15px;
  letter-spacing: 0.15px;
}
li.el-menu-item.is-active {
  background-image: linear-gradient(to right, #7367f0, #9c94f4);
}
p {
  color: #85838d;
  font-size: 16px;
  font-weight: bold;
}
</style>
