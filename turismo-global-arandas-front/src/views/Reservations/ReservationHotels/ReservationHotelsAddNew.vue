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
        lazy
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
                  v-model="reservationHotel.customerId"
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
                      :required="!reservationHotel.customerId"
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
      <tab-content
        title="Datos generales"
        icon="bi bi-airplane-engines"
        :beforeChange="validationGeneral"
        lazy
      >
        <el-row :gutter="35">
          <el-col :span="8">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationHotel.typeReservationId"
                :options="typeReservations"
                :reduce="typeReservation => typeReservation.typeReservationId"
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <label>Tipo de reserva</label>
                </template>
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!reservationHotel.typeReservationId"
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
                    :required="!destinationId"
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
                v-model="reservationHotel.hotelId"
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
                    :required="!reservationHotel.hotelId"
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
                v-model="reservationHotel.travelDate"
                class="w-100"
                type="date"
                placeholder="Selecciona una fecha"
                size="large"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </tab-content>
      <tab-content lazy title="Habitaciones" icon="bi bi-door-closed">
        <habitation-reservation-list />
      </tab-content>
      <tab-content lazy title="Tarifas" icon="bi bi-cash-stack">
        <individual-rate />
      </tab-content>
      <tab-content lazy title="Relación de pagos" icon="bi bi-receipt">
        <el-row :gutter="25" align="center">
          <el-col :span="8">
            <el-form-item>
              <div>
                <label> Promotora </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre de la promotora"
                size="large"
                v-model="reservationHotel.promoter"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <label> Agente </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre de el agente "
                size="large"
                v-model="reservationHotel.agent"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <label> Plazo de pago a cliente </label>
              </div>
              <el-input
                placeholder="Ingresa el plazo de pago a cliente"
                size="large"
                v-model="reservationHotel.paymentPeriod"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <div class="mb-2">
              <span>Fecha limite de pago</span>
            </div>
            <el-form-item>
              <el-date-picker
                v-model="reservationHotel.paymentLimitDate"
                class="w-100"
                type="date"
                placeholder="Selecciona la fecha limite del pago"
                size="large"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <div class="mb-2">
              <span>Fecha limite de pago del proveedor</span>
            </div>
            <el-form-item>
              <el-date-picker
                v-model="reservationHotel.paymentLimitDateProvider"
                class="w-100"
                type="date"
                placeholder="Selecciona la fecha limite del proveedor"
                size="large"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </tab-content>
    </form-wizard>
  </el-card>
</template>

<script>
// Services
import CustomerServices from '@/Services/Customers.Services'
import DestinationServices from '@/Services/Destinations.Services'
import HotelsServices from '@/Services/Hotels.Services'
import ReservationHotelServices from '@/Services/ReservationHotel.Services'
import TypeReservationServices from '@/Services/TypeReservation.Services'
// Components
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
import HabitationReservationList from '@/views/HabitationReservation/HabitationReservationList'
import IndividualRate from '@/views/Rates/IndividualRate.vue'
// Libraries
import { useStore } from 'vuex'
import { ref } from 'vue'
export default {
  components: {
    CustomersAddNew,
    HabitationReservationList,
    IndividualRate
  },
  setup () {
    const { getCustomers } = CustomerServices()
    const { getDestinations } = DestinationServices()
    const { getHotelByDestinationId } = HotelsServices()
    const {
      createReservationHotel,
      updateReservationHotel,
      getReservationHotel
    } = ReservationHotelServices()
    const { getTypeReservations } = TypeReservationServices()
    const store = useStore()
    const isNewCustomer = ref(false)
    const customers = ref([])
    const destinations = ref([])
    const hotels = ref([])
    const typeReservations = ref([])
    const destinationId = ref(null)
    const reservationHotel = ref([])
    const reservationHotelId = ref()
    const reservationHotelFields = ref({
      reservationHotelId: 0,
      isDeleted: false
    })
    createReservationHotel(reservationHotelFields.value, data => {
      reservationHotelId.value = data.reservationHotelId
      store.commit('setReservationHotelId', data.reservationHotelId)
      refreshReservationHotel()
    })

    getCustomers(data => {
      customers.value = data
    })
    getDestinations(data => {
      destinations.value = data
    })
    getTypeReservations(data => {
      typeReservations.value = data
    })
    const refreshCustomers = () => {
      getCustomers(data => {
        customers.value = data
      })
    }
    const onAddedCustomer = value => {
      isNewCustomer.value = !isNewCustomer.value
      reservationHotel.value.customerId = value
      refreshCustomers()
    }
    const onGetHotel = destinationId => {
      reservationHotel.value.hotelId = null
      getHotelByDestinationId(destinationId, data => {
        hotels.value = data
      })
    }
    const refreshReservationHotel = () => {
      getReservationHotel(reservationHotelId.value, data => {
        reservationHotel.value = data
      })
    }
    const validationClient = () => {
      return new Promise((resolve, reject) => {
        if (reservationHotel.value.customerId) {
          resolve(true)
        }
        reject(new Error('Error'))
      })
    }
    const validationGeneral = () => {
      return new Promise((resolve, reject) => {
        if (
          reservationHotel.value.hotelId &&
          reservationHotel.value.travelDate
        ) {
          updateReservationHotel(reservationHotel.value, data => {
            reservationHotel.value = data
            refreshReservationHotel()
          })
          resolve(true)
        }
        reject(new Error('Error'))
      })
    }
    return {
      reservationHotelFields,
      reservationHotel,
      typeReservations,
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
