<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <el-autocomplete
    v-model="query"
    :fetch-suggestions="querySearchAsync"
    placeholder="Ingresa tu búsqueda"
    @select="handleSelect"
    size="large"
    class="w-100"
  >
    <template #default="{ item }">
      <label>
        {{
          'Tipo de reserva: ' +
          item.origin +
          ', ' +
          'Folio: ' +
          item.invoice +
          ', ' +
          'Cliente: ' +
          item.name +
          item.lastName +
          ' ' +
          'Numero de tel.: ' +
          item.phoneNumber +
          ' ' +
          'Fecha venta: ' +
          item.dateSale
        }}
      </label>
      <!-- Ajusta para que coincida con la propiedad real de tus datos -->
    </template>
  </el-autocomplete>
</template>

<script>
import HomeServices from '@/Services/Home.Services'
import { useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getAllReservations } = HomeServices()
    const swal = inject('$swal')
    const query = ref('')
    const suggestions = ref([]) // Cambiado de {} a []
    const route = useRouter()
    getAllReservations(data => {
      suggestions.value = data
    })

    const querySearchAsync = (queryString, cb) => {
      const results = queryString
        ? suggestions.value.filter(
          item =>
            item.name.toLowerCase().includes(queryString.toLowerCase()) ||
              item.lastName.toLowerCase().includes(queryString.toLowerCase()) ||
              item.phoneNumber
                .toLowerCase()
                .includes(queryString.toLowerCase()) ||
              item.invoice.toLowerCase().includes(queryString.toLowerCase())
        )
        : suggestions.value

      // Simulamos una demora para mostrar el efecto de búsqueda asíncrona
      setTimeout(() => {
        cb(results)
      }, 300)
    }

    const handleSelect = item => {
      console.log(item.origin)
      switch (true) {
        case item.origin.includes('Reservación hoteleria'):
          route.push({
            name: 'PaymentsRelationReservatioHotel',
            params: { ReservationHotelId: item.reservationId }
          })
          break
        case item.origin === 'Reservación Tours':
          route.push({
            name: 'PaymentsRelationReservatioTour',
            params: { ReservationTourId: item.reservationId }
          })
          break
        case item.origin === 'Reservación Vuelos':
          route.push({
            name: 'PaymentsRelationReservatioFlight',
            params: { ReservationFlightId: item.reservationId }
          })
          break
        case item.origin === 'Reservación Vehiculos':
          route.push({
            name: 'PaymentsRelationReservatioVehicle',
            params: { ReservationVehicleId: item.reservationId }
          })
          break
        default:
          swal.fire({
            title: 'Error inesperado',
            text: 'Quien sabe que paso alv .',
            icon: 'error'
          })
      }
    }

    return {
      query,
      querySearchAsync,
      handleSelect
    }
  }
}
</script>
