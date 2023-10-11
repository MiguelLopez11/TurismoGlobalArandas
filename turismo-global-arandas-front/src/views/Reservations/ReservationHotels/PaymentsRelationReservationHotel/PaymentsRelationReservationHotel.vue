<template>
  <el-card>
    <el-row :gutter="35" justify="center">
      <el-col :span="8">
        <el-form-item>
          <div>
            <label> Monto total de reserva </label>
          </div>
          <el-input
            placeholder="Ingresa el nombre del role"
            size="large"
            v-model="paymentsRelation.amountTotal"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label> Monto faltante de reserva </label>
          </div>
          <el-input
            placeholder="Ingresa el nombre del role"
            size="large"
            v-model="paymentsRelation.amountMissing"
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
          >Cancelar</el-button
        >
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import PaymentsRelationReservationServices from '@/Services/PaymentRelationReservationHotel.Services'

export default {
  setup () {
    const paymentsRelationFormRef = ref(null)
    const paymentsRelation = ref([])
    const router = useRoute()
    const { getPaymentsRelationByReservationHotel } =
      PaymentsRelationReservationServices()
    getPaymentsRelationByReservationHotel(
      router.params.ReservationHotelId,
      data => {
        paymentsRelation.value = data
      }
    )
    const onSubmit = () => {
      //   createRole(PaymentsRelationFields.value.Name, data => {
      //     swal.fire({
      //       title: '¡Nuevo role registrado!',
      //       text: 'El role se ha registrado correctamente',
      //       icon: 'success'
      //     })
      //     isOpenDialog.value = false
      //     PaymentsRelationFields.value = JSON.parse(
      //       JSON.stringify(PaymentsRelationFieldsBlank)
      //     )
      //     paymentsRelationFormRef.value.resetForm()
      //   })
    }

    return {
      paymentsRelation,
      paymentsRelationFormRef,
      onSubmit
    }
  }
}
</script>

<style></style>
