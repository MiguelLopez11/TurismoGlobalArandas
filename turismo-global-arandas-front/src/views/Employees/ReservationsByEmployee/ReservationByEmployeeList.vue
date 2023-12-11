<template>
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :span="8">
        <el-form-item>
          <v-select
            v-if="employeeId === 1"
            v-model="employeeIdfilter"
            label="name"
            class="w-100"
            :options="employeesFilter"
            :reduce="employee => employee.employeeId"
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
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar reservación por folio..."
        />
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
            :filter-options="filterOptions"
          >
            <template #header-actions="header">
              {{ header.text }}
            </template>
            <template #item-isDeleted="items">
              <el-tag
                effect="dark"
                class="ml-2"
                :type="items.isDeleted === false ? 'success' : 'danger'"
              >
                {{ items.isDeleted === false ? 'Activo' : 'Cancelado' }}
              </el-tag>
            </template>
            <template #item-isSoldOut="items">
              <el-tag
                effect="dark"
                class="ml-2"
                :type="items.isSoldOut !== false ? 'success' : 'warning'"
              >
                {{ items.isSoldOut !== false ? 'Liquidado' : 'Sin liquidar' }}
              </el-tag>
            </template>
            <template #header-isDeleted="header">
              <div class="filter-column">
                <img
                  src="@/Images/Filter-icon.jpg"
                  width="15"
                  height="15"
                  class="filter-icon"
                  @click.stop="
                    showStatusReservationFilter = !showStatusReservationFilter
                  "
                />
                {{ header.text }}
              </div>
            </template>
            <template #header-isSoldOut="header">
              <div class="filter-column">
                <img
                  src="@/Images/Filter-icon.jpg"
                  width="15"
                  height="15"
                  class="filter-icon"
                  @click.stop="
                    showStatusPaymentFilter = !showStatusPaymentFilter
                  "
                />
                {{ header.text }}
              </div>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
  <el-dialog
    v-model="showStatusReservationFilter"
    title="Filtrar reservaciones"
    width="80%"
    center
  >
    <el-row :gutter="35">
      <el-col :lg="8" :md="8">
        <v-select
          v-model="statusReservationCriteria"
          class="w-100"
          label="label"
          :options="filterStatusReservation"
          :reduce="item => item.value"
        ></v-select>
      </el-col>
    </el-row>
  </el-dialog>
  <el-dialog
    v-model="showStatusPaymentFilter"
    title="Filtrar por pago"
    width="80%"
    center
  >
    <el-row :gutter="35">
      <el-col :lg="8" :md="8">
        <v-select
          v-model="statusPaymentCriteria"
          class="w-100"
          label="label"
          :options="filterStatusPayment"
          :reduce="item => item.value"
        ></v-select>
      </el-col>
    </el-row>
  </el-dialog>
</template>

<script>
import { ref, computed, watch } from 'vue'
import EmployeeServices from '@/Services/Employees.Services'

export default {
  setup () {
    const { getReservationsByEmployee, getEmployees } = EmployeeServices()
    const employees = ref([])
    const employeesFilter = ref([])
    const rangeDatesTravel = ref([])
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const employeeId = parseInt(window.sessionStorage.getItem('EmployeeId'))
    const employeeIdfilter = ref()
    const statusReservationCriteria = ref(null)
    const statusPaymentCriteria = ref(null)
    const showStatusReservationFilter = ref(false)
    const showStatusPaymentFilter = ref(false)
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'origin', text: 'Tipo de reservación' },
      { value: 'dateSale', text: 'Fecha de venta' },
      { value: 'isDeleted', text: 'Estatus de reservación' },
      { value: 'isSoldOut', text: 'Estatus de pago' }
    ])
    getEmployees(data => {
      employeesFilter.value = data
    })
    getReservationsByEmployee(
      employeeIdfilter.value || employeeId,
      rangeDatesTravel.value,
      data => {
        employees.value = data
        isloading.value = false
      }
    )
    watch((rangeDatesTravel, employeeIdfilter), (newValueA, newValueB) => {
      if (newValueA || newValueB) {
        refreshTable()
      }
    })
    const refreshTable = () => {
      getReservationsByEmployee(
        employeeIdfilter.value || employeeId,
        rangeDatesTravel.value,
        data => {
          employees.value = data
          isloading.value = false
        }
      )
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
    const filterOptions = computed(() => {
      const filterOptionsArray = []
      if (statusReservationCriteria.value !== null) {
        filterOptionsArray.push({
          field: 'isDeleted',
          comparison: '=',
          criteria: statusReservationCriteria.value === 'true'
        })
      }
      if (statusPaymentCriteria.value !== null) {
        filterOptionsArray.push({
          field: 'isSoldOut',
          comparison: '=',
          criteria: statusPaymentCriteria.value === 'true'
        })
      }
      return filterOptionsArray
    })
    const filterStatusReservation = ref([
      {
        label: 'Activo',
        value: 'false'
      },
      {
        label: 'Cancelado',
        value: 'true'
      },
      {
        label: 'Mostrar todo',
        value: null
      }
    ])
    const filterStatusPayment = ref([
      {
        label: 'Pago pendiente',
        value: 'false'
      },
      {
        label: 'liquidado',
        value: 'true'
      },
      {
        label: 'Mostrar todo',
        value: null
      }
    ])
    return {
      filter,
      perPage,
      currentPage,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,
      showStatusReservationFilter,
      showStatusPaymentFilter,
      filterOptions,
      statusReservationCriteria,
      statusPaymentCriteria,
      filterStatusReservation,
      filterStatusPayment,
      employeeId,
      employeeIdfilter,
      employees,
      employeesFilter,
      onSelectTravelDate,
      rangeDatesTravel
    }
  }
}
</script>

<style></style>
