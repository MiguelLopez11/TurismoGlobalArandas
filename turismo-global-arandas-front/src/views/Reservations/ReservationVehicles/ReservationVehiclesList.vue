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
          @click="isAddReservationVehicle = !isAddReservationVehicle"
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
            :items="reservationVehicles"
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
                            name: 'Edit-ReservationVehicles',
                            params: {
                              ReservationVehicleId: items.reservationVehicleId
                            }
                          })
                        }
                      "
                      :disabled="isPastDate(items.dateTravel) && !userRole.includes('administrador' || 'GERENTE GENERAL')"
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        onDeleteReservationVehicle(items.reservationVehicleId)
                      "
                      >Cancelar</el-dropdown-item
                    >
                    <el-dropdown-item
                      v-if="
                        userRole.includes('administrador' || 'GERENTE GENERAL')
                      "
                      @click="onRemoveReservationVehicle(items.reservationVehicleId)"
                      >Eliminar reservación</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDownloadPDFReport(items.reservationVehicleId)"
                      >Descargar reporte</el-dropdown-item
                    >
                  </el-dropdown-menu>
                  <el-dropdown-item
                    @click="
                      $router.push({
                        name: 'PaymentsRelationReservatioVehicle',
                        params: {
                          ReservationVehicleId: items.reservationVehicleId
                        }
                      })
                    "
                    >Relación de pagos</el-dropdown-item
                  >
                </template>
              </el-dropdown>
            </template>
            <template #item-providers="items">
              <span>{{ items.providers ? items.providers.name : '' }}</span>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
  <reservation-vehicles-add-new />
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import ReservationVehicleServices from '@/Services/ReservationVehicle.Services'
import ReservationVehiclesAddNew from './ReservationVehiclesAddNew.vue'

export default {
  components: {
    ReservationVehiclesAddNew
  },
  setup () {
    const { getReservationVehicles, deleteReservationVehicle, downloadPDFReport, RemoveReservationVehicle } =
      ReservationVehicleServices()
    const reservationVehicles = ref([])
    const swal = inject('$swal')
    const userRole = window.sessionStorage.getItem('Role')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const isAddReservationVehicle = ref(false)
    provide('AddReservationVehicle', isAddReservationVehicle)
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'departureLocation', text: 'Lugar de salida' },
      { value: 'arrivalLocation', text: 'Lugar de llegada' },
      { value: 'providers', text: 'Proveedor' },
      { value: 'pricePublic', text: 'Precio al público' },
      { value: 'priceNeto', text: 'Precio neto' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getReservationVehicles(data => {
      reservationVehicles.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getReservationVehicles(data => {
        reservationVehicles.value = data
        isloading.value = false
      })
    }
    watch(isAddReservationVehicle, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDownloadPDFReport = (reservationVehicleId) => {
      downloadPDFReport(reservationVehicleId, data => {
        try {
          const blob = new Blob([data], { type: 'application/pdf' })
          const pdfUrl = URL.createObjectURL(blob)
          window.open(pdfUrl, '_blank')
        } catch (error) {
          console.error('Error al procesar el PDF', error)
        }
      })
    }
    const onRemoveReservationVehicle = reservationVehicleId => {
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
            RemoveReservationVehicle(reservationVehicleId, data => {
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
    const isPastDate = date => {
      const currentDate = new Date()
      const apiDate = new Date(date)
      const apiDateISO = apiDate.toISOString()
      const currentDateISO = currentDate.toISOString()

      return apiDateISO < currentDateISO
    }
    const onDeleteReservationVehicle = reservationVehicleId => {
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
            deleteReservationVehicle(reservationVehicleId, data => {
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
      reservationVehicles,
      isAddReservationVehicle,
      userRole,
      isPastDate,
      refreshTable,
      onDownloadPDFReport,
      onRemoveReservationVehicle,
      onDeleteReservationVehicle
    }
  }
}
</script>

<style></style>
