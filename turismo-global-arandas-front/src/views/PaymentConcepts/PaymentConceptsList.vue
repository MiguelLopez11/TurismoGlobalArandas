<template>
  <payment-concepts-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar concepto de pago..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="4" :lg="5">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddPaymentConcept = !isAddPaymentConcept"
        >
          <i> Nuevo concepto de pago </i>
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
            :items="paymentConcepts"
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
                            name: 'Edit-Paymentconcepts',
                            params: { PaymentConceptId: items.paymentConceptId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeletePaymentConcept(items.paymentConceptId)"
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
import PaymentConceptsServices from '@/Services/PaymentConcepts.Services'
import PaymentConceptsAddNew from './PaymentConceptsAddNew.vue'

export default {
  components: { PaymentConceptsAddNew },
  setup () {
    const { getPaymentConcepts, deletePaymentConcept } = PaymentConceptsServices()
    const paymentConcepts = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddPaymentConcept = ref(false)
    provide('addPaymentMethod', isAddPaymentConcept)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'actions', text: 'Acciones' }
    ])
    getPaymentConcepts(data => {
      paymentConcepts.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getPaymentConcepts(data => {
        paymentConcepts.value = data
        isloading.value = false
      })
    }
    watch(isAddPaymentConcept, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeletePaymentConcept = paymentConceptId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un metodo de pago, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar metodo de pago',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deletePaymentConcept(paymentConceptId, data => {
              swal.fire({
                title: 'Metodo de pago eliminado!',
                text: 'El metodo de pago ha sido eliminado satisfactoriamente .',
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
      paymentConcepts,
      isAddPaymentConcept,
      refreshTable,
      onDeletePaymentConcept
    }
  }
}
</script>

<style></style>
