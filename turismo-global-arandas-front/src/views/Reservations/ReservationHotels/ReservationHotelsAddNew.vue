<template>
  <el-card>
    <form-wizard
      nextButtonText="Siguiente"
      backButtonText="Atras"
      doneButtonText="Finalizar"
      color="#7367F0"
    >
      <tab-content
        title="Datos del cliente"
        icon="bi bi-person-circle"
        :beforeChange="validationClient"
      >
        <el-card>
          <el-row :gutter="0">
            <el-col :span="8">
              <div>
                <label>¿Es un cliente nuevo?</label>
              </div>
              <el-form-item>
                <el-switch size="large" v-model="isNewCustomer"> </el-switch>
              </el-form-item>
            </el-col>
            <el-col :span="8" v-if="!isNewCustomer">
              <el-form-item>
                <v-select
                  class="w-100"
                  v-model="reservationHotelFields.customerId"
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
                      :required="!reservationHotelFields.customerId"
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
      <tab-content title="Datos generales" icon="bi bi-airplane-engines" :beforeChange="validationGeneral">
        <el-row :gutter="35">
          <el-col :span="8">
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
                    :required="!reservationHotelFields.customerId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationHotelFields.hotelId"
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
                    :required="!reservationHotelFields.customerId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <div class="mb-2">
              <span>Fecha del viaje</span>
            </div>
            <el-form-item>
              <el-date-picker
                v-model="reservationHotelFields.travelDate"
                class="w-100"
                type="date"
                placeholder="Selecciona una fecha"
                size="large"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </tab-content>
      <tab-content title="Habitaciones" icon="bi bi-door-closed">
        <habitation-reservation-list />
      </tab-content>
    </form-wizard>
  </el-card>
</template>

<script>
import { ref } from 'vue'
import CustomerServices from '@/Services/Customers.Services'
import DestinationServices from '@/Services/Destinations.Services'
import HotelsServices from '@/Services/Hotels.Services'
import ReservationHotelServices from '@/Services/ReservationHotel.Services'
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
import HabitationReservationList from '@/views/HabitationReservation/HabitationReservationList'
export default {
  components: {
    CustomersAddNew,
    HabitationReservationList
  },
  setup () {
    const { getCustomers } = CustomerServices()
    const { getDestinations } = DestinationServices()
    const { getHotelByDestinationId } = HotelsServices()
    const { createReservationHotel, updateReservationHotel } = ReservationHotelServices()
    const isNewCustomer = ref(false)
    const customers = ref([])
    const destinations = ref([])
    const hotels = ref([])
    const destinationId = ref(null)
    const reservationHotel = ref([])
    const reservationHotelFields = ref({
      reservationHotelId: 0,
      reservationInvoice: null,
      travelDate: null,
      typeHabitation: null,
      numberHabitations: null,
      phoneContact: null,
      observations: null,
      groupCoordinator: null,
      phoneCoordinator: null,
      dateSale: null,
      promoter: null,
      paymentPeriod: null,
      paymentLimitDate: null,
      typeReservationId: null,
      employeeId: null,
      customerId: null,
      hotelId: null,
      habitationsReservationId: null,
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
      reservationHotelFields.value.customerId = value
      refreshCustomers()
    }
    const onGetHotel = destinationId => {
      reservationHotelFields.value.hotelId = null
      getHotelByDestinationId(destinationId, data => {
        hotels.value = data
      })
    }
    const validationClient = () => {
      return new Promise((resolve, reject) => {
        if (reservationHotelFields.value.customerId) {
          createReservationHotel(reservationHotelFields.value, data => {
            reservationHotel.value = data
          })
          resolve(true)
        }
        reject(new Error('Error'))
      })
    }
    const validationGeneral = () => {
      return new Promise((resolve, reject) => {
        if (reservationHotelFields.value.destinationId && reservationHotelFields.value.hotelId && reservationHotelFields.value.travelDate) {
          updateReservationHotel(reservationHotelFields.value, data => {
            reservationHotel.value = data
            console.log(data)
          })
          resolve(true)
        }
        reject(new Error('Error'))
      })
    }
    return {
      reservationHotelFields,
      customers,
      destinations,
      hotels,
      isNewCustomer,
      destinationId,
      onAddedCustomer,
      onGetHotel,
      validationClient,
      validationGeneral
    }
  }
}
</script>

<style>
.vs__dropdown-toggle {
  height: 40px;
}
</style>
