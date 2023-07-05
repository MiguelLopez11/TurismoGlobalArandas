<template>
  <el-container>
    <el-aside v-if="Token">
      <Sidebar :showSidebar="showSidebar" />
    </el-aside>
    <el-container>
      <el-header v-if="Token" class="header-home">
          <Navbar @toggleSidebar="toggleSidebar" />
      </el-header>
      <el-main class="p-3 mt-5">
        <router-view />
      </el-main>
      <!-- <el-footer v-if="Token">
        <el-row justify="center"> Miguel López SA de CV </el-row>
      </el-footer> -->
    </el-container>
  </el-container>
</template>

<script>
import { ref } from 'vue'
import Navbar from './components/NavbarResponsive.vue'
import Sidebar from './components/SidebarResponsive.vue'

export default {
  components: {
    Navbar,
    Sidebar
  },
  setup () {
    const showSidebar = ref(true)
    const Token = window.sessionStorage.getItem('Token')
    const toggleSidebar = () => {
      showSidebar.value = !showSidebar.value
    }

    return {
      showSidebar,
      Token,
      toggleSidebar
    }
  }
}
</script>

<style>
/* Option 2: Import via CSS */
@import url('https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css');
@import '@/Css/Easy-data-table.css';

html {
  background-color: #f8f7fa;
}
body {
  background-color: #f8f7fa;
}
.el-aside {
  height: 100vh;
  width: 18% !important;
  background-color: #2f3349;
  box-shadow: 0 0 3px rgba(0, 0, 0, 0.5);
}
.el-header.header-home {
  padding: 1rem;
}
</style>
