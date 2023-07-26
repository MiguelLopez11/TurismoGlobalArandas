<template>
  <el-row align="middle" justify="center">
    <el-col :span="12">
      <el-card>
        <el-row align="middle" :gutter="25" justify="center">
          <el-col :xs="8" :sm="10" :md="12" :lg="12" :xl="12">
            <div class="login-image">
              <img
                src="https://demos.pixinvent.com/vuexy-vuejs-admin-template/demo-1/assets/auth-v2-login-illustration-light-d1fd488d.png"
                alt="Imagen de login"
              />
            </div>
          </el-col>
          <el-col :xs="15" :sm="14" :md="12" :lg="12" :xl="12">
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
                <Form
                  ref="AuthenticateFormRef"
                  as="el-form"
                  :validation-schema="validationSchema"
                  @submit="onAuthenticate"
                >
                  <el-row justify="center" :gutter="24">
                    <el-col :span="24">
                      <Field
                        name="username"
                        v-slot="{ value, field, errorMessage }"
                        as="text"
                      >
                        <el-form-item :error="errorMessage" size="large">
                          <div>
                            <label> Usuario </label>
                          </div>
                          <el-input
                            placeholder="Ingresa el nombre de usuario"
                            size="large"
                            v-bind="field"
                            v-model="user.username"
                            :validate-event="false"
                            :model-value="value"
                          />
                        </el-form-item>
                      </Field>
                    </el-col>
                    <el-col :span="24">
                      <Field
                        name="password"
                        v-slot="{ value, field, errorMessage }"
                        as="text"
                      >
                        <el-form-item :error="errorMessage" size="large">
                          <div>
                            <label> Contraseña </label>
                          </div>
                          <el-input
                            placeholder="Ingresa el nombre de usuario"
                            size="large"
                            v-bind="field"
                            v-model="user.password"
                            :validate-event="false"
                            :model-value="value"
                            type="password"
                            show-password
                          />
                        </el-form-item>
                      </Field>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="24">
                      <el-button
                        size="large"
                        color="#675ABA"
                        class="w-100"
                        native-type="submit"
                      >
                        <el-icon><User /></el-icon>
                        Iniciar sesion
                      </el-button>
                    </el-col>
                  </el-row>
                </Form>
              </el-card>
            </div>
          </el-col>
        </el-row>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import { ref, inject } from 'vue'
import { User } from '@element-plus/icons-vue'
import { useRouter } from 'vue-router'
import AuthenticationServices from '@/Services/Authentication.Services'
import * as yup from 'yup'
export default {
  components: {
    User
  },
  setup () {
    const swal = inject('$swal')
    const redirect = useRouter()
    const { LogIn } = AuthenticationServices()
    const AuthenticateFormRef = ref(null)
    const user = ref({
      username: null,
      password: null
    })
    const validationSchema = yup.object({
      username: yup
        .string()
        .required('este campo es requerido')
        .label('Nombre de usuario'),
      password: yup
        .string()
        .required('este campo es requerido')
        .min(8, 'debe contener minimo 8 caracteres')
        .matches(/[a-z]+/, 'minimo una letra minusculo')
        .matches(/[A-Z]+/, 'minimo una letra mayuscula')
        .matches(/[@$!%*#?&]+/, 'minimo un caracter especial')
        .matches(/\d+/, 'minimo un numero')
        .label('Contraseña')
    })
    const onAuthenticate = () => {
      LogIn(user.value, data => {
        if (data.status === 200) {
          swal
            .fire({
              title: 'Inicio de sesión correcto',
              text: `Bienvenido ${data.userName}.`,
              icon: 'success'
            })
            .then(result => {
              if (result.isConfirmed) {
                window.sessionStorage.setItem('Token', data.token)
                window.sessionStorage.setItem('User', data.userName)
                redirect.go('/')
              }
            })
        } else {
          swal.fire({
            title: 'No fue posible iniciar sesión',
            text: 'Compruebe el usuario y contraseña sean correctos y vuelva a intentarlo.',
            icon: 'error'
          })
        }
        AuthenticateFormRef.value.resetForm()
      })
    }

    return {
      user,
      validationSchema,
      AuthenticateFormRef,
      onAuthenticate
    }
  }
}
</script>

<style>
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
