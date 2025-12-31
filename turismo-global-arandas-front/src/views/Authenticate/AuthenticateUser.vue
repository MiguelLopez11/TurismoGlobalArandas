<template>
  <div class="login-page">

    <div class="glass-card animate-fade">
      <div class="header">
        <img
          src="https://cdn-icons-png.flaticon.com/512/201/201623.png"
          class="logo"
        />
        <h2>Turismo Global Arandas</h2>
        <p>Viaja sin límites ✈️</p>
      </div>

      <Form
        ref="AuthenticateFormRef"
        as="el-form"
        :validation-schema="validationSchema"
        @submit="onAuthenticate"
      >
        <el-form-item prop="username">
          <Field name="username" v-slot="{ field, errorMessage }">
            <el-input
              v-bind="field"
              size="large"
              placeholder="Usuario"
              prefix-icon="UserFilled"
            />
            <small class="error">{{ errorMessage }}</small>
          </Field>
        </el-form-item>
        <el-form-item prop="password">
          <Field name="password" v-slot="{ field, errorMessage }">
            <el-input
              v-bind="field"
              size="large"
              type="password"
              show-password
              placeholder="Contraseña"
              prefix-icon="Lock"
            />
            <small class="error">{{ errorMessage }}</small>
          </Field>
        </el-form-item>

        <el-button
          class="login-button"
          size="large"
          color="#2563eb"
          native-type="submit"
          block
        >
          <el-icon v-if="!loading"><User /></el-icon>
          <div v-else class="loader"></div>
          {{ loading ? "Cargando..." : "Iniciar sesión" }}
        </el-button>
      </Form>
    </div>

    <!-- IMAGEN COMPLETA DERECHA -->
    <div class="image-side animate-slide">
      <transition-group name="fade-carousel" tag="div">
        <img
          v-for="(img, i) in images"
          :key="img + i"
          v-show="currentImage === i"
          :src="img"
          class="bg-img"
          alt=""
        />
      </transition-group>

      <div class="travel-text">
        <h1>Explora el mundo con nosotros 🌍</h1>
        <p>Descubre destinos, aventuras y experiencias únicas.</p>
      </div>
    </div>

  </div>
</template>

<script setup>
import { ref, inject } from 'vue'
import { User } from '@element-plus/icons-vue'
import { useRouter } from 'vue-router'
import AuthenticationServices from '@/Services/Authentication.Services'
import * as yup from 'yup'

// ALERTAS
const swal = inject('$swal')
const router = useRouter()

// SERVICIO
const { LogIn } = AuthenticationServices()

const AuthenticateFormRef = ref(null)

// STATE FORMULARIO
const user = ref({
  username: '',
  password: ''
})

// VALIDACIÓN (manteniendo tus reglas)
const validationSchema = yup.object({
  username: yup.string().required('Este campo es requerido'),
  password: yup
    .string()
    .required('Este campo es requerido')
    .min(8, 'Mínimo 8 caracteres')
    .matches(/[a-z]/, 'Debe contener una letra minúscula')
    .matches(/[A-Z]/, 'Debe contener una letra mayúscula')
    .matches(/[@$!%*#?&]/, 'Debe contener un carácter especial')
    .matches(/\d/, 'Debe contener un número')
})

// SUBMIT LOGIN
const onAuthenticate = () => {
  LogIn(user.value, (data) => {
    if (data.status === 200) {
      swal
        .fire({
          title: 'Inicio de sesión correcto',
          text: `Bienvenido ${data.userName}.`,
          icon: 'success'
        })
        .then(() => {
          sessionStorage.setItem('Token', data.token)
          sessionStorage.setItem('User', data.userName)
          sessionStorage.setItem('EmployeeId', data.employeeId)
          sessionStorage.setItem('Role', data.role.toString())
          router.push('/')
        })
    } else {
      swal.fire({
        title: 'Error al iniciar sesión',
        text: 'Usuario o contraseña incorrectos.',
        icon: 'error'
      })
    }
    AuthenticateFormRef.value.resetForm()
  })
}

// IMÁGENES DEL CARRUSEL
const images = [
  'https://images.unsplash.com/photo-1518684079-3c830dcef090?w=1200',
  'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=1200',
  'https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?w=1200'
]

const currentImage = ref(0)

// ROTACIÓN AUTOMÁTICA
setInterval(() => {
  currentImage.value = (currentImage.value + 1) % images.length
}, 4500)

const loading = ref(false)

</script>

<style scoped>
/* --- GENERAL PAGE --- */
.login-page {
  display: grid;
  grid-template-columns: 40% 60%;
  min-height: 100vh;
  background: linear-gradient(135deg, #1e3a8a, #3b82f6);
  position: relative;
  overflow: hidden;
}

/* --- GLASS CARD --- */
.glass-card {
  align-self: center;
  margin: auto;
  width: 85%;
  max-width: 420px;
  padding: 40px;
  border-radius: 22px;
  backdrop-filter: blur(18px);
  background: rgba(255, 255, 255, 0.27);
  box-shadow: 0 8px 32px rgba(0, 0, 0, 0.2);
  border: 1px solid rgba(255, 255, 255, 0.3);
  animation: fadeIn 1s ease forwards;
}

/* --- HEADER --- */
.header {
  text-align: center;
  margin-bottom: 30px;
}

.logo {
  width: 60px;
  margin-bottom: 10px;
}

.header h2 {
  margin: 0;
  font-size: 26px;
  color: #fff;
  font-weight: 600;
}

.header p {
  margin: 0;
  color: #e0e7ff;
}

/* --- INPUTS --- */
.error {
  color: #fee2e2;
  font-size: 12px;
}

/* --- BUTTON --- */
.login-button {
  margin-top: 10px;
  position: relative;
  display: flex !important;
  align-items: center;
  justify-content: center;
  gap: 10px;
}

/* Loader circular */
.loader {
  width: 18px;
  height: 18px;
  border: 3px solid rgba(255, 255, 255, 0.4);
  border-top-color: white;
  border-radius: 50%;
  animation: spin 0.9s linear infinite;
}

/* --- RIGHT IMAGE SIDE --- */
.image-side {
  position: relative;
  overflow: hidden;
}

.bg-img {
  position: absolute;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

/* TEXT OVER IMAGE */
.travel-text {
  position: absolute;
  bottom: 40px;
  left: 40px;
  color: white;
  max-width: 450px;
}

.travel-text h1 {
  font-size: 40px;
  margin: 0 0 10px;
  text-shadow: 0 4px 15px rgba(0, 0, 0, 0.4);
}

.travel-text p {
  font-size: 18px;
  opacity: 0.9;
}

/* --- ANIMATION --- */
@keyframes spin {
  to { transform: rotate(360deg); }
}

.fade-carousel-enter-active,
.fade-carousel-leave-active {
  transition: opacity 1s ease;
}

.fade-carousel-enter-from,
.fade-carousel-leave-to {
  opacity: 0;
}

.animate-fade {
  animation: fadeIn 1.1s ease forwards;
}

.animate-slide {
  animation: slideIn 1.2s ease forwards;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(20px); }
  to { opacity: 1; transform: translateY(0); }
}

@keyframes slideIn {
  from { opacity: 0; transform: translateX(60px); }
  to { opacity: 1; transform: translateX(0); }
}

/* --- RESPONSIVE --- */
@media (max-width: 900px) {
  .login-page {
    grid-template-columns: 1fr;
  }
  .image-side {
    height: 260px;
  }
}
</style>
