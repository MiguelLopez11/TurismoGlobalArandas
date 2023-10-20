<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdatePaymentConcept">
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
                v-model="paymentConcept.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
            <el-form-item>
              <div>
                <label> Descripción </label>
              </div>
              <el-input
                placeholder="Ingresa una descripción del concepto"
                size="large"
                v-model="paymentConcept.description"
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
                $router.push('/ConceptosPago')
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
import PaymentConceptsServices from '@/Services/PaymentConcepts.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getPaymentConcept, updatePaymentConcept } = PaymentConceptsServices()
    const paymentConcept = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getPaymentConcept(router.params.PaymentConceptId, data => {
      paymentConcept.value = data
    })
    const onUpdatePaymentConcept = () => {
      updatePaymentConcept(paymentConcept.value, data => {
        swal
          .fire({
            title: 'Concepto de pago modificado correctamente',
            text: 'El concepto de pago se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ConceptosPago')
            }
          })
      })
    }
    const validateName = () => {
      if (!paymentConcept.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      paymentConcept,
      onUpdatePaymentConcept,
      validateName
    }
  }
}
</script>

<style></style>
