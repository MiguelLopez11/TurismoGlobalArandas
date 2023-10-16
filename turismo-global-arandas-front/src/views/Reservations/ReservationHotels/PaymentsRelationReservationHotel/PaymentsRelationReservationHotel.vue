<template>
  <el-card>
    <el-row :gutter="35">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
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
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
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
    <el-row>
      <el-col :span="24">
      <payment-relation-list v-model:PaymentReservationHotelId="reservationHotelId" />
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
        <el-button class="w-100" color="#F1F1F2" size="large"
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
import PaymentRelationList from './PaymentRelationList.vue'
import { useStore } from 'vuex'

export default {
  components: {
    PaymentRelationList
  },
  setup () {
    const paymentsRelationFormRef = ref(null)
    const store = useStore()
    const paymentsRelation = ref([])
    const router = useRoute()
    const reservationHotelId = ref(parseInt(router.params.ReservationHotelId))
    const paymentReservationHotelId = ref()
    const { getPaymentsRelationByReservationHotel } =
      PaymentsRelationReservationServices()
    getPaymentsRelationByReservationHotel(reservationHotelId.value, data => {
      paymentsRelation.value = data
      paymentReservationHotelId.value = data.paymentReservationHotelId
      store.commit('setPaymentReservationHotelId', data.paymentReservationHotelId)
    })
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
      reservationHotelId,
      paymentReservationHotelId,
      paymentsRelationFormRef,
      onSubmit
    }
  }
}
</script>

<style></style>
