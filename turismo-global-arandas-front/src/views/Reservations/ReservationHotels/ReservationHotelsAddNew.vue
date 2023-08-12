<template>
  <el-card>
    <form-wizard color="#7367F0">
      <tab-content title="Datos del cliente" icon="bi bi-person-circle">
        <el-card>
          <el-row :gutter="35">
            <el-col :span="10">
              <div>
                <label>¿Es un cliente nuevo?</label>
              </div>
              <el-form-item>
                <el-switch size="large" v-model="isNewCustomer"> </el-switch>
              </el-form-item>
            </el-col>
            <el-col :span="10" v-if="!isNewCustomer">
              <el-form-item>
                <v-select
                  class="w-100"
                  v-model="ReservationHotelFields.customerId"
                  label="name"
                  :options="customers"
                  :reduce="customer => customer.customerId"
                >
                  <template #selected-option="{ name, lastname }">
                    <label>{{ name }} {{ lastname }}</label>
                  </template>
                  <template #option="{ name, lastname }">
                    <label>{{ name }} {{ lastname }}</label>
                  </template>
                  <template #header>
                    <label>Cliente</label>
                  </template>
                  <template #search="{ attributes, events }">
                    <input
                      class="vs__search"
                      :required="!ReservationHotelFields.customerId"
                      v-bind="attributes"
                      v-on="events"
                    />
                  </template>
                </v-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <customers-add-new
                v-if="isNewCustomer"
                @add-customer="onAddedCustomer"
              />
            </el-col>
          </el-row>
        </el-card>
      </tab-content>
      <tab-content title="Datos generales" icon=""> </tab-content>
      <tab-content title="Last step">
        Yuhuuu! This seems pretty damn simple
      </tab-content>
    </form-wizard>
  </el-card>
</template>

<script>
import { ref } from 'vue'
import CustomerServices from '@/Services/Customers.Services'
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
export default {
  components: {
    CustomersAddNew
  },
  setup () {
    const { getCustomers } = CustomerServices()
    const isNewCustomer = ref(false)
    const customers = ref([])
    const ReservationHotelFields = ref({
      reservationHotelId: null,
      reservationInvoice: '',
      travelDate: '',
      typeHabitation: '',
      numberHabitations: null,
      phoneContact: '',
      observations: '',
      groupCoordinator: '',
      phoneCoordinator: '',
      dateSale: '',
      promoter: '',
      paymentPeriod: '',
      paymentLimitDate: '',
      typeReservationId: null,
      employeeId: null,
      customerId: null,
      hotelId: null,
      habitationsReservationId: null,
      inlcudesTrasport: false,
      inlcudesBreakfast: false,
      isDeleted: false
    })
    getCustomers(data => {
      customers.value = data
    })
    const refreshCustomers = () => {
      getCustomers(data => {
        customers.value = data
      })
    }
    const onAddedCustomer = value => {
      isNewCustomer.value = !isNewCustomer.value
      ReservationHotelFields.value.customerId = value
      refreshCustomers()
    }
    return {
      ReservationHotelFields,
      customers,
      isNewCustomer,
      onAddedCustomer
    }
  }
}
</script>

<style scoped>
</style>
