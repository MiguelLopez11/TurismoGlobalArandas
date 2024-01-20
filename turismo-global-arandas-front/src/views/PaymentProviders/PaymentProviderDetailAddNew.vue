<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo pago" width="80%" center>
    <Form
      ref="paymentFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="amount" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage">
              <div>
                <label> Monto </label>
              </div>
              <el-input
                placeholder="Ingresa el monto del pago"
                size="large"
                v-bind="field"
                v-model="paymentFields.amount"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <!-- <el-col :span="8">
          <el-form-item>
            <div>
              <label> Cantidad que recibe </label>
            </div>
            <el-input
              placeholder="Ingresa el la cantidad con la que paga el cliente"
              size="large"
              v-model="paymentFields.amountReceivedClient"
              @change="calculateAmountReturned()"
            />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <div>
              <label> Cantidad cambio al cliente </label>
            </div>
            <el-input
              placeholder="Ingresa el la cantidad de cambio que recibe el cliente"
              size="large"
              v-model="paymentFields.amountReturnedClient"
              disabled
              :min="1"
            />
          </el-form-item>
        </el-col> -->
        <el-col :span="8">
          <el-form-item>
            <div>
              <label> Metodo de pago </label>
            </div>
            <v-select
              v-model="paymentFields.paymentMethodId"
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
              <label> Concepto de pago </label>
            </div>
            <v-select
              v-model="paymentFields.paymentConceptId"
              class="w-100"
              label="name"
              :options="paymentConcepts"
              :reduce="concept => concept.paymentConceptId"
            ></v-select>
          </el-form-item>
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
            :disabled="
              paymentFields.amount > paymentProviderAmountMissing ||
              paymentProviderAmountMissing === 0
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
import PaymentProvidersServices from '@/Services/paymentProviders.Services'
import PaymentMethodsServices from '@/Services/PaymentMethods.Services'
import PaymentConceptsServices from '@/Services/PaymentConcepts.Services'
import * as yup from 'yup'
import { ref, inject } from 'vue'
import { useStore } from 'vuex'
import { ElMessage } from 'element-plus'
// import { useRouter } from 'vue-router'

export default {
  setup () {
    const isOpenDialog = inject('addPaymentProviderItem')
    const swal = inject('$swal')
    const store = useStore()
    const paymentFormRef = ref(null)
    const paymentMethods = ref([])
    const paymentConcepts = ref([])
    // const redirect = useRouter()
    const { createPaymentProviderList } = PaymentProvidersServices()
    const { getPaymentMethods } = PaymentMethodsServices()
    const { getPaymentConcepts } = PaymentConceptsServices()
    const paymentProviderAmountMissing = ref()
    const paymentProviderId = ref()
    getPaymentMethods(data => {
      paymentMethods.value = data
    })
    getPaymentConcepts(data => {
      paymentConcepts.value = data
    })
    const validationSchema = yup.object({
      amount: yup
        .number()
        .test('is-decimal', 'invalid decimal', value =>
          (value + '').match(/^\d+(\.\d+)?$/)
        )
        .required('Este campo es requerido')
    })
    setTimeout(() => {
      paymentProviderId.value = parseInt(store.getters.getPaymentProviderId)
      paymentProviderAmountMissing.value = parseInt(
        store.getters.getPaymentProviderAmountMissing
      )
    }, 1000)
    const paymentFields = ref({
      paymentId: 0,
      invoice: null,
      amount: null,
      paymentMethodId: null,
      paymentConceptId: null,
      paymentDate: null,
      paymentProviderId: null,
      observations: null,
      isDeleted: false
    })
    const paymentFieldsBlank = ref(JSON.parse(JSON.stringify(paymentFields)))

    const onSubmit = () => {
      paymentFields.value.paymentProviderId = paymentProviderId.value
      if (
        parseInt(paymentFields.value.amount) >
        paymentProviderAmountMissing.value
      ) {
        isOpenDialog.value = false
        paymentFields.value = JSON.parse(JSON.stringify(paymentFieldsBlank))
        paymentFormRef.value.resetForm()
        swal.fire({
          title: '¡Error al registrar pago!',
          text: 'El pago que se desea registrar, es mayor al monto total de la reservación',
          icon: 'error'
        })
      } else {
        if (
          paymentFields.value.amountReceivedClient < paymentFields.value.amount
        ) {
          isOpenDialog.value = false
          paymentFields.value = JSON.parse(JSON.stringify(paymentFieldsBlank))
          paymentFormRef.value.resetForm()
          swal.fire({
            title: '¡Error al registrar pago!',
            text: 'El pago que se desea registrar, Contiene datos erroneos',
            icon: 'error'
          })
        } else {
          createPaymentProviderList(paymentFields.value, data => {
            swal.fire({
              title: '¡Nuevo pago registrado!',
              text: 'El pago se ha registrado correctamente',
              icon: 'success'
            })
          })
          isOpenDialog.value = false
          paymentFields.value = JSON.parse(JSON.stringify(paymentFieldsBlank))
          paymentFormRef.value.resetForm()
        }
      }
    }
    const calculateAmountReturned = () => {
      if (
        paymentFields.value.amountReceivedClient - paymentFields.value.amount <
        0
      ) {
        ElMessage({
          showClose: true,
          message:
            'Los datos ingresados no son correctos, rectifique e intente de nuevo.',
          type: 'error'
        })
      } else {
        paymentFields.value.amountReturnedClient =
          paymentFields.value.amountReceivedClient - paymentFields.value.amount
      }
    }
    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      paymentFields,
      paymentMethods,
      paymentConcepts,
      paymentProviderAmountMissing,
      paymentFormRef,
      calculateAmountReturned
    }
  }
}
</script>

<style></style>
