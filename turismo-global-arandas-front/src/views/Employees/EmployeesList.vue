<template>
  <el-card header="Empleados">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input size="large" placeholder="Buscar empleado..." />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button class="w-100" size="large" color="#7367F0">
          <i> Agregar empleado </i>
        </el-button>
      </el-col>
    </el-row>
    <el-row class="mt-3">
      <el-col :span="24">
        <EasyDataTable
          ref="refTableRouter"
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
          <!-- Header actions -->
          <template #header-actions="header">
            {{ header.text }}
          </template>
          <!-- Content row actions -->
          <template #item-actions>
            <el-dropdown>
              <span class="bi bi-three-dots-vertical"> </span>
              <template #dropdown>
                <el-dropdown-menu>
                  <el-dropdown-item>Action 1</el-dropdown-item>
                  <el-dropdown-item>Action 2</el-dropdown-item>
                  <el-dropdown-item>Action 3</el-dropdown-item>
                  <el-dropdown-item disabled>Action 4</el-dropdown-item>
                  <el-dropdown-item divided>Action 5</el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
            <!-- <v-menu>
                <template v-slot:activator="{ props }">
                  <v-btn icon="mdi-dots-vertical" v-bind="props"></v-btn>
                </template>

                <v-list>
                  <v-list-item>
                    <v-list-item
                      @click="
                        () => {
                          $router.push({
                            name: 'edit-Routers',
                            params: { RouterId: items.routerId }
                          })
                        }
                      "
                    >
                      <template v-slot:prepend>
                        <v-icon icon="mdi mdi-square-edit-outline"></v-icon>
                      </template>
                      Editar
                    </v-list-item>
                    <v-list-item @click="onDeleteRouter(items.routerId)">
                      <template v-slot:prepend>
                        <v-icon icon="mdi mdi-trash-can-outline"></v-icon>
                      </template>
                      Eliminar
                    </v-list-item>
                    <v-list-item @click="() => (chatsOpen = !chatsOpen)">
                      <template v-slot:prepend>
                        <v-icon icon="mdi mdi-chart-areaspline"></v-icon>
                      </template>
                      Gráfica
                    </v-list-item>
                  </v-list-item>
                </v-list>
              </v-menu> -->
          </template>
        </EasyDataTable>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch } from 'vue'
import EmployeeServices from '@/Services/Employees.Services'
export default {
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
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'lastname', text: 'Apellidos' },
      { value: 'workstation', text: 'Puesto de trabajo' },
      { value: 'email', text: 'Correo electronico' },
      { value: 'phoneNumber', text: 'Telefono' },
      { value: 'salario', text: 'Salario' },
      { value: 'actions', text: 'Acciones' }
    ])
    getEmployees(data => {
      employees.value = data
    })
    watch(employees, newValue => {
      if (newValue) {
        isloading.value = !isloading.value
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
      employees
    }
  }
}
</script>

<style></style>
