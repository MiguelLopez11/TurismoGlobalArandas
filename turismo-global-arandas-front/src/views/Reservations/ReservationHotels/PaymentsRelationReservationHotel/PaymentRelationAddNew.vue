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
            <el-form-item :error="errorMessage">
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
import PaymentsRelationListServices from '@/Services/PaymentRelationList.Services'
import * as yup from 'yup'
import { ref, inject } from 'vue'
import { useStore } from 'vuex'
// import { useRouter } from 'vue-router'

export default {
  setup () {
    const isOpenDialog = inject('addPaymentRelation')
    const swal = inject('$swal')
    const store = useStore()
    const paymentFormRef = ref(null)
    // const redirect = useRouter()
    const { createPaymentRelationList } = PaymentsRelationListServices()
    const paymentAmountTotal = ref()
    const paymentReservationId = ref()
    const validationSchema = yup.object({
      invoice: yup.string().required('Este campo es requerido').label('Nombre'),
      amount: yup
        .number()
        .test('is-decimal', 'invalid decimal', value =>
          (value + '').match(/^\d+(\.\d+)?$/)
        )
        .required('Este campo es requerido')
        .label('Nombre')
    })
    setTimeout(() => {
      paymentReservationId.value = parseInt(
        store.getters.getPaymentReservationId
      )
      paymentAmountTotal.value = parseInt(store.getters.getPaymentAmountTotal)
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
      console.log(
        parseInt(paymentFields.value.amount),
        paymentAmountTotal.value
      )
      if (parseInt(paymentFields.value.amount) > paymentAmountTotal.value) {
        isOpenDialog.value = false
        paymentFields.value = JSON.parse(JSON.stringify(paymentFieldsBlank))
        paymentFormRef.value.resetForm()
        swal.fire({
          title: '¡Error al registrar pago!',
          text: 'El pago que se desea registrar, es mayor al monto total de la reservación',
          icon: 'error'
        })
      } else {
        createPaymentRelationList(paymentFields.value, data => {
          swal.fire({
            title: '¡Nuevo pago registrado!',
            text: 'El pago se ha registrado correctamente',
            icon: 'success'
          })
          isOpenDialog.value = false
          paymentFields.value = JSON.parse(JSON.stringify(paymentFieldsBlank))
          paymentFormRef.value.resetForm()
          // redirect.go(0)
        })
      }
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
