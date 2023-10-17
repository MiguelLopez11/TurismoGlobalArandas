<template>
  <el-card>
    <reservation-tours-wizard :ReservationTourId="reservationTourId" />
  </el-card>
</template>

<script>
import { useRoute } from 'vue-router'
import { ref } from 'vue'
import ReservationToursWizard from '../ReservationToursWizard/ReservationToursWizard.vue'
export default {
  components: {
    ReservationToursWizard
  },
  setup () {
    const router = useRoute()
    const reservationTourId = ref(parseInt(router.params.ReservationTourId))
    return {
      reservationTourId
    }
  }
}
</script>

<style></style>
