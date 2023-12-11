<template>
  <el-card>
    <div class="card-header">
      <el-dropdown class="right-button">
        <el-badge :value="countUnreadNotifications" class="notification-badge">
          <el-button class="w-100" size="large" circle>
            <template #icon>
              <i class="bi bi-bell"></i>
            </template>
          </el-button>
        </el-badge>
        <template #dropdown>
          <i class="el-icon-bell"></i>
          <el-dropdown-menu trigger="click">
            <el-dropdown-item
              v-for="notification in notifications"
              :key="notification.id"
            >
              <el-timeline
                :class="{ 'unread-notification': !notification.isReaded }"
              >
                <el-timeline-item
                  :timestamp="notification.created"
                  @click="handleCommand(notification.notificationId)"
                >
                  <div>
                    <strong> {{ notification.title }} </strong>
                  </div>
                  <div :style="{ maxWidth: '500px', whiteSpace: 'pre-wrap' }">
                    <span>
                      {{ notification.message }}
                    </span>
                  </div>
                </el-timeline-item>
              </el-timeline>
            </el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>

      <el-dropdown class="right-button">
        <el-button class="w-100" size="large" circle>
          <template #icon>
            <i class="bi bi-person-circle"></i>
          </template>
        </el-button>
        <template #dropdown>
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
    </div>
  </el-card>
</template>

<script>
import {} from '@/router'
import { Tools } from '@element-plus/icons-vue'
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import NotificationServices from '@/Services/Notifications.Services'
export default {
  components: {
    // eslint-disable-next-line vue/no-unused-components
    Tools
  },
  setup () {
    const icon = ref(Tools)
    const { getNotifications, isReadNotification } = NotificationServices()
    const notifications = ref([])
    const redirect = useRouter()
    getNotifications(data => {
      notifications.value = data
    })
    const handleCommand = notificationId => {
      isReadNotification(notificationId, data => {})
      console.log(notificationId)
    }
    const closeSession = () => {
      window.sessionStorage.removeItem('Token')
      window.sessionStorage.removeItem('EmployeeId')
      window.sessionStorage.removeItem('User')
      window.sessionStorage.removeItem('Role')
      redirect.go(0)
    }
    const countUnreadNotifications = computed(() => {
      return notifications.value.filter(notification => !notification.isReaded)
        .length
    })
    return {
      Tools,
      notifications,
      icon,
      countUnreadNotifications,
      handleCommand,
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
  justify-content: flex-end;
  align-items: flex-start;
}
.right-button {
  margin: 5px;
}
.unread-notification {
  background-color: #e4ecf7; /* Cambia este color según tus preferencias */
}
.notification-badge {
  margin-left: 5px; /* Ajusta el margen según tus necesidades */
}
</style>
