<template>
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :span="8">
        <el-form-item>
          <v-select
            v-model="employeeFilter"
            label="name"
            class="w-100"
            :options="employees"
            :reduce="employee => employee.name + ' ' + employee.lastname"
            placeholder="Selecciona un empleado"
          >
            <template #option="{ name, lastname }">
              <label>{{ name }} {{ lastname }}</label>
            </template>
          </v-select>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <el-date-picker
            v-model="rangeDatesTravel"
            class="w-100"
            type="daterange"
            range-separator="A"
            start-placeholder="Fecha de salida"
            end-placeholder="Fecha de llegada"
            size="large"
            @change="onSelectTravelDate"
          />
        </el-form-item>
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
            :items="commissionsEmployee"
            :search-field="searchField"
            :search-value="searchValue"
            :filter-options="filterOptions"
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
                            name: 'Edit-CategoryReservation',
                            params: {
                              CategoryReservationId: items.categoryReservationId
                            }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="
                        onDeleteCategoryReservation(items.categoryReservationId)
                      "
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
import { ref, watch, computed } from 'vue'
import HomeServices from '@/Services/Home.Services'
import EmployeeServices from '@/Services/Employees.Services'
export default {
  setup () {
    const { getCommissionsEmployees } = HomeServices()
    const { getEmployees } = EmployeeServices()
    const commissionsEmployee = ref([])
    const rangeDatesTravel = ref([])
    const employees = ref([])
    const employeeSelected = ref()
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const employeeFilter = ref('')
    const fields = ref([
      { value: 'employeeName', text: 'Nombre de empleado' },
      { value: 'typeReservation', text: 'Tipo de reservacion' },
      { value: 'invoiceReservation', text: 'Folio de reserva' },
      { value: 'commission', text: 'Commision' },
      { value: 'dateSale', text: 'Fecha de venta' },
      { value: 'actions', text: 'Acciones' }
    ])
    const filterOptions = computed(() => {
      const filterOptionsArray = []
      if (employeeFilter.value !== '') {
        filterOptionsArray.push({
          field: 'employeeName',
          criteria: employeeFilter.value,
          comparison: '='
        })
      }
      return filterOptionsArray
    })
    getCommissionsEmployees(rangeDatesTravel.value, data => {
      commissionsEmployee.value = data
      isloading.value = false
    })
    getEmployees(data => {
      employees.value = data
    })
    const refreshTable = () => {
      getCommissionsEmployees(rangeDatesTravel.value, data => {
        commissionsEmployee.value = data
        isloading.value = false
      })
    }
    const onSelectTravelDate = () => {
      rangeDatesTravel.value[0] = rangeDatesTravel.value[0]
        .toISOString()
        .split('.')[0]
      rangeDatesTravel.value[1] = rangeDatesTravel.value[1]
        .toISOString()
        .split('.')[0]
      refreshTable()
    }
    watch(rangeDatesTravel, newValueA => {
      if (newValueA) {
        refreshTable()
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
      filterOptions,
      employeeFilter,
      commissionsEmployee,
      employees,
      employeeSelected,
      rangeDatesTravel,
      onSelectTravelDate
    }
  }
}
</script>

<style></style>
