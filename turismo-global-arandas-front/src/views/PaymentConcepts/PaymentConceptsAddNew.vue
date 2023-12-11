<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo concepto de pago" width="80%" center>
    <Form
      ref="paymentConceptFormRef"
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
                v-model="paymentConceptFields.name"
                :validate-event="false"
                :model-value="value"
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
import PaymentConceptsServices from '@/Services/PaymentConcepts.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addPaymentMethod')
    const swal = inject('$swal')
    const paymentConceptFormRef = ref(null)
    const { createPaymentConcept } = PaymentConceptsServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const paymentConceptFields = ref({
      paymentConceptId: 0,
      name: null,
      description: null,
      isDeleted: false
    })
    const paymentConceptFieldsBlank = ref(
      JSON.parse(JSON.stringify(paymentConceptFields))
    )

    const onSubmit = () => {
      createPaymentConcept(paymentConceptFields.value, data => {
        swal.fire({
          title: '¡Nuevo concepto de pago registrado!',
          text: 'El concepto de pago se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        paymentConceptFields.value = JSON.parse(
          JSON.stringify(paymentConceptFieldsBlank)
        )
        paymentConceptFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      paymentConceptFields,
      paymentConceptFormRef
    }
  }
}
</script>

<style></style>
