<template>
  <el-card>
    <form-wizard
      nextButtonText="Siguiente"
      backButtonText="Atras"
      doneButtonText="Finalizar"
      color="#7367F0"
    >
      <tab-content title="Datos del cliente" icon="bi bi-person-circle">
        <el-card>
          <el-row :gutter="35" justify="center">
            <el-col :span="10">
              <div>
                <label>¿Es un cliente nuevo?</label>
              </div>
              <el-form-item>
                <el-switch size="large" v-model="isNewCustomer"> </el-switch>
              </el-form-item>
            </el-col>
            <el-col :span="6" v-if="!isNewCustomer">
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
      <tab-content title="Datos generales" icon="">
        <el-row :gutter="35">
          <el-col :span="6">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="destinationId"
                :options="destinations"
                :reduce="destination => destination.destinationId"
                @close="onGetHotel(destinationId)"
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <label>Destino</label>
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
          <el-col :span="6">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="ReservationHotelFields.hotelId"
                :options="hotels"
                :reduce="hotel => hotel.hotelId"
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <label>Hotel</label>
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
          <el-col :span="6">
            <div class="mb-2">
              <span>Fecha del viaje</span>
            </div>
            <el-form-item>
              <el-date-picker
                class="w-100"
                type="date"
                placeholder="Selecciona una fecha"
                size="large"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </tab-content>
      <tab-content title="Last step">
        Yuhuuu! This seems pretty damn simple
      </tab-content>
    </form-wizard>
  </el-card>
</template>

<script>
import { ref } from 'vue'
import CustomerServices from '@/Services/Customers.Services'
import DestinationServices from '@/Services/Destinations.Services'
import HotelsServices from '@/Services/Hotels.Services'
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
export default {
  components: {
    CustomersAddNew
  },
  setup () {
    const { getCustomers } = CustomerServices()
    const { getDestinations } = DestinationServices()
    const { getHotelByDestinationId } = HotelsServices()
    const isNewCustomer = ref(false)
    const customers = ref([])
    const destinations = ref([])
    const hotels = ref([])
    const destinationId = ref(null)
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
    getDestinations(data => {
      destinations.value = data
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
    const onGetHotel = destinationId => {
      ReservationHotelFields.value.hotelId = null
      getHotelByDestinationId(destinationId, data => {
        hotels.value = data
      })
    }
    return {
      ReservationHotelFields,
      customers,
      destinations,
      hotels,
      isNewCustomer,
      destinationId,
      onAddedCustomer,
      onGetHotel
    }
  }
}
</script>

<style>
.vs__dropdown-toggle {
  height: 40px;
}
</style>
