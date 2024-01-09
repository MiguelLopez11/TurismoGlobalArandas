<template>
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar vuelo..."
        />
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
            :filter-options="filterOptions"
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
                      @click="
                        $router.push({
                          name: 'PaymentsRelationReservatioFlight',
                          params: {
                            ReservationFlightId: items.flightId
                          }
                        })
                      "
                      >Relación de pagos</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        $router.push({
                          name: 'PaymentProvider-Details-ReservationFlight',
                          params: {
                            ReservationFlightId: items.flightId
                          }
                        })
                      "
                      >Relación de pagos a proveedores</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
            <template #item-customer="items">
              {{ items.customers.name }} {{ items.customers.lastname }}
            </template>
            <template #item-isDeleted="items">
              <el-tag
                effect="dark"
                class="ml-2"
                :type="items.isDeleted === false ? 'success' : 'danger'"
              >
                {{ items.isDeleted === false ? 'Activo' : 'Cancelado' }}
              </el-tag>
            </template>
            <template #header-isDeleted="header">
              <div class="filter-column">
                <img
                  src="@/Images/Filter-icon.jpg"
                  width="15"
                  height="15"
                  class="filter-icon"
                  @click.stop="
                    showStatusReservationFilter = !showStatusReservationFilter
                  "
                />
                {{ header.text }}
              </div>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
  <el-dialog
    v-model="showStatusReservationFilter"
    title="Filtrar reservaciones"
    width="50%"
    center
  >
    <el-row :gutter="35" align="middle">
      <el-col :lg="8" :md="8">
        <v-select
          v-model="statusReservationCriteria"
          class="w-100"
          label="label"
          :options="filterStatusReservation"
          :reduce="item => item.value"
        ></v-select>
      </el-col>
    </el-row>
  </el-dialog>
</template>

<script>
import { ref, watch, provide, computed } from 'vue'
import reservationFlightservices from '@/Services/ReservationFlights.Services'

export default {
  setup () {
    const { getReservationFlightsReservadedByReservationHotel } =
      reservationFlightservices()
    const reservationFlights = ref([])
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const isAddReservationFlight = ref(false)
    const statusReservationCriteria = ref(null)
    const showStatusReservationFilter = ref(false)
    provide('addReservationFlight', isAddReservationFlight)
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'dateTravel', text: 'Fecha del viaje' },
      { value: 'dateSale', text: 'Fecha de venta' },
      { value: 'confirmationKey', text: 'Clave de confirmación' },
      { value: 'customer', text: 'Cliente' },
      { value: 'contactPhone', text: 'Telefono de contacto' },
      { value: 'isDeleted', text: 'Estado reservación' },
      { value: 'actions', text: 'Acciones' }
    ])
    const filterOptions = computed(() => {
      const filterOptionsArray = []
      if (statusReservationCriteria.value !== null) {
        filterOptionsArray.push({
          field: 'isDeleted',
          comparison: '=',
          criteria: statusReservationCriteria.value === 'true'
        })
      }
      return filterOptionsArray
    })
    const filterStatusReservation = ref([
      {
        label: 'Activo',
        value: 'false'
      },
      {
        label: 'Cancelado',
        value: 'true'
      },
      {
        label: 'Mostrar todo',
        value: null
      }
    ])
    getReservationFlightsReservadedByReservationHotel(data => {
      reservationFlights.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getReservationFlightsReservadedByReservationHotel(data => {
        reservationFlights.value = data
        isloading.value = false
      })
    }
    watch(isAddReservationFlight, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    return {
      filter,
      perPage,
      currentPage,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,
      filterOptions,
      filterStatusReservation,
      statusReservationCriteria,
      showStatusReservationFilter,
      reservationFlights,
      isAddReservationFlight,
      refreshTable
    }
  }
}
</script>

<style></style>
