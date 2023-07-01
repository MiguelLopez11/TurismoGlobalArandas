<template>
  <div class="login-container">
    <el-row justify="center">
      <el-col :xs="23" :sm="10" :md="14" :lg="16" :xl="10">
        <el-card>
          <el-col :xs="15" :sm="10" :md="14" :lg="15" :xl="15">
            <el-row style="flex-wrap: nowrap" align-content="center">
              <el-col
                :xs="18"
                :sm="10"
                :md="14"
                :lg="15"
                :xl="15"
                style="display: flex"
              >
                <div class="login-image">
                  <img
                    src="https://demos.pixinvent.com/vuexy-vuejs-admin-template/demo-1/assets/auth-v2-login-illustration-light-d1fd488d.png"
                    alt="Imagen de login"
                  />
                </div>
              </el-col>
              <el-col :span="24">
                <div class="login-form">
                  <el-card>
                    <template #header>
                      <el-row justify="center">
                        <span>¡Bienvenido a Turismo Global Arandas!</span>
                      </el-row>
                      <el-row justify="center">
                        <span>
                          Por favor ingrese su usuario y contraseña para iniciar
                          sesion.
                        </span>
                      </el-row>
                    </template>
                    <el-form label-position="top">
                      <el-row justify="center" :gutter="24">
                        <el-col :span="24">
                          <Field name="username" rules="required" as="text">
                            <el-form-item label="Usuario" size="large">
                              <el-input v-model="user.username"></el-input>
                            </el-form-item>
                          </Field>
                        </el-col>
                        <el-col :span="24">
                          <Field name="password" rules="required" as="text">
                            <el-form-item label="Contraseña" size="large">
                              <el-input
                                type="password"
                                v-model="user.password"
                              ></el-input>
                            </el-form-item>
                          </Field>
                        </el-col>
                      </el-row>
                      <el-col :span="24">
                        <el-button
                          size="large"
                          color="#675ABA"
                          class="w-100"
                          @click="onAuthenticate()"
                        >
                          <el-icon><User /></el-icon>
                          Iniciar sesion
                        </el-button>
                      </el-col>
                    </el-form>
                  </el-card>
                </div>
              </el-col>
            </el-row>
          </el-col>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { ref, inject } from 'vue'
import { User } from '@element-plus/icons-vue'
import { useRouter } from 'vue-router'
import AuthenticationServices from '@/Services/Authentication.Services'
export default {
  components: {
    User
  },
  setup () {
    const swal = inject('$swal')
    const redirect = useRouter()
    const { LogIn } = AuthenticationServices()
    const user = ref({
      username: '',
      password: ''
    })

    const onAuthenticate = () => {
      console.log('holi')
      LogIn(user.value, data => {
        if (data.status === 200) {
          swal
            .fire({
              title: 'Inicio de sesión correcto',
              text: `Bienvenido ${data.userName} a UConnect.`,
              icon: 'success'
            })
            .then(result => {
              if (result.isConfirmed) {
                window.localStorage.setItem('Token', data.token)
                redirect.go('/')
              }
            })
        }
      })
    }

    return {
      user,
      onAuthenticate
    }
  }
}
</script>

<style scoped>
.login-container {
  height: 98vh;
  display: flex;
  align-items: center;
  justify-content: center;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-size: cover;
  background-position: center;
  z-index: -1;
}

.login-image img {
  max-width: 100%;
  height: 100%;
  background-color: #ffffff;
}

.login-form {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 4px;
}
</style>
