<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo tipo de reservación" width="80%" center>
    <Form
      ref="TypeReservationFormRef"
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
                placeholder="Ingresa el nombre del tipo de reservación"
                size="large"
                v-bind="field"
                v-model="TypeReservationFields.name"
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
              placeholder="Ingresa una descripción del tipo de reservación"
              size="large"
              v-model="TypeReservationFields.description"
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
import TypeReservationServices from '@/Services/TypeReservation.Services'
import * as yup from 'yup'

export default {
  setup () {
    const isOpenDialog = inject('AddTypeReservation')
    const swal = inject('$swal')
    const TypeReservationFormRef = ref(null)
    const { createTypeReservation } = TypeReservationServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const TypeReservationFields = ref({
      typeReservationId: 0,
      name: null,
      description: null,
      isDeleted: false
    })
    const destinationFieldsBlank = ref(
      JSON.parse(JSON.stringify(TypeReservationFields))
    )

    const onSubmit = () => {
      createTypeReservation(TypeReservationFields.value, data => {
        swal.fire({
          title: '¡Nuevo tipo de reservación registrado!',
          text: 'El tipo de reservación se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        TypeReservationFields.value = JSON.parse(
          JSON.stringify(destinationFieldsBlank)
        )
        TypeReservationFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      TypeReservationFields,
      TypeReservationFormRef
    }
  }
}
</script>

<style></style>
