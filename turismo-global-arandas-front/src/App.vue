<template>
  <div>
    <Navbar @toggleSidebar="toggleSidebar" />
    <div class="container-fluid">
      <div class="row">
          <Sidebar :showSidebar="showSidebar" />
          <main class="col">
            <!-- Contenido principal -->
            <h1>Bienvenido a mi sitio</h1>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
          </main>
      </div>
    </div>
  </div>
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

    const toggleSidebar = () => {
      showSidebar.value = !showSidebar.value
    }

    return {
      showSidebar,
      toggleSidebar
    }
  }
}
</script>

<style>
.container-fluid {
  padding-top: 0px;
}
html {
  background-color: #f8f7fa;
}
body {
  background-color: #f8f7fa;
}
.flex-grow {
  flex-grow: 1;
}
</style>
