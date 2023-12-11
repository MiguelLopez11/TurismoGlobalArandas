<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdatePaymentMethod">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del concepto"
                size="large"
                v-model="paymentMethod.name"
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
                $router.push('/MetodosPago')
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-card>
</template>

<script>
import PaymentMethodsServices from '@/Services/PaymentMethods.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getPaymentMethod, updatePaymentMethod } = PaymentMethodsServices()
    const paymentMethod = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getPaymentMethod(router.params.PaymentMethodId, data => {
      paymentMethod.value = data
    })
    const onUpdatePaymentMethod = () => {
      updatePaymentMethod(paymentMethod.value, data => {
        swal
          .fire({
            title: 'Método de pago modificado correctamente',
            text: 'El metodo de pago se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/MetodosPago')
            }
          })
      })
    }
    const validateName = () => {
      if (!paymentMethod.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      paymentMethod,
      onUpdatePaymentMethod,
      validateName
    }
  }
}
</script>

<style></style>
