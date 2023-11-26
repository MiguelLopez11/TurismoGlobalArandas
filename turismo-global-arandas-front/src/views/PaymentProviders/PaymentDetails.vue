<template>
  <el-card>
    <el-row :gutter="25">
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Folio</label>
          </div>
          <el-input
            placeholder="Folio"
            size="large"
            v-model="payment.invoice"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Fecha de reservación</label>
          </div>
          <el-date-picker
            v-model="payment.paymentDate"
            class="w-100"
            type="date"
            placeholder="Selecciona la fecha del viaje"
            size="large"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Observaciones</label>
          </div>
          <el-input
            placeholder="Ingresa las observaciones de la habitación"
            v-model="payment.observations"
            size="large"
            type="textarea"
            :autosize="{ minRows: 4, maxRows: 8 }"
          />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <payment-provider-documents />
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import PaymentProviders from '@/Services/paymentProviders.Services'
import PaymentProviderDocuments from './PaymentProviderDocuments.vue'
import { useRoute } from 'vue-router'
import { ref } from 'vue'
import { useStore } from 'vuex'
export default {
  components: { PaymentProviderDocuments },
  setup () {
    const {
      getPaymentProvider,
      getPaymentProviderByReservationHotel,
      getPaymentProviderByReservationTour
    } = PaymentProviders()
    const router = useRoute()
    const store = useStore()
    const payment = ref([])
    const onPaymentProvider = () => {
      getPaymentProvider(router.params.PaymentProviderId, data => {
        payment.value = data
        store.commit('setPaymentProviderId', data.paymentId)
      })
    }
    const onPaymentProviderByReservationHotel = () => {
      getPaymentProviderByReservationHotel(
        router.params.ReservationHotelId,
        data => {
          payment.value = data
          store.commit('setPaymentProviderId', data.paymentId)
        }
      )
    }
    const onPaymentProviderByReservationTour = () => {
      getPaymentProviderByReservationTour(
        router.params.ReservationTourId,
        data => {
          payment.value = data
          store.commit('setPaymentProviderId', data.paymentId)
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
    }
    return {
      payment
    }
  }
}
</script>

<style></style>
