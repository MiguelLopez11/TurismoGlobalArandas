<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Aerolinea" width="80%" center>
    <Form
      ref="AirlineFormRef"
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
                v-model="airlineFields.name"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="phoneNumber" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Numero de telefono </label>
              </div>
              <el-input
                placeholder="Ingresa una descripción de la categoria"
                size="large"
                v-model="airlineFields.phoneNumber"
                :validate-event="false"
                :model-value="value"
                v-bind="field"
                minlength="10"
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
import AirlineServices from '@/Services/Airline.Services'
import * as yup from 'yup'

export default {
  setup () {
    const isOpenDialog = inject('AddCategoryReservation')
    const swal = inject('$swal')
    const AirlineFormRef = ref(null)
    const { createAirline } = AirlineServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre'),
      phoneNumber: yup.number().required('Este campo es requerido')
    })
    const airlineFields = ref({
      airlineId: 0,
      name: null,
      phoneNumber: null,
      isDeleted: false
    })
    const airlineFieldsBlank = ref(JSON.parse(JSON.stringify(airlineFields)))

    const onSubmit = () => {
      createAirline(airlineFields.value, data => {
        swal.fire({
          title: '¡Nueva categoría de reservación registrado!',
          text: 'La categoría de reservación se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        airlineFields.value = JSON.parse(JSON.stringify(airlineFieldsBlank))
        AirlineFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      airlineFields,
      AirlineFormRef
    }
  }
}
</script>

<style></style>
