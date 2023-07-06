<template>
  <employees-add-new />
  <el-card header="Empleados">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input v-model="searchValue" size="large" placeholder="Buscar empleado..." />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddedEmployee = !isAddedEmployee"
        >
          <i> Agregar empleado </i>
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
          :items="employees"
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
                <el-dropdown-item @click="() => {
                          $router.push({
                            name: 'Edit-Employees',
                            params: { EmployeeId: items.employeeId }
                          })
                        }
                      ">Editar</el-dropdown-item>
                <el-dropdown-item>Eliminar</el-dropdown-item>
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
import { ref, watch, provide } from 'vue'
import EmployeeServices from '@/Services/Employees.Services'
import EmployeesAddNew from './EmployeesAddNew.vue'

export default {
  components: { EmployeesAddNew },
  setup () {
    const { getEmployees } = EmployeeServices()
    const employees = ref([])
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddedEmployee = ref(false)
    provide('AddEmployee', isAddedEmployee)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'lastname', text: 'Apellidos' },
      { value: 'workStation', text: 'Puesto de trabajo' },
      { value: 'address', text: 'Dirección' },
      { value: 'phoneNumber', text: 'Telefono' },
      { value: 'salary', text: 'Salario' },
      { value: 'actions', text: 'Acciones' }
    ])
    getEmployees(data => {
      employees.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getEmployees(data => {
        employees.value = data
        isloading.value = false
      })
    }
    watch(isAddedEmployee, (newValue) => {
      if (!newValue) {
        refreshTable()
        // getEmployees(data => {
        //   employees.value = data
        // })
      }
    })
    return {
      filter,
      perPage,
      currentPage,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,
      employees,
      isAddedEmployee,
      refreshTable
    }
  }
}
</script>

<style></style>
