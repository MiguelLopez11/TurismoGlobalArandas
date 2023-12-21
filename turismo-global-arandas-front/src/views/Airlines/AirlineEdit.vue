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
                placeholder="Ingresa el nombre del destino"
                size="large"
                v-model="airline.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="phoneNumber" :rules="validatePhoneNumber" as="text">
            <el-form-item :error="errors.phoneNumber" required>
              <div>
                <label> Numero de telefono </label>
              </div>
              <el-input
                placeholder="Ingresa una descripción del destino"
                size="large"
                v-model="airline.phoneNumber"
              />
            </el-form-item>
          </Field>
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
                $router.push('/Aerolineas')
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
import AirlineServices from '@/Services/Airline.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getAirline, updateAirline } = AirlineServices()
    const airline = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getAirline(router.params.AirlineId, data => {
      airline.value = data
    })
    const onUpdateProvider = () => {
      updateAirline(airline.value, data => {
        swal
          .fire({
            title: 'Categoría de reservación modificado correctamente',
            text: 'La categoría de reservación se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Aerolineas')
            }
          })
      })
    }
    const validateName = () => {
      if (!airline.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePhoneNumber = () => {
      if (!airline.value.phoneNumber) {
        return 'Este campo es requerido'
      }
      if (airline.value.phoneNumber.length < 10) {
        return 'Ingresa un numero de telefono válido'
      }
      return true
    }
    return {
      airline,
      onUpdateProvider,
      validateName,
      validatePhoneNumber
    }
  }
}
</script>

<style></style>
