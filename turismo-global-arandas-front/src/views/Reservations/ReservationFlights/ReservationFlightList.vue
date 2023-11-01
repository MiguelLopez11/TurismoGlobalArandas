<template>
  <reservation-flights-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar vuelo..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddReservationFlight = !isAddReservationFlight"
        >
          <i> registrar nuevo vuelo </i>
        </el-button>
      </el-col>
    </el-row>
    <el-row class="mt-3">
      <el-col :span="24">
        <div class="table-scroll">
          <EasyDataTable
            rows-per-page-message="registro por pagina"
            empty-message="No se en cuentran registros"
            table-class-name="customize-table"
            buttons-pagination
            header-text-direction="center"
            body-text-direction="center"
            border-cell
            theme-color="#7367F0"
            :rows-per-page="10"
            :loading="isloading"
            :headers="fields"
            :items="reservationFlights"
            :search-field="searchField"
            :search-value="searchValue"
          >
            <template #header-actions="header">
              {{ header.text }}
            </template>
            <template #item-actions="items">
              <el-dropdown>
                <span class="bi bi-three-dots-vertical"> </span>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item
                      @click="
                        () => {
                          $router.push({
                            name: 'Edit-ReservationFlight',
                            params: { FlightId: items.flightId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteReservationFlight(items.flightId)"
                      >Eliminar</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import reservationFlightservices from '@/Services/ReservationFlights.Services'
import ReservationFlightsAddNew from './ReservationFlightsAddNew.vue'
// import RoleAddNew from './RoleAddNew.vue'

export default {
  components: { ReservationFlightsAddNew },
  setup () {
    const { getReservationFlights, deleteReservationFlight } =
      reservationFlightservices()
    const reservationFlights = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const isAddReservationFlight = ref(false)
    provide('addReservationFlight', isAddReservationFlight)
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'travelDateStart', text: 'Fecha desde' },
      { value: 'travelDateEnd', text: 'Fecha hasta' },
      { value: 'dateSale', text: 'Fecha de venta' },
      { value: 'departureAirport', text: 'Aeropuerto de salida' },
      { value: 'arrivalAirport', text: 'Aeropuerto de llegada' },
      { value: 'confirmationKey', text: 'Clave de confirmación' },
      { value: 'customer.name', text: 'Cliente' },
      { value: 'paymentMethodAgency', text: 'Método de pago agencia' },
      { value: 'paymentMethodClient', text: 'Método de pago cliente' },
      { value: 'contactPhone', text: 'Telefono de contacto' },
      { value: 'actions', text: 'Acciones' }
    ])
    getReservationFlights(data => {
      reservationFlights.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getReservationFlights(data => {
        reservationFlights.value = data
        isloading.value = false
      })
    }
    watch(isAddReservationFlight, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteReservationFlight = reservationFlightId => {
      swal
        .fire({
          title: 'Estás a punto de cancelar un vuelo, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, cancelar vuelo',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteReservationFlight(reservationFlightId, data => {
              swal.fire({
                title: 'Vuelo Cancelado!',
                text: 'El vuelo ha sido cancelado satisfactoriamente .',
                icon: 'success'
              })
              refreshTable()
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      filter,
      perPage,
      currentPage,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,
      reservationFlights,
      isAddReservationFlight,
      refreshTable,
      onDeleteReservationFlight
    }
  }
}
</script>

<style></style>
