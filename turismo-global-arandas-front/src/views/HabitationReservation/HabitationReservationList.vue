<template>
  <habitation-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar habitación..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddHabitation = !isAddHabitation"
        >
          <i> Nueva habitación </i>
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
            :items="habitationReservations"
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
                            name: 'Edit-Customer',
                            params: { habitationId: items.habitationId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteHabitation(items.habitationId)"
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
import { ref, watch, inject, provide } from 'vue'
import HabitationReservationServices from '@/Services/HabitationReservation.Services'
import HabitationAddNew from '../Habitation/HabitationAddNew.vue'

export default {
  components: { HabitationAddNew },
  setup () {
    const { getHabitationReservations, deleteHabitationReservation } = HabitationReservationServices()
    const habitationReservations = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddHabitation = ref(false)
    provide('addHabitation', isAddHabitation)
    const fields = ref([
      { value: 'habitations.invoice', text: 'Folio de la habitación' },
      { value: 'habitations.cost', text: 'Costo' },
      { value: 'habitations.adults', text: 'Adultos' },
      { value: 'habitations.minors', text: 'Menores' },
      { value: 'habitations.purchaseDate', text: 'Fecha de compra' },
      { value: 'actions', text: 'Acciones' }
    ])
    getHabitationReservations(data => {
      habitationReservations.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getHabitationReservations(data => {
        habitationReservations.value = data
        isloading.value = false
      })
    }
    watch(isAddHabitation, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const changeDialog = () => {
      isAddHabitation.value = !isAddHabitation.value
    }
    const onDeleteHabitation = habitationId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un habitación, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar habitación',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteHabitationReservation(habitationId, data => {
              swal.fire({
                title: 'Habitación eliminado!',
                text: 'La habitación ha sido eliminado satisfactoriamente .',
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
      habitationReservations,
      isAddHabitation,
      refreshTable,
      onDeleteHabitation,
      changeDialog
    }
  }
}
</script>

<style></style>
