<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Destino" width="80%" center>
    <Form
      ref="DestinationFormRef"
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
                placeholder="Ingresa el nombre del destino"
                size="large"
                v-bind="field"
                v-model="destinationFields.name"
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
              placeholder="Ingresa una descripción del destino"
              size="large"
              v-model="destinationFields.description"
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
import { Field, Form } from 'vee-validate'
import DestinationServices from '@/Services/Destinations.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addDestination')
    const swal = inject('$swal')
    const DestinationFormRef = ref(null)
    const { createDestination } = DestinationServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const destinationFields = ref({
      destinationId: 0,
      name: null,
      description: null,
      isDeleted: false
    })
    const destinationFieldsBlank = ref(
      JSON.parse(JSON.stringify(destinationFields))
    )

    const onSubmit = () => {
      createDestination(destinationFields.value, data => {
        swal.fire({
          title: '¡Nuevo destino registrado!',
          text: 'El destino se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        destinationFields.value = JSON.parse(
          JSON.stringify(destinationFieldsBlank)
        )
        DestinationFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      destinationFields,
      DestinationFormRef
    }
  }
}
</script>

<style></style>
