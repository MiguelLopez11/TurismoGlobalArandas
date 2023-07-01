<template>
  <el-card>
    <el-header color="#fff" class="navbar">
      <div class="container">
        <div class="navbar-brand">
            <el-image style="width: 100px; height: 50px;"  :src="logo"/>
          <span>Turismo Global Arandas</span>
        </div>
        <el-collapse-transition>
          <!-- background-color="#f8f9fa" -->
          <el-menu
            :default-active="activeMenu"
            text-color="#726F7B"
            active-text-color="#007bff"
            mode="horizontal"
          >
            <template v-for="item in menuItems">
              <el-sub-menu
                v-if="item.submenu"
                :index="item.index"
                :key="item.index"
              >
                <template #title>
                  <font-awesome-icon :class="item.icon" />{{ item.title }}
                </template>
                <el-menu-item
                  v-for="subItem in item.submenu"
                  :index="subItem.index"
                  :key="subItem.index"
                  @click="handleMenuItemClick(subItem.path)"
                >
                  {{ subItem.title }}
                </el-menu-item>
              </el-sub-menu>
              <el-menu-item
                v-else
                :index="item.index"
                :key="item"
                @click="handleMenuItemClick(item.path)"
              >
                <font-awesome-icon :icon="item.icon" />
                <span>{{ item.title }}</span>
              </el-menu-item>
            </template>
          </el-menu>
        </el-collapse-transition>
      </div>
    </el-header>
  </el-card>
</template>

<script>
import { ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'
import Logo from '@/Images/logo.jpg'

export default {
  setup () {
    const activeMenu = ref('')
    const sidebarWidth = ref('250px')
    const sidebarStyle = ref({})
    const logo = ref(Logo)
    const menuItems = ref([
      {
        index: '1',
        title: 'Home',
        path: '/inicio',
        icon: 'fa-solid fa-user-secret'
      },
      {
        index: '2',
        title: 'Productos',
        path: '/productos',
        icon: 'fa-solid fa-user'
      },
      {
        index: '3',
        title: 'Servicios',
        path: '/servicios',
        icon: 'fa-solid fa-user'
      },
      {
        index: '4',
        title: 'Acerca de',
        path: '/acerca-de',
        icon: 'fa-solid fa-user'
      },
      {
        index: '5',
        title: 'Contacto',
        path: '/contacto',
        icon: 'fa-solid fa-user'
      }
    ])

    const router = useRouter()

    const handleMenuItemClick = path => {
      router.push(path)
    }

    const handleMediaQuery = mediaQuery => {
      if (mediaQuery.matches) {
        sidebarWidth.value = '100%'
        sidebarStyle.value = { position: 'fixed', top: '60px', bottom: '0' }
      } else {
        sidebarWidth.value = '250px'
        sidebarStyle.value = {}
      }
    }

    const mediaQuery = window.matchMedia('(max-width: 768px)')

    onMounted(() => {
      handleMediaQuery(mediaQuery)
      mediaQuery.addEventListener('change', handleMediaQuery)
    })

    onUnmounted(() => {
      mediaQuery.removeEventListener('change', handleMediaQuery)
    })

    return {
      activeMenu,
      sidebarWidth,
      sidebarStyle,
      menuItems,
      logo,
      handleMenuItemClick
    }
  }
}
</script>

<style scoped>
.sidebar {
  transition: width 0.3s;
  overflow: auto;
}

.el-menu {
  border-right: none;
}

@media screen and (max-width: 768px) {
  .el-menu {
    height: 100vh;
  }
}
.el-menu-item{
    gap: 15px;
}
</style>
