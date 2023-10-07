<template>
  <TypeReservationsGroupAddNew />
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
          @click="isAddedTypeReservationGroup = !isAddedTypeReservationGroup"
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
          :rows-per-page="10"
          :loading="isloading"
          :headers="fields"
          :items="typeReservationsgroup"
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
                          name: 'Edit-TypeReservationsGroup',
                          params: { TypeReservationGrupalId: items.typeReservationGrupalId }
                        })
                      }
                    "
                    >Editar</el-dropdown-item
                  >
                  <el-dropdown-item
                    @click="onDeleteTypeReservation(items.typeReservationGrupalId)"
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
import TypeReservationGrupalservices from '@/Services/TypeReservationGroup.Services'
import TypeReservationsGroupAddNew from './TypeReservationsGroupAddNew.vue'

export default {
  components: { TypeReservationsGroupAddNew },
  setup () {
    const { getTypeReservationGrupals, deleteTypeReservationGrupal } =
      TypeReservationGrupalservices()
    const typeReservationsgroup = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddedTypeReservationGroup = ref(false)
    provide('AddTypeReservationGroup', isAddedTypeReservationGroup)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getTypeReservationGrupals(data => {
      typeReservationsgroup.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getTypeReservationGrupals(data => {
        typeReservationsgroup.value = data
        isloading.value = false
      })
    }
    watch(isAddedTypeReservationGroup, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteTypeReservation = typeReservationGrupalId => {
      swal
        .fire({
          title:
            'Estás a punto de eliminar un tipo de reservación grupal, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar tipo de reservación grupal',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteTypeReservationGrupal(typeReservationGrupalId, data => {
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
      typeReservationsgroup,
      isAddedTypeReservationGroup,
      refreshTable,
      onDeleteTypeReservation
    }
  }
}
</script>

<style>

</style>
