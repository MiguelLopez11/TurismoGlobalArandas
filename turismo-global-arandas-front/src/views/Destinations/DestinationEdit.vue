<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateProvider">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-model="destination.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
            <el-form-item>
              <div>
                <label> Descripción </label>
              </div>
              <el-input
                placeholder="Ingresa una descripción del destino"
                size="large"
                v-model="destination.description"
              />
            </el-form-item>
        </el-col>
      </el-row>
      <el-divider />
      <el-row :gutter="25" justify="end">
        <el-col :span="3">
          <el-button
            color="#7367F0"
            class="w-100"
            native-type="submit"
            size="large"
            >Guardar</el-button
          >
        </el-col>
        <el-col :span="3">
          <el-button
            class="w-100"
            color="#F1F1F2"
            size="large"
            @click="
              () => {
                $router.push('/Destinos')
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-card>
</template>

<script>
import DestinationServices from '@/Services/Destination.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getDestination, updateDestination } = DestinationServices()
    const destination = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getDestination(router.params.DestinationId, data => {
      destination.value = data
    })
    const onUpdateProvider = () => {
      updateDestination(destination.value, data => {
        swal
          .fire({
            title: 'Destino modificado correctamente',
            text: 'El destino se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Destinos')
            }
          })
      })
    }
    const validateName = () => {
      if (!destination.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      destination,
      onUpdateProvider,
      validateName
    }
  }
}
</script>

<style></style>
