<template>
  <Providers-add-new />
  <el-card>
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar proveedor..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddProvider = !isAddProvider"
        >
          <i> Nuevo proveedor </i>
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
          border-cell
          :loading="isloading"
          :headers="fields"
          :items="Providers"
          :rows-per-page="5"
          :search-field="searchField"
          :search-value="searchValue"
          theme-color="#6366F1"
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
                          name: 'Edit-Provider',
                          params: { ProviderId: items.providerId }
                        })
                      }
                    "
                    >Editar</el-dropdown-item
                  >
                  <el-dropdown-item @click="onDeleteProvider(items.providerId)"
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
import ProviderServices from '@/Services/Provider.Services'
// import CustomersAddNew from './CustomersAddNew.vue'

export default {
//   components: { CustomersAddNew },
  setup () {
    const { getProviders, deleteProvider } = ProviderServices()
    const Providers = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddProvider = ref(false)
    provide('addProvider', isAddProvider)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'lastname', text: 'Apellidos' },
      { value: 'phoneNumber', text: 'Telefono' },
      { value: 'actions', text: 'Acciones' }
    ])
    getProviders(data => {
      Providers.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getProviders(data => {
        Providers.value = data
        isloading.value = false
      })
    }
    watch(isAddProvider, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteProvider = customerId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un proveedor, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar proveedor',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteProvider(customerId, data => {
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
      Providers,
      isAddProvider,
      refreshTable,
      onDeleteProvider
    }
  }
}
</script>

<style></style>
