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
import { ref, computed, watch } from 'vue'
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
    const router = useRoute()
    const paymentsRelation = ref([])
    const {
      getPaymentsRelationByReservationHotel,
      getPaymentsRelationByReservationTour,
      getPaymentsRelationByReservationVehicle,
      getPaymentsRelationByReservationFlight
    } = PaymentsRelationReservationServices()
    const refreshPaymentRelation = computed(
      () => store.getters.getRefreshPaymentRelation
    )
    const onPaymentReservationHotels = () => {
      getPaymentsRelationByReservationHotel(
        router.params.ReservationHotelId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
          store.commit('setPaymentAmountTotal', data.amountMissing)
        }
      )
    }
    const onPaymentReservationVehicles = () => {
      getPaymentsRelationByReservationVehicle(
        router.params.ReservationVehicleId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
          store.commit('setPaymentAmountTotal', data.amountMissing)
        }
      )
    }
    const onPaymentReservationTours = () => {
      getPaymentsRelationByReservationTour(
        router.params.ReservationTourId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
          store.commit('setPaymentAmountTotal', data.amountMissing)
        }
      )
    }
    const onPaymentReservationFlights = () => {
      getPaymentsRelationByReservationFlight(
        router.params.ReservationFlightId,
        data => {
          paymentsRelation.value = data
          store.commit('setPaymentReservationId', data.paymentReservationId)
          store.commit('setPaymentAmountTotal', data.amountMissing)
        }
      )
    }
    if (router.params.ReservationHotelId) {
      onPaymentReservationHotels()
    } else if (router.params.ReservationTourId) {
      onPaymentReservationTours()
    } else if (router.params.ReservationVehicleId) {
      onPaymentReservationVehicles()
    } else if (router.params.ReservationFlightId) {
      onPaymentReservationFlights()
    }
    watch(refreshPaymentRelation, NewValue => {
      if (NewValue) {
        if (router.params.ReservationHotelId) {
          location.reload()
        } else if (router.params.ReservationTourId) {
          location.reload()
        } else if (router.params.ReservationVehicleId) {
          location.reload()
        } else if (router.params.ReservationFlightId) {
          location.reload()
        }
      }
    })
    return {
      paymentsRelation,
      paymentsRelationFormRef
    }
  }
}
</script>

<style></style>
