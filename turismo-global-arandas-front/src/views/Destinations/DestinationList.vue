<template>
  <destination-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar Destino..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddDestination = !isAddDestination"
        >
          <i> Nuevo destino </i>
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
            :items="destinations"
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
                            name: 'Edit-Destination',
                            params: { DestinationId: items.destinationId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteDestination(items.destinationId)"
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
import DestinationServices from '@/Services/Destinations.Services'
import DestinationAddNew from './DestinationAddNew.vue'

export default {
  components: { DestinationAddNew },
  setup () {
    const { getDestinations, deleteDestination } = DestinationServices()
    const destinations = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddDestination = ref(false)
    provide('addDestination', isAddDestination)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getDestinations(data => {
      destinations.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getDestinations(data => {
        destinations.value = data
        isloading.value = false
      })
    }
    watch(isAddDestination, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteDestination = destinationId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un destino, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar destino',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteDestination(destinationId, data => {
              swal.fire({
                title: 'Destino eliminado!',
                text: 'El destino ha sido eliminado satisfactoriamente .',
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
      destinations,
      isAddDestination,
      refreshTable,
      onDeleteDestination
    }
  }
}
</script>

<style></style>
