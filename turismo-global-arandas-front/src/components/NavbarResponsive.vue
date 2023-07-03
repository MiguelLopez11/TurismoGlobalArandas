<template>
  <el-card>
    <div class="card-header">
      <el-button class="right-button" color="#7367f0" @click="closeSession">
        <i class="bi bi-box-arrow-in-right"></i
      ></el-button>
    </div>
  </el-card>
</template>

<script>
import {} from '@/router'
import { Tools } from '@element-plus/icons-vue'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
export default {
  components: {
    // eslint-disable-next-line vue/no-unused-components
    Tools
  },
  setup () {
    const icon = ref(Tools)
    const redirect = useRouter()
    const closeSession = () => {
      window.sessionStorage.removeItem('Token')
      redirect.go(0)
    }
    return {
      Tools,
      icon,
      closeSession
    }
  }
}
</script>

<style>
.flex-grow {
  flex-grow: 1;
}
.el-menu {
  border-bottom: none !important;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.right-button {
  margin-left: auto;
}
</style>
