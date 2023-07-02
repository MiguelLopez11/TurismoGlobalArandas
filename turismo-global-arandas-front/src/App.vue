<template>
  <el-container>
    <el-aside v-if="Token" width="300px">
      <Sidebar :showSidebar="showSidebar" />
    </el-aside>
    <el-container>
      <el-header v-if="Token" class="p-2">
        <Navbar @toggleSidebar="toggleSidebar" />
      </el-header>
      <el-main class="p-5">
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
import Sidebar from './components/Sidebar.vue'

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
html {
  background-color: #f8f7fa;
}
body {
  background-color: #f8f7fa;
}
.el-aside {
  height: 100vh;
  background-color: #fff;
  box-shadow: 0 0 3px rgba(0, 0, 0, 0.5);
}

</style>
