<template>
  <el-container class="app-container">
    <el-aside v-if="Token" class="responsive-sidebar" :style="{ height: windowHeight + 'px' }">
      <Sidebar />
    </el-aside>
    <el-container>
      <el-header v-if="Token" class="header-home">
        <Navbar />
      </el-header>
      <el-main class="main-content">
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
import Navbar from './components/NavbarResponsive.vue'
import Sidebar from './components/SidebarResponsive.vue'

export default {
  components: {
    Navbar,
    Sidebar
  },
  setup () {
    const Token = window.sessionStorage.getItem('Token')
    const windowHeight = window.innerHeight

    return {
      windowHeight,
      Token
    }
  },
  mounted () {
    window.addEventListener('resize', this.handleResize)
  },
  // eslint-disable-next-line vue/no-deprecated-destroyed-lifecycle
  beforeDestroy () {
    window.removeEventListener('resize', this.handleResize)
  },
  methods: {
    handleResize () {
      this.windowHeight = window.innerHeight
    }
  }
}
</script>

<style>
/* Option 2: Import via CSS */
@import url('https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css');
@import '@/Css/Easy-data-table.css';
@import 'vue-select/dist/vue-select.css';

/* Ensure a full-height layout */
html,
body,
#app {
  height: 100%;
  margin: 0;
  padding: 0;
  display: flex;
  background-color: #e6eef9;
  flex-direction: column;
}

/* Set the container to full height */
.app-container {
  flex: 1;
  display: flex;
  /* flex-direction: column; */
}

.el-aside {
  width: 25% !important;
  background-color: #fff;
  flex: 0 0 25%;
  min-height: 0;
  overflow-y: auto !important;
  z-index: 1000;
}

.el-main {
  background-color: #e6eef9;
  flex: 1;
  display: flex;
  flex-direction: column;
  min-height: 0;
  overflow-y: auto;
}

/* ... (other styles) */

/* Media query for smaller screens */
@media (max-width: 768px) {
  .responsive-sidebar {
    width: 0;
    flex: 0 0 0;
  }
}
</style>
