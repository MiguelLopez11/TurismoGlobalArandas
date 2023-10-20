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
            disabled
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
            disabled
          />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <payment-relation-list />
      </el-col>
    </el-row>
    <el-divider />
  </el-card>
</template>

<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import PaymentsRelationReservationServices from '@/Services/PaymentRelationReservationHotel.Services'
import PaymentRelationList from './ReservationHotels/PaymentsRelationReservationHotel/PaymentRelationList.vue'
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
    const {
      getPaymentsRelationByReservationHotel,
      getPaymentsRelationByReservationTour
    } = PaymentsRelationReservationServices()
    if (reservationHotelId.value) {
      getPaymentsRelationByReservationHotel(reservationHotelId.value, data => {
        paymentsRelation.value = data
        store.commit('setPaymentReservationId', data.paymentReservationId)
      })
    } else {
      getPaymentsRelationByReservationTour(
        router.params.ReservationTourId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
        }
      )
    }
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
      paymentsRelationFormRef,
      onSubmit
    }
  }
}
</script>

<style></style>
