<template>
  <el-card>
    <div class="card-header">
      <el-dropdown class="right-button">
        <el-button class="w-100" size="large" circle>
          <template #icon>
            <i class="bi bi-person-circle"></i>
          </template>
        </el-button>
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item @click="closeSession">
              <span>
                <i class="bi bi-box-arrow-in-right"></i>
                Cerrar sesión
              </span>
            </el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
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
  align-items: flex-start;
}
.el-header {
  display: inline-table;
}
.right-button {
  margin-left: auto;
}
</style>
