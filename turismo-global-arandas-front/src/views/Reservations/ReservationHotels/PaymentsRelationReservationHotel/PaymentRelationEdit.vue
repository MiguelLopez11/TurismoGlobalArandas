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
                @input="calculateAmountReturned()"
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
              disabled
            />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <div>
              <label> Metodo de pago del cliente </label>
            </div>
            <v-select
              v-model="payment.paymentMethodId"
              class="w-100"
              label="name"
              :options="paymentMethods"
              :reduce="method => method.paymentMethodId"
            ></v-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <div>
              <label> Detalles de pago </label>
            </div>
            <el-input
              placeholder="Ingresa detalles del pago"
              size="large"
              v-model="payment.detailsPayment"
              type="textarea"
              :autosize="{ minRows: 4, maxRows: 8 }"
            />
          </el-form-item>
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
            :disabled="
              payment.amount > paymentAmountMissing ||
              payment.amountReceivedClient < payment.amount ||
              paymentAmountMissing == 0
            "
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
import PaymentMethodsServices from '@/Services/PaymentMethods.Services'
import { useStore } from 'vuex'
import { ElMessage } from 'element-plus'

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
    const paymentMethods = ref([])
    const store = useStore()
    const swal = inject('$swal')
    const paymentAmountMissing = ref(
      parseInt(store.getters.getPaymentAmountTotal)
    )
    const { getPaymentRelationList, updatePaymentRelationList } =
      PaymentsRelationListServices()
    const { getPaymentMethods } = PaymentMethodsServices()
    getPaymentMethods(data => {
      paymentMethods.value = data
    })
    watch(isOpenDialog, newValue => {
      if (newValue) {
        getPaymentRelationList(props.PaymentId, data => {
          payment.value = data
        })
      }
    })

    const onUpdatePayment = () => {
      // paymentFields.value.paymentReservationId = paymentReservationId.value
      if (
        parseInt(payment.value.amount) > paymentAmountMissing.value ||
        payment.value.amountReceivedClient < payment.value.amount ||
        paymentAmountMissing.value - payment.value.amount < 0
      ) {
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
    const calculateAmountReturned = () => {
      if (payment.value.amountReceivedClient - payment.value.amount < 0) {
        ElMessage({
          showClose: true,
          message:
            'Los datos ingresados no son correctos, rectifique e intente de nuevo.',
          type: 'error'
        })
      } else {
        payment.value.amountReturnedClient =
          payment.value.amountReceivedClient - payment.value.amount
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
      paymentMethods,
      paymentAmountMissing,
      onUpdatePayment,
      calculateAmountReturned,
      validateAmount,
      ValidateAmountReceivedClient,
      ValidatePaymentMethodClient
    }
  }
}
</script>

<style></style>
