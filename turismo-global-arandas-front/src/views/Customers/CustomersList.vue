<template>
  <customers-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar cliente..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddCustomer = !isAddCustomer"
        >
          <i> Nuevo cliente </i>
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
            :items="customers"
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
                            params: { CustomerId: items.customerId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteCustomer(items.customerId)"
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
import CustomerServices from '@/Services/Customers.Services'
import CustomersAddNew from './CustomersAddNew.vue'

export default {
  components: { CustomersAddNew },
  setup () {
    const { getCustomers, deleteCustomer } = CustomerServices()
    const customers = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddCustomer = ref(false)
    provide('addCustomer', isAddCustomer)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'lastname', text: 'Apellidos' },
      { value: 'phoneNumber', text: 'Telefono' },
      { value: 'actions', text: 'Acciones' }
    ])
    getCustomers(data => {
      customers.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getCustomers(data => {
        customers.value = data
        isloading.value = false
      })
    }
    watch(isAddCustomer, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteCustomer = customerId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un cliente, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar cliente',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteCustomer(customerId, data => {
              swal.fire({
                title: 'Cliente eliminado!',
                text: 'El cliente ha sido eliminado satisfactoriamente .',
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
      customers,
      isAddCustomer,
      refreshTable,
      onDeleteCustomer
    }
  }
}
</script>

<style></style>
