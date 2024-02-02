<template>
  <el-card>
    <el-row :gutter="25">
      <!-- Columna de búsqueda de reserva -->
      <el-col :span="10">
        <el-row>
          <el-col>
            <el-form-item label="Generar pago">
              <Autocomplete></Autocomplete
            ></el-form-item>
          </el-col>
        </el-row>
      </el-col>

      <!-- Columna de información de la empresa -->
      <el-col :span="7">
        <el-row justify="end">
          <el-col :span="8">
            <img src="@/Images/logo.png" alt="" width="50" />
          </el-col>
          <el-col :span="16">
            <strong>TURISMO GLOBAL ARANDAS</strong>
          </el-col>
        </el-row>
      </el-col>

      <!-- Columna de notificaciones y opciones de usuario -->
      <el-col :span="7">
        <el-row justify="end">
          <!-- Dropdown de notificaciones -->
          <el-dropdown class="right-button">
            <el-badge
              :value="countUnreadNotifications"
              class="notification-badge"
            >
              <el-button class="w-100" size="large" circle>
                <template #icon>
                  <i class="bi bi-bell"></i>
                </template>
              </el-button>
            </el-badge>
            <template #dropdown>
              <!-- Contenido del dropdown de notificaciones -->
              <i class="el-icon-bell"></i>
              <el-dropdown-menu style="width: 500px">
                <el-scrollbar height="500px">
                  <el-dropdown-item
                    v-for="notification in notifications"
                    :key="notification.id"
                  >
                    <!-- Contenido de cada elemento de notificación -->
                    <el-timeline
                      :class="{ 'unread-notification': !notification.isReaded }"
                    >
                      <el-timeline-item
                        :timestamp="notification.created"
                        @click="handleCommand(notification.notificationId)"
                      >
                        <div>
                          <strong>{{ notification.title }}</strong>
                        </div>
                        <div
                          :style="{ maxWidth: '500px', whiteSpace: 'pre-wrap' }"
                        >
                          <span>{{ notification.message }}</span>
                        </div>
                      </el-timeline-item>
                    </el-timeline>
                  </el-dropdown-item>
                </el-scrollbar>
              </el-dropdown-menu>
            </template>
          </el-dropdown>

          <!-- Dropdown de opciones de usuario -->
          <el-dropdown class="right-button">
            <el-button class="w-100" size="large" circle>
              <template #icon>
                <i class="bi bi-person-circle"></i>
              </template>
            </el-button>
            <template #dropdown>
              <!-- Contenido del dropdown de opciones de usuario -->
              <el-dropdown-item
                @click="
                  () => {
                    $router.push('/ReservacionesEmpleados')
                  }
                "
              >
                <span>
                  <i class="bi bi-card-text"></i>
                  Mis ventas
                </span>
              </el-dropdown-item>
              <el-dropdown-menu>
                <el-dropdown-item @click="closeSession">
                  <span>
                    <i class="bi bi-box-arrow-in-right"></i>
                    Cerrar sesión
                  </span>
                </el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </el-row>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, computed, onMounted } from 'vue'

import NotificationServices from '@/Services/Notifications.Services'
import HomeServices from '@/Services/Home.Services'
import Autocomplete from '../components/Autocomplete'

export default {
  components: {
    Autocomplete
  },
  setup () {
    const { getNotifications, isReadNotification } = NotificationServices()
    const { getAllReservations } = HomeServices()
    const notifications = ref([])
    const reservations = ref([
      { id: 1, name: 'Cliente1' },
      { id: 2, name: 'Cliente2' },
      { id: 3, name: 'Cliente3' }
      // ... Puedes agregar más datos de prueba según sea necesario
    ])
    const searchResults = ref([]) // Almacena los resultados de la búsqueda
    const searchQuery = ref('')
    // Obtener notificaciones y reservaciones al montar el componente
    onMounted(() => {
      getNotifications(data => {
        notifications.value = data
      })

      getAllReservations(data => {
        reservations.value = data
      })
    })

    // Método para actualizar notificaciones
    const refreshNotifications = () => {
      getNotifications(data => {
        notifications.value = data
      })
    }

    // Método para realizar la búsqueda de reservaciones
    const searchReservations = async query => {
      const searchQueryValue = query.toLowerCase()
      searchResults.value = reservations.value.filter(reservation =>
        reservation.name.toLowerCase().includes(searchQueryValue)
      )
    }

    // Manejar la lectura de notificaciones y actualizar
    const handleCommand = notificationId => {
      isReadNotification(notificationId, data => {
        refreshNotifications()
      })
    }

    // Cerrar sesión del usuario
    const closeSession = () => {
      window.sessionStorage.removeItem('Token')
      window.sessionStorage.removeItem('EmployeeId')
      window.sessionStorage.removeItem('User')
      window.sessionStorage.removeItem('Role')
      location.reload()
    }

    // Contar notificaciones no leídas
    const countUnreadNotifications = computed(() => {
      return notifications.value.filter(notification => !notification.isReaded)
        .length
    })

    return {
      notifications,
      reservations,
      searchResults,
      searchQuery,
      countUnreadNotifications,
      handleCommand,
      searchReservations,
      closeSession
    }
  }
}
</script>

<style>
.el-menu {
  border-bottom: none !important;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
}
.right-button {
  margin: 5px;
}
.unread-notification {
  background-color: #d3d3d3; /* Cambia este color según tus preferencias */
}
.notification-badge {
  margin-left: 5px; /* Ajusta el margen según tus necesidades */
}
strong{
  color: #5D5A68;
}
</style>
