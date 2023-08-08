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
                v-model="categoryReservation.name"
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
              v-model="categoryReservation.description"
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
                $router.push('/CategoriaReservaciones')
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
import CategoryReservationServices from '@/Services/CategoryReservation.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getCategoryReservation, updateCategoryReservation } =
      CategoryReservationServices()
    const categoryReservation = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getCategoryReservation(router.params.CategoryReservationId, data => {
      categoryReservation.value = data
    })
    const onUpdateProvider = () => {
      updateCategoryReservation(categoryReservation.value, data => {
        swal
          .fire({
            title: 'Categoría de reservación modificado correctamente',
            text: 'La categoría de reservación se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/CategoriaReservaciones')
            }
          })
      })
    }
    const validateName = () => {
      if (!categoryReservation.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      categoryReservation,
      onUpdateProvider,
      validateName
    }
  }
}
</script>

<style></style>
