<template>
  <group-rate-add-new />
  <group-rate-edit :groupRateId="groupRateId" />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddGroupRate = !isAddGroupRate"
        >
          <i> Nueva Tarifa </i>
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
            :items="groupRates"
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
                      @click="onEditHabitation(items.groupRateId)"
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteHabitation(items.groupRateId)"
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
import { ref, watch, inject, provide, computed } from 'vue'
import GroupRateServices from '@/Services/GroupRate.Services'
import groupRateAddNew from './groupRateAddNew.vue'
import GroupRateEdit from './GroupRateEdit.vue'
import { useStore } from 'vuex'
export default {
  components: { groupRateAddNew, GroupRateEdit },
  setup () {
    const { getGroupRateByReservationHotelGroup, deleteGroupRate } =
      GroupRateServices()
    const store = useStore()
    const groupRates = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const isAddGroupRate = ref(false)
    const isEditGroupRate = ref(false)
    const groupRateId = ref()
    const reservationHotelGroupId = computed(
      () => store.getters.getReservationHotelGroupId
    )
    provide('addGroupRate', isAddGroupRate)
    provide('editHabitation', isEditGroupRate)
    const fields = ref([
      { value: 'adults', text: 'Adultos' },
      { value: 'juniors', text: 'Juniors' },
      { value: 'minorsCharge', text: 'Menores con cargo' },
      { value: 'minorsWithoutCharge', text: 'Menores sin cargo' },
      { value: 'agesMinors', text: 'Edad de los menores' },
      { value: 'dateStart', text: 'Fecha de llegada' },
      { value: 'dateEnd', text: 'Fecha de salida' },
      { value: 'rangePublicClient', text: 'TARIFA    X PAX   BASE DBL ' },
      { value: 'rangeJunior', text: 'Tarifa por junior' },
      { value: 'rangeMinor', text: 'Tarifa por menor' },
      { value: 'nightsNumber', text: 'Numero de noches' },
      { value: 'rangeNight', text: 'Tarifa por noche' },
      { value: 'rangeTotal', text: 'Tarifa total' },
      { value: 'observations', text: 'observaciones' },
      { value: 'actions', text: 'Acciones' }
    ])
    if (reservationHotelGroupId.value !== null && reservationHotelGroupId.value !== 0) {
      getGroupRateByReservationHotelGroup(
        reservationHotelGroupId.value,
        data => {
          groupRates.value = data
          isloading.value = false
        }
      )
    }
    const refreshTable = () => {
      isloading.value = true
      getGroupRateByReservationHotelGroup(
        reservationHotelGroupId.value,
        data => {
          groupRates.value = data
          isloading.value = false
        }
      )
    }
    watch([isAddGroupRate, isEditGroupRate], ([newValueA, newValueB]) => {
      if (!newValueA || !newValueB) {
        setTimeout(() => {
          refreshTable()
        }, 1000)
      }
    })
    const changeDialog = () => {
      isAddGroupRate.value = !isAddGroupRate.value
    }
    const onDeleteHabitation = habitationReservationId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar una tarifa, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar tarifa',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteGroupRate(habitationReservationId, data => {
              swal.fire({
                title: 'Tarifa eliminada!',
                text: 'La tarifa ha sido eliminada satisfactoriamente .',
                icon: 'success'
              })
              refreshTable()
            })
          } else {
            isloading.value = false
          }
        })
    }
    const onEditHabitation = item => {
      isEditGroupRate.value = !isEditGroupRate.value
      groupRateId.value = parseInt(item)
    }
    return {
      filter,
      perPage,
      currentPage,
      perPageSelect,
      isloading,
      fields,
      groupRates,
      isAddGroupRate,
      isEditGroupRate,
      groupRateId,
      reservationHotelGroupId,
      refreshTable,
      onDeleteHabitation,
      changeDialog,
      onEditHabitation
    }
  }
}
</script>

<style></style>
