<template>
  <el-container class="app-container">
    <el-aside v-if="Token" class="responsive-sidebar">
      <Sidebar />
    </el-aside>
    <el-container>
      <el-main class="main-content">
        <el-header v-if="Token" class="mb-5 p-0">
          <Navbar />
        </el-header>
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
  height: 100% !important;
  flex: 1;
  display: flex;
  /* flex-direction: column; */
}

.el-aside {
  width: 25% !important;
  background-color: #fff;
  flex: 0 0 25%;
  z-index: 1000;
}
body.swal2-shown.swal2-height-auto {
  height: inherit !important;
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
    height: 0;
    width: 0;
    flex: 0 0 0;
  }
}
</style>
