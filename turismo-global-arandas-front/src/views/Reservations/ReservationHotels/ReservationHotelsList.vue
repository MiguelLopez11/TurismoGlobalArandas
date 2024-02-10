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
                name: 'ReservacionesHoteleria-AddNew',
                params: {
                  ReservationHotelId: null
                }
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
                            name: 'Edit-ReservationHotels',
                            params: {
                              ReservationHotelId: items.reservationHotelId
                            }
                          })
                        }
                      "
                      :disabled="isPastDate(items.travelDateStart) && !userRole.includes('administrador' || 'GERENTE GENERAL')"
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        onDeleteReservationHotel(items.reservationHotelId)
                      "
                      >Cancelar reservación</el-dropdown-item
                    >
                    <!-- :disabled="isDisabledItem" -->
                    <el-dropdown-item
                      v-if="
                        userRole.includes('administrador' || 'GERENTE GENERAL')
                      "
                      @click="
                        onRemoveReservationHotel(items.reservationHotelId)
                      "
                      >Eliminar reservación</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        $router.push({
                          name: 'PaymentsRelationReservatioHotel',
                          params: {
                            ReservationHotelId: items.reservationHotelId
                          }
                        })
                      "
                      >Relación de pagos</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        $router.push({
                          name: 'PaymentProvider-Details-ReservationHotel',
                          params: {
                            ReservationHotelId: items.reservationHotelId
                          }
                        })
                      "
                      >Relación de pagos a proveedores</el-dropdown-item
                    >
                  </el-dropdown-menu>
                  <el-dropdown-item @click="onDownloadFile(items)"
                    >Descargar reporte</el-dropdown-item
                  >
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
            <template #item-isDeleted="items">
              <el-tag
                effect="dark"
                class="ml-2"
                :type="items.isDeleted === false ? 'success' : 'danger'"
              >
                {{ items.isDeleted === false ? 'Activo' : 'Cancelado' }}
              </el-tag>
            </template>
            <template #item-isSoldOut="items">
              <el-tag
                effect="dark"
                class="ml-2"
                :type="items.isSoldOut !== false ? 'success' : 'warning'"
              >
                {{ items.isSoldOut !== false ? 'Liquidado' : 'Sin liquidar' }}
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
            <template #header-isSoldOut="header">
              <div class="filter-column">
                <img
                  src="@/Images/Filter-icon.jpg"
                  width="15"
                  height="15"
                  class="filter-icon"
                  @click.stop="
                    showStatusPaymentFilter = !showStatusPaymentFilter
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
    <el-row :gutter="35" align="center">
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
  <el-dialog
    v-model="showStatusPaymentFilter"
    title="Filtrar por pago"
    width="80%"
    center
  >
    <el-row :gutter="35">
      <el-col :lg="8" :md="8">
        <v-select
          v-model="statusPaymentCriteria"
          class="w-100"
          label="label"
          :options="filterStatusPayment"
          :reduce="item => item.value"
        ></v-select>
      </el-col>
    </el-row>
  </el-dialog>
</template>

<script>
import { ref, watch, provide, inject, computed, onMounted } from 'vue'
import ReservationServices from '@/Services/ReservationHotel.Services'
export default {
  setup () {
    const {
      getReservationHotels,
      deleteReservationHotel,
      RemoveReservationHotel,
      downloadIndividualPDF,
      downloadGrupalPDF
    } = ReservationServices()
    const reservationHotels = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const isAddedEmployee = ref(false)
    const statusReservationCriteria = ref(null)
    const statusPaymentCriteria = ref(null)
    const showStatusReservationFilter = ref(false)
    const showStatusPaymentFilter = ref(false)
    const isDisabledItem = ref(false)
    const userRole = window.sessionStorage.getItem('Role')
    provide('AddEmployee', isAddedEmployee)
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'travelDateStart', text: 'Fecha de salida' },
      { value: 'travelDateEnd', text: 'Fecha de regreso' },
      { value: 'hotel', text: 'Hotel' },
      { value: 'destinations', text: 'Destino' },
      { value: 'dateSale', text: 'Fecha de venta' },
      { value: 'isSoldOut', text: 'Estado de pago' },
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
      if (statusPaymentCriteria.value !== null) {
        filterOptionsArray.push({
          field: 'isSoldOut',
          comparison: '=',
          criteria: statusPaymentCriteria.value === 'true'
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
    const filterStatusPayment = ref([
      {
        label: 'Pago pendiente',
        value: 'false'
      },
      {
        label: 'liquidado',
        value: 'true'
      },
      {
        label: 'Mostrar todo',
        value: null
      }
    ])
    const refreshTable = () => {
      isloading.value = true
      getReservationHotels(data => {
        reservationHotels.value = data
        isloading.value = false
      })
    }
    onMounted(() => {
      getReservationHotels(data => {
        reservationHotels.value = data
        isloading.value = false
      })
    })
    watch(
      [isAddedEmployee, statusReservationCriteria, statusPaymentCriteria],
      (newValueA, newValueB, newValueC) => {
        if (!newValueA) {
          refreshTable()
        }
        refreshTable()
      }
    )
    const isPastDate = date => {
      const currentDate = new Date()
      const apiDate = new Date(date)
      const apiDateISO = apiDate.toISOString()
      const currentDateISO = currentDate.toISOString()

      return apiDateISO < currentDateISO
    }
    const onDownloadFile = reservation => {
      if (
        reservation.typeReservationId === 2 &&
        reservation.typeReservationGroupId === 1
      ) {
        downloadGrupalPDF(reservation.reservationHotelId, data => {
          try {
            const blob = new Blob([data], { type: 'application/pdf' })
            const pdfUrl = URL.createObjectURL(blob)
            window.open(pdfUrl, '_blank')
          } catch (error) {
            console.error('Error al procesar el PDF', error)
          }
        })
      } else {
        downloadIndividualPDF(reservation.reservationHotelId, data => {
          try {
            const blob = new Blob([data], { type: 'application/pdf' })
            const pdfUrl = URL.createObjectURL(blob)
            window.open(pdfUrl, '_blank')
          } catch (error) {
            console.error('Error al procesar el PDF', error)
          }
        })
      }
    }
    const onRemoveReservationHotel = reservationHotelId => {
      swal
        .fire({
          title: 'Estás a punto de cancelar una reservación, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, cancelar reservación',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            RemoveReservationHotel(reservationHotelId, data => {
              swal.fire({
                title: 'Reservación eliminada!',
                text: 'La reservación ha sido eliminada satisfactoriamente .',
                icon: 'success'
              })
              refreshTable()
            })
          } else {
            isloading.value = false
          }
        })
    }
    const onDeleteReservationHotel = reservationHotelId => {
      swal
        .fire({
          title: 'Estás a punto de cancelar una reservación, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, cancelar reservación',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteReservationHotel(reservationHotelId, data => {
              swal.fire({
                title: 'Reservación cancelada!',
                text: 'La reservación ha sido cancelada satisfactoriamente .',
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
      showStatusReservationFilter,
      showStatusPaymentFilter,
      filterOptions,
      statusReservationCriteria,
      statusPaymentCriteria,
      filterStatusReservation,
      filterStatusPayment,
      isDisabledItem,
      refreshTable,
      onRemoveReservationHotel,
      onDeleteReservationHotel,
      onDownloadFile,
      userRole,
      isPastDate
    }
  }
}
</script>

<style></style>
