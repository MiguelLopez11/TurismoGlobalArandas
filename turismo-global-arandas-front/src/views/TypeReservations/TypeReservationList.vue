<template>
  <TypeReservationAddNew />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col class="m-1" :xs="24" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar tipo de reservación..."
        />
      </el-col>
      <el-col class="m-1" :xs="24" :sm="8" :md="6" :xl="4" :lg="5">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddedTypeReservation = !isAddedTypeReservation"
        >
          <i> Agregar tipo de reservación </i>
        </el-button>
      </el-col>
    </el-row>
    <el-row class="mt-3">
      <el-col :span="24">
        <EasyDataTable
          rows-per-page-message="registro por pagina"
          empty-message="No se en cuentran registros"
          table-class-name="customize-table"
          buttons-pagination
          header-text-direction="center"
          body-text-direction="center"
          border-cell
          theme-color="#7367F0"
          :table-height="500"
          :rows-per-page="10"
          :loading="isloading"
          :headers="fields"
          :items="typeReservations"
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
                          name: 'Edit-TypeReservation',
                          params: { TypeReservationId: items.typeReservationId }
                        })
                      }
                    "
                    >Editar</el-dropdown-item
                  >
                  <el-dropdown-item
                    @click="onDeleteTypeReservation(items.typeReservationId)"
                    >Eliminar</el-dropdown-item
                  >
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </template>
        </EasyDataTable>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import TypeReservationServices from '@/Services/TypeReservation.Services'
import TypeReservationAddNew from './TypeReservationAddNew.vue'

export default {
  components: { TypeReservationAddNew },
  setup () {
    const { getTypeReservations, deleteTypeReservation } =
      TypeReservationServices()
    const typeReservations = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddedTypeReservation = ref(false)
    provide('AddTypeReservation', isAddedTypeReservation)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getTypeReservations(data => {
      typeReservations.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getTypeReservations(data => {
        typeReservations.value = data
        isloading.value = false
      })
    }
    watch(isAddedTypeReservation, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteTypeReservation = typeReservationId => {
      swal
        .fire({
          title:
            'Estás a punto de eliminar un tipo de reservación, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar tipo de reservación',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteTypeReservation(typeReservationId, data => {
              swal.fire({
                title: 'Tipo de reservación eliminado!',
                text: 'El tipo de reservasion ha sido eliminado satisfactoriamente .',
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
      typeReservations,
      isAddedTypeReservation,
      refreshTable,
      onDeleteTypeReservation
    }
  }
}
</script>

<style>

</style>
