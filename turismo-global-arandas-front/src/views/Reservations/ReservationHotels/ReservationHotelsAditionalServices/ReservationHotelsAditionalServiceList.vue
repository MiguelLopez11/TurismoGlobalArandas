<template>
  <reservation-hotles-aditional-service-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="10" :sm="12" :md="6" :xl="4" :lg="5">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="
            isAddReservationAditionalService = !isAddReservationAditionalService
          "
        >
          <i> registrar nuevo servicio </i>
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
            :items="servicesAditional"
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
                      @click="onDeleteAditionalService(items.id)"
                      >Eliminar</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
            <template #item-cost="items">
              {{
                items.reservationFlightId
                  ? items.reservationFlight.priceNeto
                  : ''
              }}
              {{
                items.reservationVehicleId
                  ? items.reservationVehicle.priceNeto
                  : ''
              }}
              {{
                !items.reservationFlightId && !items.reservationVehicleId
                  ? items.aditionalServices.cost
                  : ''
              }}
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject, computed } from 'vue'
import ReservationHotelAditionalService from '@/Services/ReservationHotelAditionalService.Service'
import ReservationFlightServices from '@/Services/ReservationFlights.Services'
import ReservationVehicleServices from '@/Services/ReservationVehicle.Services'
import ReservationHotlesAditionalServiceAddNew from './ReservationHotlesAditionalServiceAddNew.vue'
import PaymentProviders from '@/Services/paymentProviders.Services'
import { useStore } from 'vuex'
export default {
  components: { ReservationHotlesAditionalServiceAddNew },
  setup () {
    const {
      getReservationAditionalServiceByReservationHotelId,
      getReservationHotelAditionalService,
      deleteReservationHotelAditionalService
    } = ReservationHotelAditionalService()
    const { deleteReservationFlight } = ReservationFlightServices()
    const { deleteReservationVehicle } = ReservationVehicleServices()
    const { deletePaymentProvider, getPaymentProviderByReservationFlight } =
      PaymentProviders()
    const store = useStore()
    const servicesAditional = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('aditionalServices.name')
    const isAddReservationAditionalService = ref(false)
    const reservationHotelId = computed(
      () => store.getters.getReservationHotelId
    )
    provide('addReservationAditionalService', isAddReservationAditionalService)

    const fields = ref([
      { value: 'aditionalServices.name', text: 'Servicio' },
      { value: 'cost', text: 'Costo' },
      { value: 'actions', text: 'Acciones' }
    ])
    getReservationAditionalServiceByReservationHotelId(
      reservationHotelId.value,
      data => {
        servicesAditional.value = data
        isloading.value = false
      }
    )
    const refreshTable = () => {
      isloading.value = true
      getReservationAditionalServiceByReservationHotelId(
        reservationHotelId.value,
        data => {
          servicesAditional.value = data
          isloading.value = false
        }
      )
    }
    watch(isAddReservationAditionalService, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteAditionalService = id => {
      swal
        .fire({
          title:
            'Estás a punto de cancelar un servicio adicional, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, cancelar servicio',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            getReservationHotelAditionalService(id, data => {
              // console.log(data)
              if (data.reservationFlightId) {
                deleteReservationFlight(data.reservationFlightId, data => {})
                getPaymentProviderByReservationFlight(
                  data.reservationFlightId,
                  resp => {
                    deletePaymentProvider(
                      resp.paymentProviderId,
                      response => {}
                    )
                  }
                )
              }
              if (data.reservationVehicleId) {
                deleteReservationVehicle(data.reservationVehicleId, data => {})
              }
            })
            deleteReservationHotelAditionalService(id, data => {
              swal.fire({
                title: 'Servicio Cancelado!',
                text: 'El servicio adicional ha sido cancelado satisfactoriamente .',
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
      servicesAditional,
      isAddReservationAditionalService,
      refreshTable,
      onDeleteAditionalService
    }
  }
}
</script>

<style></style>
