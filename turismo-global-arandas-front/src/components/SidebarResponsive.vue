<template>
  <el-menu
    background-color="#2F3349"
    text-color="#9498B3"
    active-text-color="#F2F1FC"
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
    <template v-for="item in menuItems">
      <el-sub-menu v-if="item.submenu" :index="item.index" :key="item.index">
        <template #title>
          <i :class="item.icon" class="menu-icon"></i>
          <span class="menu-title">{{ item.title }}</span>
        </template>
        <el-menu-item
          v-for="subItem in item.submenu"
          :index="subItem.index"
          :key="subItem.index"
          @click="handleMenuItemClick(subItem.path)"
        >
          <i :class="subItem.icon" class="sub-menu-icon"></i>
          <span class="sub-menu-title">{{ subItem.title }}</span>
        </el-menu-item>
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
        title: 'Gestor administrativo',
        icon: 'bi bi-card-checklist',
        submenu: [
          {
            index: '1',
            title: 'Empleados',
            path: '/Empleados',
            icon: 'bi bi-person'
          }
        ]
      },
      {
        index: '3',
        title: 'Servicios',
        path: '/servicios',
        icon: 'el-icon-service'
      },
      {
        index: '4',
        title: 'Acerca de',
        path: '/acerca-de',
        icon: 'el-icon-info'
      },
      {
        index: '5',
        title: 'Contacto',
        path: '/contacto',
        icon: 'el-icon-phone'
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

.menu {
  border-radius: 8px;
  padding: 5%;
}

.el-menu {
  border-right: none;
}

.menu-icon {
  margin-right: 15px;
  font-size: 17px;
  font-weight: bold;
  letter-spacing: 0.15px;
}

.menu-title {
  font-size: 16px;
  font-weight: bold;
  letter-spacing: 0.15px;
}

.sub-menu-icon {
  margin-right: 15px;
  font-size: 18px;
  font-weight: bold;
  letter-spacing: 0.15px;
}

.sub-menu-title {
  font-size: 15px;
  font-weight: bold;
  letter-spacing: 0.15px;
}
p {
  color: #9498b3;
  font-size: 16px;
  font-weight: bold;
}
</style>
