<template>
  <el-dialog v-model="isOpenDialog" title="Editar pago" width="80%" center>
    <Form v-slot="{ errors }" @submit="onUpdateHabitation">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="invoice" :rules="validateInvoice" as="text">
            <el-form-item :error="errors.invoice" required>
              <div>
                <label> Folio </label>
              </div>
              <el-input
                placeholder="Ingresa el Folio de la habitación"
                size="large"
                v-model="payment.invoice"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="amount" :rules="validateAmount" as="text">
            <el-form-item :error="errors.amount" required>
              <div>
                <label> Monto </label>
              </div>
              <el-input
                placeholder="Ingresa el tipo de habitación"
                size="large"
                v-model="payment.amount"
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
              placeholder="Ingresa el monto del anticipo"
              size="large"
              v-model="payment.observations"
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
import { ref, inject, watch } from 'vue'
import PaymentsRelationListServices from '@/Services/PaymentRelationList.Services'

export default {
  props: {
    PaymentId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const isOpenDialog = inject('editPaymentRelation')
    // const swal = inject('$swal')
    const payment = ref([])
    const { getPaymentRelationList, updatePaymentRelationList } =
      PaymentsRelationListServices()
    watch(isOpenDialog, newValue => {
      if (newValue) {
        getPaymentRelationList(props.PaymentId, data => {
          payment.value = data
        })
      }
    })

    const onUpdateHabitation = () => {
      updatePaymentRelationList(payment.value, data => {
        isOpenDialog.value = false
      })
    }
    const validateInvoice = () => {
      if (!payment.value.invoice) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAmount = () => {
      if (!payment.value.amount) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      isOpenDialog,
      payment,
      onUpdateHabitation,
      validateInvoice,
      validateAmount
    }
  }
}
</script>

<style></style>
