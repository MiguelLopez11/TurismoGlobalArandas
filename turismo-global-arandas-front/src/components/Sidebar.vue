<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <el-menu text-color="#7A7782" active-text-color="#fff" class="p-3">
    <el-header>
      <el-row :gutter="25" justify="center">
        <el-col :xs="6" :sm="6" :md="6" :xl="24" :lg="24">
          <div>
            <el-image style="width: 50px; height: 50px" :src="logo" />
            <label>Turismo Global Arandas</label>
          </div>
        </el-col>
      </el-row>
    </el-header>
    <el-divider />
    <template v-for="item in menuItems">
      <el-sub-menu v-if="item.submenu" :index="item.index" :key="item.index">
        <template #title>
          <i :class="item.icon"></i>
          <span>{{ item.title }}</span>
        </template>
        <el-menu-item
          style="
            background: linear-gradient(to right, #7367f0, #9c94f4);
            color: white;
          "
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
        <i :class="item.icon"></i>
        <span>{{ item.title }}</span>
      </el-menu-item>
    </template>
  </el-menu>
</template>

<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Logo from '@/Images/logo.jpg'
export default {
  setup () {
    const activeMenu = ref('1')
    const sidebarStyle = ref({})
    const logo = ref(Logo)
    const menuItems = ref([
      {
        index: '1',
        title: 'Empleados',
        path: '/Empleados',
        icon: 'fa-solid fa-user'
      },
      {
        index: '2',
        title: 'Usuarios',
        path: '/Empleados',
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
    return {
      activeMenu,
      sidebarStyle,
      menuItems,
      logo,
      handleMenuItemClick
    }
  }
}
</script>
<style scoped>
.el-menu-item.is-active {
  background: linear-gradient(to right, #7367f0, #9c94f4);
  color: white;
}
li {
  border-radius: 5px;
}
.el-menu {
  border-right: #fff;
}
.el-sub-menu.is-opened {
  background-color: #f7f7f7;
}
.el-sub-menu__title {
  border-radius: 5px;
}
.el-sub-menu__title:hover {
  background-color: #faf9f9;
}
</style>
