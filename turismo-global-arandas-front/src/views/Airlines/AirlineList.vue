<template>
  <AirlineAddNew />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col class="m-1" :xs="24" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar aerolinea..."
        />
      </el-col>
      <el-col class="m-1" :xs="24" :sm="8" :md="6" :xl="4" :lg="5">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddAirline = !isAddAirline"
        >
          <i> Agregar aerolinea </i>
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
            :items="airlines"
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
                            name: 'Edit-Airlines',
                            params: { AirlineId: items.airlineId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteAirline(items.airlineId)"
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
import AirlineServices from '@/Services/Airline.Services'
import AirlineAddNew from './AirlineAddNew.vue'

export default {
  components: { AirlineAddNew },
  setup () {
    const { getAirlines, deleteAirline } = AirlineServices()
    const airlines = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddAirline = ref(false)
    provide('AddCategoryReservation', isAddAirline)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'phoneNumber', text: 'Numero de telefono' },
      { value: 'actions', text: 'Acciones' }
    ])
    getAirlines(data => {
      airlines.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getAirlines(data => {
        airlines.value = data
        isloading.value = false
      })
    }
    watch(isAddAirline, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteAirline = airlineId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un categoría de reservación, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar categoría de reservación',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteAirline(airlineId, data => {
              swal.fire({
                title: 'Categoría de reservación eliminado!',
                text: 'La categoría de reservasion ha sido eliminado satisfactoriamente .',
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
      airlines,
      isAddAirline,
      refreshTable,
      onDeleteAirline
    }
  }
}
</script>

<style></style>
