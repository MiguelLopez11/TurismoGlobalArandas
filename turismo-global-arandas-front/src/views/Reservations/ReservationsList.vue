<template>
  <!-- <employees-add-new /> -->
  <el-card>
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar empleado..."
        />
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
            :items="employees"
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
                            name: 'Edit-Employees',
                            params: { EmployeeId: items.employeeId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteEmployee(items.employeeId)"
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
import EmployeeServices from '@/Services/Employees.Services'
// import EmployeesAddNew from './EmployeesAddNew.vue'

export default {
//   components: { EmployeesAddNew },
  setup () {
    const { getEmployees, deleteEmployee } = EmployeeServices()
    const employees = ref([])
    const swal = inject('$swal')
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
    watch(isAddedEmployee, newValue => {
      console.log(newValue)
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteEmployee = employeeId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un Empleado, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar empleado',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteEmployee(employeeId, data => {
              swal.fire({
                title: 'Empleado eliminado!',
                text: 'El Empleado ha sido eliminado satisfactoriamente .',
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
      employees,
      isAddedEmployee,
      refreshTable,
      onDeleteEmployee
    }
  }
}
</script>

<style></style>
