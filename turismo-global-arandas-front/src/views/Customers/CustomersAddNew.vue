<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo cliente" width="80%" center>
    <Form
      ref="CustomerFormRef"
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
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-bind="field"
                v-model="customerFields.name"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="lastname" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Apellidos </label>
              </div>
              <el-input
                placeholder="Ingresa los apellidos del empleado"
                size="large"
                v-bind="field"
                v-model="customerFields.lastname"
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
                placeholder="Ingresa el numero de telefono del empleado"
                size="large"
                v-bind="field"
                v-model="customerFields.phoneNumber"
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
import CustomerServices from '@/Services/Customers.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addCustomer')
    const swal = inject('$swal')
    const { createCustomer } = CustomerServices()
    const CustomerFormRef = ref(null)
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre'),
      lastname: yup
        .string()
        .required('Este campo es requerido')
        .label('Apellidos'),
      phoneNumber: yup
        .string()
        .required('Este campo es requerido')
        .min(10)
        .label('Numero de telefono')
    })
    const customerFields = ref({
      customerId: 0,
      name: '',
      lastname: '',
      phoneNumber: '',
      isDeleted: false
    })
    const customerFieldsBlank = ref(JSON.parse(JSON.stringify(customerFields)))

    const onSubmit = () => {
      createCustomer(customerFields.value, data => {
        swal.fire({
          title: '¡Nuevo cliente registrado!',
          text: 'El cliente se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        customerFields.value = JSON.parse(JSON.stringify(customerFieldsBlank))
        CustomerFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      customerFields,
      CustomerFormRef
    }
  }
}
</script>

<style></style>
