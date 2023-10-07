<template>
  <ServiceProviderAddNew />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar comision..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddServiceProvider = !isAddServiceProvider"
        >
          <i> Nuevo servicio </i>
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
            :items="servicesProvider"
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
                            name: 'Edit-ServicesProvider',
                            params: { ServiceId: items.serviceId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteProvider(items.serviceId)"
                      >Eliminar</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
            <template #item-color="items">
              <el-tag :style="`background-color: ${items.color}`"> </el-tag>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import ServicesProviderServices from '@/Services/ProviderServices.Services'
import ServiceProviderAddNew from './ServiceProviderAddNew.vue'

export default {
  components: { ServiceProviderAddNew },
  setup () {
    const { getServicesProvider, deleteServiceProvider } =
      ServicesProviderServices()
    const servicesProvider = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddServiceProvider = ref(false)
    provide('addServices', isAddServiceProvider)
    const fields = ref([
      { value: 'color', text: 'Color' },
      { value: 'name', text: 'Servicio' },
      { value: 'providers.name', text: 'Provedor' },
      { value: 'commissionAgency', text: 'Comisión de la agencia' },
      { value: 'commissionClient', text: 'Comision al cliente' },
      { value: 'commissionEmployee', text: 'Comision al empleado' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getServicesProvider(data => {
      data.forEach((item, index) => {
        item.commissionAgency = (item.commissionAgency / 100).toFixed(2)
        item.commissionClient = (item.commissionClient / 100).toFixed(2)
        item.commissionEmployee = (item.commissionEmployee / 100).toFixed(2)
      })
      servicesProvider.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getServicesProvider(data => {
        data.forEach((item, index) => {
          item.commissionAgency = (item.commissionAgency / 100).toFixed(2)
          item.commissionClient = (item.commissionClient / 100).toFixed(2)
          item.commissionEmployee = (item.commissionEmployee / 100).toFixed(2)
        })
        servicesProvider.value = data
        isloading.value = false
      })
    }
    watch(isAddServiceProvider, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteProvider = commissionId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un servicio, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar servicio',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteServiceProvider(commissionId, data => {
              swal.fire({
                title: 'Proveedor eliminado!',
                text: 'El proveedor ha sido eliminado satisfactoriamente .',
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
      servicesProvider,
      isAddServiceProvider,
      refreshTable,
      onDeleteProvider
    }
  }
}
</script>

<style></style>
