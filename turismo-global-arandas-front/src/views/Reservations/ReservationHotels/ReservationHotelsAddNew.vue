<template>
  <el-card>
    <reservation-hotels-wizard :reservationHotelId="0"/>
  </el-card>
</template>

<script>
import ReservationHotelsWizard from '../ReservationHotelsWizard/ReservationHotelsWizard.vue'
export default {
  components: {
    ReservationHotelsWizard
  }

}
</script>

<style>

</style>
