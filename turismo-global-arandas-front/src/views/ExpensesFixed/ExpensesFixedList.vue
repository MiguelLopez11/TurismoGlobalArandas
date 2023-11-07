<template>
  <expenses-fixed-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar gasto..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="4" :lg="5">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddExpenseFixed = !isAddExpenseFixed"
        >
          <i> Registrar nuevo gasto fijo </i>
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
            :items="Expenses"
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
                            name: 'Edit-ExpensesFixed',
                            params: { ExpenseFixedId: items.expensesFixedId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item @click="onDeleteExpense(items.expensesFixedId)"
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
import ExpenseFixedServices from '@/Services/ExpensesFixed.Services'
import ExpensesFixedAddNew from './ExpensesFixedAddNew.vue'

export default {
  components: { ExpensesFixedAddNew },
  setup () {
    const { getExpenses, deleteExpense } = ExpenseFixedServices()
    const Expenses = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddExpenseFixed = ref(false)
    provide('addExpenseFixed', isAddExpenseFixed)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'cost', text: 'Costo' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getExpenses(data => {
      Expenses.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getExpenses(data => {
        Expenses.value = data
        isloading.value = false
      })
    }
    watch(isAddExpenseFixed, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteExpense = expenseId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar gasto fijo, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar gasto',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteExpense(expenseId, data => {
              swal.fire({
                title: 'Gasto fijo eliminado!',
                text: 'El Gasto fijo ha sido eliminado satisfactoriamente .',
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
      Expenses,
      isAddExpenseFixed,
      refreshTable,
      onDeleteExpense
    }
  }
}
</script>

<style></style>
