<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <el-card>
    <el-row class="unauthorized-container">
      <!-- Columna izquierda con la imagen -->
      <el-col :xs="24" :sm="12" :md="12" :lg="12" class="image-col">
        <img
          src="@/Images/unauthorized.png"
          alt="Unauthorized"
          class="unauthorized-image"
        />
      </el-col>

      <!-- Columna derecha con el mensaje -->
      <el-col :xs="24" :sm="12" :md="12" :lg="12" class="message-col">
        <el-card class="unauthorized-content">
          <h2>Acceso no autorizado</h2>
          <p>Lo siento, no tienes permisos para acceder a esta página.</p>
          <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="$router.push('/')"
        >
          <i> Regresar al inicio </i>
        </el-button>
        </el-card>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
export default {}
</script>

<style scoped>

.image-col {
  display: flex;
  justify-content: center;
  align-items: center;
}

.unauthorized-image {
  max-width: 100%;
  height: auto;
  border-radius: 8px;
}

.message-col {
  display: flex;
  justify-content: center;
  align-items: center;
}

.unauthorized-content {
  width: 80%; /* Ajusta el ancho del card según tus necesidades */
  text-align: center;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.unauthorized-content h2 {
  color: #6D64AD; /* Tomato color */
}

.unauthorized-content p {
  color: #85838D;
}

.unauthorized-content router-link {
  margin-top: 15px;
  display: inline-block;
  color: #3498db; /* Blue color */
  text-decoration: underline;
  cursor: pointer;
}

.unauthorized-content router-link:hover {
  color: #2580b3; /* Darker blue color on hover */
}
</style>
