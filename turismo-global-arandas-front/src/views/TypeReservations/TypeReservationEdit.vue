<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onupdateTypeReservation">
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
                v-model="typeReservation.name"
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
              v-model="typeReservation.description"
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
                $router.push('/TiposReservaciones')
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
import TypeReservationServices from '@/Services/TypeReservation.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getTypeReservation, updateTypeReservation } =
      TypeReservationServices()
    const typeReservation = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getTypeReservation(router.params.TypeReservationId, data => {
      typeReservation.value = data
    })
    const onupdateTypeReservation = () => {
      updateTypeReservation(typeReservation.value, data => {
        swal
          .fire({
            title: 'Tipo de reservación modificado correctamente',
            text: 'El Tipo de reservación se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/TiposReservaciones')
            }
          })
      })
    }
    const validateName = () => {
      if (!typeReservation.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      typeReservation,
      onupdateTypeReservation,
      validateName
    }
  }
}
</script>

<style></style>
