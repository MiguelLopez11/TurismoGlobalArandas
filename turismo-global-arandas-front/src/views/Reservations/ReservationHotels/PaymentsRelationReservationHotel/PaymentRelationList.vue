<template>
  <payment-relation-list-add-new />
  <payment-realtion-edit :PaymentId="paymenRelationtId" />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar pagos..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddPaymentRelation = !isAddPaymentRelation"
          :disabled="!paymentsRelation.amountTotal"
        >
          <i> Registrar nuevo pago </i>
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
            :items="paymentsRelationList"
            :search-field="searchField"
            :search-value="searchValue"
          >
            <template #header-actions="header">
              {{ header.text }}
            </template>
            <template #item-employee="items">
              {{ items.employees ? items.employees.name + ' ' + items.employees.lastname : '' }}
            </template>
            <template #item-actions="items">
              <el-dropdown>
                <span class="bi bi-three-dots-vertical"> </span>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item
                      @click="onDownloadPayment(items.paymentId)"
                      >Descargar Pago</el-dropdown-item
                    >
                    <el-dropdown-item @click="onEditPayment(items.paymentId)"
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item @click="onDeletePayment(items.paymentId)"
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
import PaymentsRelationListServices from '@/Services/PaymentRelationList.Services'
import PaymentsRelationReservationServices from '@/Services/PaymentRelationReservationHotel.Services'
import PaymentRelationListAddNew from './PaymentRelationAddNew.vue'
import PaymentRealtionEdit from './PaymentRelationEdit.vue'
import { useStore } from 'vuex'

export default {
  components: { PaymentRelationListAddNew, PaymentRealtionEdit },
  emits: ['refresh-payment-relation'],
  setup (props, { emit }) {
    const {
      getPaymentRelationListByPaymentReservationHotel,
      deletePaymentRelationList,
      downloadPayment
    } = PaymentsRelationListServices()
    const { getPaymentRelation } = PaymentsRelationReservationServices()
    const store = useStore()
    const paymentsRelationList = ref([])
    const paymentsRelation = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const paymenRelationtId = ref(0)
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const isAddPaymentRelation = ref(false)
    const isEditPaymentRelation = ref(false)
    const paymentReservationId = ref(0)
    setTimeout(() => {
      paymentReservationId.value = parseInt(
        store.getters.getPaymentReservationId
      )
      getPaymentRelationListByPaymentReservationHotel(
        paymentReservationId.value,
        data => {
          paymentsRelationList.value = data
          isloading.value = false
        }
      )
      getPaymentRelation(paymentReservationId.value, data => {
        paymentsRelation.value = data
      })
    }, 1000)
    provide('addPaymentRelation', isAddPaymentRelation)
    provide('editPaymentRelation', isEditPaymentRelation)
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'amount', text: 'Monto' },
      { value: 'paymentDate', text: 'Fecha de pago' },
      { value: 'observations', text: 'Observaciones' },
      { value: 'employee', text: 'Empleado receptor' },
      { value: 'actions', text: 'Acciones' }
    ])

    const refreshTable = () => {
      isloading.value = true
      setTimeout(() => {
        getPaymentRelationListByPaymentReservationHotel(
          paymentReservationId.value,
          data => {
            paymentsRelationList.value = data
            isloading.value = false
            emit('refresh-payment-relation')
          }
        )
      }, 2000)
    }
    const onDownloadPayment = paymentId => {
      downloadPayment(paymentId, async data => {
        try {
          const blob = new Blob([data], { type: 'application/pdf' })
          const pdfUrl = URL.createObjectURL(blob)
          window.open(pdfUrl, '_blank')
        } catch (error) {
          console.error('Error al procesar el PDF', error)
        }
      })
    }
    watch(
      [isAddPaymentRelation, isEditPaymentRelation],
      ([newValueA, newValueB]) => {
        if (!newValueA && !newValueB) {
          refreshTable()
        }
      }
    )
    const onEditPayment = paymentId => {
      isEditPaymentRelation.value = true
      paymenRelationtId.value = paymentId
    }
    const onDeletePayment = PaymentId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un role, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar pago',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deletePaymentRelationList(PaymentId, data => {
              store.commit('setRefreshPaymentRelation', true)
              swal.fire({
                title: 'pago eliminado!',
                text: 'El pago ha sido eliminado satisfactoriamente .',
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
      paymentsRelationList,
      paymentsRelation,
      isAddPaymentRelation,
      paymentReservationId,
      paymenRelationtId,
      onDownloadPayment,
      refreshTable,
      onEditPayment,
      onDeletePayment
    }
  }
}
</script>

<style></style>
