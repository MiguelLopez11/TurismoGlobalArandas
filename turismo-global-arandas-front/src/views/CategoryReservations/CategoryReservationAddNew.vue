<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo categoría de reservación" width="80%" center>
    <Form
      ref="CategoryReservationFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre de la categoría"
                size="large"
                v-bind="field"
                v-model="CategoryReservationFields.name"
                :validate-event="false"
                :model-value="value"
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
              placeholder="Ingresa una descripción de la categoria"
              size="large"
              v-model="CategoryReservationFields.description"
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
                isOpenDialog = !isOpenDialog
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-dialog>
</template>

<script>
import { ref, inject } from 'vue'
import CategoryReservationServices from '@/Services/CategoryReservation.Services'
import * as yup from 'yup'

export default {
  setup () {
    const isOpenDialog = inject('AddCategoryReservation')
    const swal = inject('$swal')
    const CategoryReservationFormRef = ref(null)
    const { createCategoryReservation } = CategoryReservationServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const CategoryReservationFields = ref({
      destinationId: 0,
      name: null,
      description: null,
      isDeleted: false
    })
    const destinationFieldsBlank = ref(
      JSON.parse(JSON.stringify(CategoryReservationFields))
    )

    const onSubmit = () => {
      createCategoryReservation(CategoryReservationFields.value, data => {
        swal.fire({
          title: '¡Nueva categoría de reservación registrado!',
          text: 'La categoría de reservación se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        CategoryReservationFields.value = JSON.parse(
          JSON.stringify(destinationFieldsBlank)
        )
        CategoryReservationFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      CategoryReservationFields,
      CategoryReservationFormRef
    }
  }
}
</script>

<style></style>
