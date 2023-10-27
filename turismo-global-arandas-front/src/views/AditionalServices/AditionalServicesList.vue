<template>
  <aditional-service-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar servicio adicional..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddAditionalService = !isAddAditionalService"
        >
          <i> Nuevo Servicio adicional </i>
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
            :items="aditionalServices"
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
                            name: 'Edit-AditionalServices',
                            params: {
                              AditionalServiceId: items.aditionalServiceId
                            }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteAditionalService(items.aditionalServiceId)"
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
import { ref, inject, provide, watch } from 'vue'
import AditionalServiceServices from '@/Services/AditionalServices.Services'
import AditionalServiceAddNew from './AditionalServiceAddNew.vue'

export default {
  components: { AditionalServiceAddNew },
  setup () {
    const { getAditionalServices, deleteAditionalService } =
      AditionalServiceServices()
    const aditionalServices = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddAditionalService = ref(false)
    provide('addAditionalService', isAddAditionalService)
    watch(isAddAditionalService, NewValue => {
      if (!NewValue) {
        refreshTable()
      }
    })
    const fields = ref([
      { value: 'name', text: 'Nombre del servicio' },
      { value: 'cost', text: 'Costo' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getAditionalServices(data => {
      aditionalServices.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getAditionalServices(data => {
        aditionalServices.value = data
        isloading.value = false
      })
    }
    const onDeleteAditionalService = reservationTourId => {
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
            deleteAditionalService(reservationTourId, data => {
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
      aditionalServices,
      isAddAditionalService,
      refreshTable,
      onDeleteAditionalService
    }
  }
}
</script>

<style></style>
