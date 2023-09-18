<template>
  <el-card>
    <reservation-hotels-wizard :reservationHotelId="reservationHotelId" />
  </el-card>
</template>

<script>
import ReservationHotelsWizard from '../ReservationHotelsWizard/ReservationHotelsWizard.vue'
import { useRoute } from 'vue-router'
import { ref } from 'vue'
export default {
  components: {
    ReservationHotelsWizard
  },
  setup () {
    const router = useRoute()
    const reservationHotelId = ref(parseInt(router.params.ReservationHotelId))
    return {
      reservationHotelId
    }
  }
}
</script>

<style></style>
