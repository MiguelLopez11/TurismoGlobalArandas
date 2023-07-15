<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Proveedor" width="80%" center>
    <Form :validation-schema="validationSchema" @submit="onSubmit">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del proveedor"
                size="large"
                v-bind="field"
                v-model="providerFields.name"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="email" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Email </label>
              </div>
              <el-input
                placeholder="Ingresa el email del proveedor"
                size="large"
                v-bind="field"
                v-model="providerFields.email"
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
                <label>Numero de telefono</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de telefono del proveedor"
                size="large"
                v-bind="field"
                v-model="providerFields.phoneNumber"
                :validate-event="false"
                :model-value="value"
                type="number"
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
import { Field, Form } from 'vee-validate'
import ProviderServices from '@/Services/Provider.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addProvider')
    const swal = inject('$swal')
    const { createProvider } = ProviderServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre'),
      email: yup
        .string()
        .required('Este campo es requerido')
        .email('Inserte un email válido')
        .label('Email'),
      phoneNumber: yup
        .string()
        .required('Este campo es requerido')
        .min(10)
        .label('Numero de telefono')
    })
    const providerFields = ref({
      providerId: 0,
      name: '',
      email: '',
      phoneNumber: '',
      isDeleted: false
    })
    const providerFieldsBlank = ref(JSON.parse(JSON.stringify(providerFields)))

    const onSubmit = () => {
      createProvider(providerFields.value, data => {
        swal.fire({
          title: '¡Nuevo proveedor registrado!',
          text: 'El proveedor se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        providerFields.value = JSON.parse(JSON.stringify(providerFieldsBlank))
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      providerFields
    }
  }
}
</script>

<style></style>
