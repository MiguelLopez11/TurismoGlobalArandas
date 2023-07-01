<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <el-card class="card-sidebar">
    <el-aside class="sidebar" :width="'200px'">
      <el-menu
        :default-active="activeMenu"
        text-color="#726F7B"
        active-text-color="#007bff"
      >
        <template v-for="item, index in menuItems">
          <el-sub-menu
            v-if="item.submenu"
            :index="index"
            :key="item.index"
          >
            <template #title>
              <i :class="item.icon"></i>
              <span>{{ item.title }}</span>
            </template>
            <el-menu-item
              v-for="subItem, index in item.submenu"
              :index="index"
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
            <i :class="item.icon"></i>
            <span>{{ item.title }}</span>
          </el-menu-item>
        </template>
      </el-menu>
    </el-aside>
  </el-card>
</template>

<script>
import { ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'

export default {
  setup () {
    const activeMenu = ref('')
    const sidebarWidth = ref('250px')
    const sidebarStyle = ref({})
    const menuItems = ref([
      {
        index: '1',
        title: 'Home',
        path: '/inicio',
        icon: 'fa-solid fa-user'
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
      handleMenuItemClick
    }
  }
}
</script>
<style scoped>
.sidebar {
  transition: width 0.3s;
  overflow: hidden;
  height: 85vh;
  background-color: #fff;
}

.el-menu {
  height: 100%;
  border-right: none;
}
.card-sidebar {
  width: 20%;
}
.el-menu-item{
    gap: 15px;
}
</style>
