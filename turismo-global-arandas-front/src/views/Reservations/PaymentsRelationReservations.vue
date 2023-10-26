<template>
  <el-card>
    <el-row :gutter="35">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-form-item>
          <div>
            <label> Monto total de reserva </label>
          </div>
          <el-input
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
import { ref, provide } from 'vue'
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
    const isAddedPayment = ref(false)
    provide('isAddedPayment', isAddedPayment)
    const {
      getPaymentsRelationByReservationHotel,
      getPaymentsRelationByReservationTour,
      getPaymentsRelationByReservationVehicle
    } = PaymentsRelationReservationServices()
    if (router.params.ReservationHotelId) {
      getPaymentsRelationByReservationHotel(router.params.ReservationHotelId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
        }
      )
    } else if (router.params.ReservationTourId) {
      getPaymentsRelationByReservationTour(router.params.ReservationTourId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
        }
      )
    } else if (router.params.ReservationVehicleId) {
      getPaymentsRelationByReservationVehicle(router.params.ReservationVehicleId, data => {
        paymentsRelation.value = data
        store.commit('setPaymentReservationId', data.paymentReservationId)
      })
    }

    return {
      paymentsRelation,
      paymentsRelationFormRef
    }
  }
}
</script>

<style></style>
