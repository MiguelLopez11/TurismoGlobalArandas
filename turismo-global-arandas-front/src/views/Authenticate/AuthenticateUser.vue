<template>
  <div class="container">
    <el-row justify="start" :gutter="5">
      <el-col :ofset="5" :xs="20" :sm="24" :md="24" :lg="24" :xl="24">
        <el-card class="form-card" shadow="always">
          <template #header>
            <el-row justify="center">
              <el-image
                style="
                  width: 300px;
                  height: 300px;
                  border-radius: 20%;
                  margin-bottom: 30px;
                "
                src="https://cdn-icons-png.flaticon.com/512/826/826070.png"
                fit="fill"
              />
            </el-row>
            <el-row justify="center">
              <span>¡Bienvenido a Turismo Global Arandas!</span>
            </el-row>
            <el-row justify="center">
              <span>
                Por favor ingrese su usuario y contraseña para iniciar sesion.
              </span>
            </el-row>
          </template>
          <el-form label-position="top" label-width="150px" size="large">
            <el-form-item label="Usuario">
              <el-input />
            </el-form-item>
            <el-form-item label="Contraseña">
              <el-input />
            </el-form-item>
            <el-form-item>
              <el-button size="large" class="w-100" type="primary"
                >Iniciar sesion</el-button
              >
            </el-form-item>
          </el-form>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { ref } from 'vue'
import AuthenticationServices from '@/Services/Authentication.Services'
export default {
  setup () {
    const user = ref({
      userName: '',
      password: ''
    })
    const { LogIn } = AuthenticationServices()

    const AuthenticateUser = () => {
      LogIn(user.value, data => {
        console.log(data)
      })
    }
    return {
      user,
      AuthenticateUser
    }
  }
}
</script>

<style scoped>
.form-card {
  width: 150%;
}
.container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 98vh;
  background-image: url('https://scontent-hou1-1.xx.fbcdn.net/v/t39.30808-6/305960510_498834395582315_2152758621421830000_n.jpg?_nc_cat=109&cb=99be929b-3346023f&ccb=1-7&_nc_sid=09cbfe&_nc_eui2=AeFYc2Rmu8jvhXOkhVTYG54USdOF4FJuDVdJ04XgUm4NV9Zs9DWgnl1yfLGz8wxqzQUlVG2ZEmyDctXXSv_ldZFQ&_nc_ohc=P4v6-0RKKQEAX86seDb&_nc_ht=scontent-hou1-1.xx&oh=00_AfCYeFOTVJv0U4RQz5bumbmw2mgu4kgPY_TxECAI5C35BA&oe=64A3B2E0');
  background-size: cover;
  background-position: center;
}
</style>
