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
        <el-row align="middle" justify="center" :gutter="25">
          <el-col :span="24">
              <router-view />
          </el-col>
        </el-row>
      </el-main>
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
@import 'vue-select/dist/vue-select.css';

html {
  background-color: #e6eef9;
}
body {
  background-color: #e6eef9;
}
.el-aside {
  height: 100vh;
  width: 25% !important;
  background-color: #fff;
  box-shadow: 0 0 3px rgba(0, 0, 0, 0.5);
}
.el-main {
  background-color: #e6eef9;
}
.el-header.header-home {
  padding: 1rem;
}
</style>
