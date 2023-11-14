<template>
  <el-dialog v-model="isOpenDialog" title="Editar pago" width="80%" center>
    <Form v-slot="{ errors }" @submit="onUpdatePayment">
      <el-row :gutter="35">
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
          <Field
            name="amountReceivedClient"
            :rules="ValidateAmountReceivedClient"
          >
            <el-form-item :error="errors.amountReceivedClient">
              <div>
                <label> Cantidad que recibe </label>
              </div>
              <el-input
                placeholder="Ingresa el la cantidad con la que paga el cliente"
                size="large"
                v-model="payment.amountReceivedClient"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <div>
              <label> Cantidad cambio al cliente </label>
            </div>
            <el-input
              placeholder="Ingresa el la cantidad de cambio que recibe el cliente"
              size="large"
              v-model="payment.amountReturnedClient"
            />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <Field
            name="paymentMethodClient"
            :rules="ValidatePaymentMethodClient"
          >
            <el-form-item :error="errors.paymentMethodClient">
              <div>
                <label> Metodo de pago del cliente </label>
              </div>
              <el-input
                placeholder="Ingresa cual fue el método de pago del cliente"
                size="large"
                v-model="payment.paymentMethodClient"
                type="textarea"
                :autosize="{ minRows: 4, maxRows: 8 }"
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
import { useStore } from 'vuex'

export default {
  props: {
    PaymentId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const isOpenDialog = inject('editPaymentRelation')
    const payment = ref([])
    const store = useStore()
    const swal = inject('$swal')
    const paymentAmountMissing = ref(
      parseInt(store.getters.getPaymentAmountTotal)
    )
    const { getPaymentRelationList, updatePaymentRelationList } =
      PaymentsRelationListServices()
    watch(isOpenDialog, newValue => {
      if (newValue) {
        getPaymentRelationList(props.PaymentId, data => {
          payment.value = data
        })
      }
    })

    const onUpdatePayment = () => {
      if (paymentAmountMissing.value - payment.value.amount < 0) {
        isOpenDialog.value = false
        swal.fire({
          title: '¡Error al registrar pago!',
          text: 'El pago que se desea registrar, es mayor al monto total de la reservación',
          icon: 'error'
        })
      } else {
        updatePaymentRelationList(payment.value, data => {
          isOpenDialog.value = false
          store.commit('setRefreshPaymentRelation', true)
        })
      }
    }
    const validateAmount = () => {
      if (!payment.value.amount) {
        return 'Este campo es requerido'
      }
      return true
    }
    const ValidateAmountReceivedClient = () => {
      if (!payment.value.amountReceivedClient) {
        return 'Este campo es requerido'
      }
      return true
    }
    const ValidatePaymentMethodClient = () => {
      if (!payment.value.paymentMethodClient) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      isOpenDialog,
      payment,
      onUpdatePayment,
      validateAmount,
      ValidateAmountReceivedClient,
      ValidatePaymentMethodClient
    }
  }
}
</script>

<style></style>
