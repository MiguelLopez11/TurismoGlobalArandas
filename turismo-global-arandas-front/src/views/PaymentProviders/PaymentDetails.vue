<template>
  <el-card>
    <el-row :gutter="25">
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Monto Total</label>
          </div>
          <el-input
            placeholder="Monto total"
            size="large"
            v-model="payment.amountTotal"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Monto Faltante</label>
          </div>
          <el-input
            placeholder="Monto Faltante"
            size="large"
            v-model="payment.amountMissing"
            disabled
          />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <payment-provider-detail-list />
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import PaymentProviders from '@/Services/paymentProviders.Services'
import PaymentProviderDetailList from './PaymentProviderDetailList.vue'
import { useRoute } from 'vue-router'
import { ref, watch, computed } from 'vue'
import { useStore } from 'vuex'
export default {
  components: { PaymentProviderDetailList },
  setup () {
    const {
      getPaymentProvider,
      getPaymentProviderByReservationHotel,
      getPaymentProviderByReservationTour,
      getPaymentProviderByReservationFlight,
      getPaymentProviderByReservationVehicle
    } = PaymentProviders()
    const router = useRoute()
    const store = useStore()
    const payment = ref([])
    const refreshPaymentProvider = computed(
      () => store.getters.getRefreshPaymentProvider
    )
    const onPaymentProvider = () => {
      getPaymentProvider(router.params.PaymentProviderId, data => {
        payment.value = data
        store.commit('setPaymentProviderId', data.paymentProviderId)
        store.commit('setPaymentProviderAmountMissing', data.amountMissing)
      })
    }
    const onPaymentProviderByReservationHotel = () => {
      getPaymentProviderByReservationHotel(
        router.params.ReservationHotelId,
        data => {
          payment.value = data
          store.commit('setPaymentProviderId', data.paymentProviderId)
          store.commit('setPaymentProviderAmountMissing', data.amountMissing)
        }
      )
    }
    const onPaymentProviderByReservationTour = () => {
      getPaymentProviderByReservationTour(
        router.params.ReservationTourId,
        data => {
          payment.value = data
          store.commit('setPaymentProviderId', data.paymentProviderId)
          store.commit('setPaymentProviderAmountMissing', data.amountMissing)
        }
      )
    }
    const onPaymentProviderByReservationFlight = () => {
      getPaymentProviderByReservationFlight(
        router.params.ReservationFlightId,
        data => {
          payment.value = data
          store.commit('setPaymentProviderId', data.paymentProviderId)
          store.commit('setPaymentProviderAmountMissing', data.amountMissing)
        }
      )
    }
    const onPaymentProviderByReservationVehicle = () => {
      getPaymentProviderByReservationVehicle(
        router.params.ReservationVehicleId,
        data => {
          payment.value = data
          store.commit('setPaymentProviderId', data.paymentProviderId)
          store.commit('setPaymentProviderAmountMissing', data.amountMissing)
        }
      )
    }
    // IF
    if (router.params.PaymentProviderId) {
      onPaymentProvider()
    } else if (router.params.ReservationHotelId) {
      onPaymentProviderByReservationHotel()
    } else if (router.params.ReservationTourId) {
      onPaymentProviderByReservationTour()
    } else if (router.params.ReservationFlightId) {
      onPaymentProviderByReservationFlight()
    } else if (router.params.ReservationVehicleId) {
      onPaymentProviderByReservationVehicle()
    }
    watch(refreshPaymentProvider, NewValue => {
      if (NewValue) {
        if (router.params.PaymentProviderId) {
          location.reload()
        } else if (router.params.ReservationHotelId) {
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
      payment
    }
  }
}
</script>

<style></style>
