<template>
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar reservación..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="
            () => {
              $router.push({
                name: 'ReservacionesHoteleria-AddNew'
              })
            }
          "
        >
          <i> Agregar reservación </i>
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
            :items="reservationHotels"
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
                            name: 'Edit-ReservationHotels',
                            params: {
                              ReservationHotelId: items.reservationHotelId
                            }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        onDeleteReservationHotel(items.reservationHotelId)
                      "
                      >Eliminar</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
            <template #item-destinations="items">
              <span>{{
                items.destinations ? items.destinations.name : ''
              }}</span>
            </template>
            <template #item-hotel="items">
              <span>{{ items.hotels ? items.hotels.name : '' }}</span>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import ReservationServices from '@/Services/ReservationHotel.Services'

export default {
  setup () {
    const { getReservationHotels, deleteReservationHotel } =
      ReservationServices()
    const reservationHotels = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('reservationInvoice')
    const isAddedEmployee = ref(false)
    provide('AddEmployee', isAddedEmployee)
    const fields = ref([
      { value: 'reservationInvoice', text: 'Folio' },
      { value: 'travelDateStart', text: 'Fecha de salida' },
      { value: 'travelDateEnd', text: 'Fecha de regreso' },
      { value: 'hotel', text: 'Hotel' },
      { value: 'destinations', text: 'Destino' },
      { value: 'dateSale', text: 'Fecha de venta' },
      { value: 'actions', text: 'Acciones' }
    ])
    getReservationHotels(data => {
      reservationHotels.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getReservationHotels(data => {
        reservationHotels.value = data
        isloading.value = false
      })
    }
    watch(isAddedEmployee, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteReservationHotel = reservationHotelId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar una reservación, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar reservación',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteReservationHotel(reservationHotelId, data => {
              swal.fire({
                title: 'Reservación archivada!',
                text: 'La reservación ha sido archivada satisfactoriamente .',
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
      isloading,
      searchValue,
      searchField,
      fields,
      reservationHotels,
      isAddedEmployee,
      refreshTable,
      onDeleteReservationHotel
    }
  }
}
</script>

<style></style>
