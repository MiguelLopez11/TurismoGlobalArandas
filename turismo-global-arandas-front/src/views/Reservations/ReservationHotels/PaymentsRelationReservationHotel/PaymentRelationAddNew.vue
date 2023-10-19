<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo pago" width="80%" center>
    <Form
      ref="paymentFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35" justify="center">
        <el-col :span="8">
          <Field name="invoice" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Folio </label>
              </div>
              <el-input
                placeholder="Ingresa el folio del pago"
                size="large"
                v-bind="field"
                v-model="paymentFields.invoice"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="amount" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Monto </label>
              </div>
              <el-input
                placeholder="Ingresa el folio del pago"
                size="large"
                v-bind="field"
                v-model="paymentFields.amount"
                :validate-event="false"
                :model-value="value"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
            <el-form-item>
              <div>
                <label> Observaciones </label>
              </div>
              <el-input
                placeholder="Ingresa una observacion del pago"
                size="large"
                v-model="paymentFields.observations"
                type="textarea"
                :autosize="{ minRows: 4, maxRows: 8 }"
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
import PaymentsRelationListServices from '@/Services/PaymentRelationList.Services'
import * as yup from 'yup'
import { useStore } from 'vuex'

export default {
  setup () {
    const isOpenDialog = inject('addPaymentRelation')
    const swal = inject('$swal')
    const store = useStore()
    const paymentFormRef = ref(null)
    const { createPaymentRelationList } = PaymentsRelationListServices()
    const validationSchema = yup.object({
      invoice: yup.string().required('Este campo es requerido').label('Nombre'),
      amount: yup.number().required('Este campo es requerido').label('Nombre')
    })
    const paymentReservationId = ref()
    setTimeout(() => {
      paymentReservationId.value = parseInt(
        store.getters.getPaymentReservationId
      )
    }, 1000)
    const paymentFields = ref({
      paymentId: 0,
      invoice: null,
      amount: null,
      paymentDate: null,
      observations: null,
      paymentReservationId: null,
      isDeleted: false
    })
    const paymentFieldsBlank = ref(JSON.parse(JSON.stringify(paymentFields)))

    const onSubmit = () => {
      paymentFields.value.paymentReservationId = paymentReservationId.value
      createPaymentRelationList(paymentFields.value, data => {
        swal.fire({
          title: '¡Nuevo pago registrado!',
          text: 'El pago se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        paymentFields.value = JSON.parse(JSON.stringify(paymentFieldsBlank))
        paymentFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      paymentFields,
      paymentFormRef
    }
  }
}
</script>

<style></style>
