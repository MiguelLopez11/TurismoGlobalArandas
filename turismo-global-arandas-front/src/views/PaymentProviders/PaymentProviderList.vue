<template>
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar pago..."
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
            :items="paymentProviders"
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
                            name: 'PaymentProvider-Details',
                            params: {
                              PaymentProviderId: items.paymentProviderId
                            }
                          })
                        }
                      "
                      >Detalles</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeletePaymentMethod(items.paymentId)"
                      >Eliminar</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
            <template #item-isDeleted="items">
              <el-tag
                class="ml-2"
                effect="dark"
                :type="items.isDeleted === false ? 'success' : 'danger'"
              >
                {{ items.isDeleted === false ? 'Activo' : 'Cancelado' }}
              </el-tag>
            </template>
            <template #item-reservation="items">
              <el-tag class="ml-2" effect="dark">
                {{
                  items.reservationHotelId
                    ? 'Reservacion Hoteleria'
                    : null || items.reservationTourId
                    ? 'Reservacion Tours'
                    : null || items.reservationFlightId
                    ? 'Reservacion vuelos'
                    : null || items.reservationVehicleId
                    ? 'Reservacion vehiculos'
                    : null
                }}
              </el-tag>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, inject } from 'vue'
import PaymentProviders from '@/Services/paymentProviders.Services'
// import PaymentMethodsAddNew from './PaymentMethodsAddNew.vue'

export default {
  //   components: { PaymentMethodsAddNew },
  setup () {
    const { getPaymentProviders, deletePaymentProvider } = PaymentProviders()
    const paymentProviders = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const fields = ref([
      { value: 'invoice', text: 'Folio' },
      { value: 'paymentDate', text: 'Fecha de pago' },
      { value: 'observations', text: 'Observaciones' },
      { value: 'reservation', text: 'Reservacion' },
      { value: 'isDeleted', text: 'Estatus pago proveedor' },
      { value: 'actions', text: 'Acciones' }
    ])
    getPaymentProviders(data => {
      paymentProviders.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getPaymentProviders(data => {
        paymentProviders.value = data
        isloading.value = false
      })
    }
    const onDeletePaymentMethod = paymentProviderId => {
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
            deletePaymentProvider(paymentProviderId, data => {
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
      paymentProviders,
      refreshTable,
      onDeletePaymentMethod
    }
  }
}
</script>

<style></style>
