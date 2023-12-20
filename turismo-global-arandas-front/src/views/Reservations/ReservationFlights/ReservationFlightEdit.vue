<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateFlight">
      <el-row :gutter="35">
        <el-col :span="8" v-if="reservationFlight.isSimpleFlight">
          <Field name="travelDate" :rules="validateTravelDate">
            <el-form-item :error="errors.travelDate" required>
              <div>
                <span>Fecha del viaje</span>
              </div>
              <el-date-picker
                v-model="reservationFlight.dateTravel"
                class="w-100"
                size="large"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8" v-if="reservationFlight.isSimpleFlight">
          <el-form-item>
            <v-select
              class="w-100"
              label="routeName"
              v-model="reservationFlight.routeDepartureAirportId"
              :options="routes"
              :reduce="route => route.routeId"
            >
              <template #header>
                <span class="text-danger">*</span>
                <label>Ruta del aeropuerto de salida</label>
              </template>
              <template #search="{ attributes, events }">
                <input
                  class="vs__search"
                  :required="!reservationFlight.routeDepartureAirportId"
                  v-bind="attributes"
                  v-on="events"
                />
              </template>
            </v-select>
          </el-form-item>
        </el-col>
        <el-col :span="8" v-if="reservationFlight.isSimpleFlight">
          <el-form-item>
            <v-select
              class="w-100"
              label="routeName"
              v-model="reservationFlight.routeArrivalAirportId"
              :options="routes"
              :reduce="route => route.routeId"
            >
              <template #header>
                <span class="text-danger">*</span>
                <label>Ruta del aeropuerto de llegada</label>
              </template>
              <template #search="{ attributes, events }">
                <input
                  class="vs__search"
                  :required="!reservationFlight.routeArrivalAirportId"
                  v-bind="attributes"
                  v-on="events"
                />
              </template>
            </v-select>
          </el-form-item>
        </el-col>
        <el-col :span="8" v-if="reservationFlight.isSimpleFlight">
          <el-form-item>
            <v-select
              class="w-100"
              label="name"
              v-model="reservationFlight.airlineId"
              :options="airlines"
              :reduce="airline => airline.airlineId"
            >
              <template #selected-option="{ name, lastname }">
                <label>{{ name }} {{ lastname }}</label>
              </template>
              <template #option="{ name, lastname }">
                <label>{{ name }} {{ lastname }}</label>
              </template>
              <template #header>
                <span class="text-danger">*</span>
                <label>aerolinea</label>
              </template>
              <template #search="{ attributes, events }">
                <input
                  class="vs__search"
                  :required="!reservationFlight.airlineId"
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
              v-model="reservationFlight.customerId"
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
                <span class="text-danger">*</span>
                <label> Cliente</label>
              </template>
              <template #list-footer>
                <el-button
                  class="w-100"
                  @click="
                    () => {
                      isAddedCustomer = !isAddedCustomer
                    }
                  "
                >
                  Agregar nuevo cliente</el-button
                >
              </template>
              <template #search="{ attributes, events }">
                <input
                  class="vs__search"
                  :required="!reservationFlight.customerId"
                  v-bind="attributes"
                  v-on="events"
                />
              </template>
            </v-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <Field name="confirmationKey" :rules="validateConfirmationKey">
            <el-form-item :error="errors.confirmationKey" required>
              <div>
                <label>Clave de confirmación</label>
              </div>
              <el-input
                v-model="reservationFlight.confirmationKey"
                size="large"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="pricePublic" :rules="validatePricePublic">
            <el-form-item :error="errors.pricePublic" required>
              <div>
                <label>Precio al público</label>
              </div>
              <el-input
                placeholder="Ingresa el precio al público"
                size="large"
                v-model="reservationFlight.pricePublic"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="priceNeto" :rules="validatePriceNeto">
            <el-form-item :error="errors.priceNeto" required>
              <div>
                <label>Precio neto</label>
              </div>
              <el-input
                placeholder="Ingresa el precio neto"
                size="large"
                v-model="reservationFlight.priceNeto"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <div>
            <label> ¿Es un vuelo sencillo?</label>
          </div>
          <el-form-item>
            <el-switch
              size="large"
              v-model="reservationFlight.isSimpleFlight"
              :disabled="
                reservationFlight.isRoundFlight ||
                reservationFlight.isMultidestinationFlight
              "
            >
            </el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <div>
            <label> ¿Es un vuelo redondo?</label>
          </div>
          <el-form-item>
            <el-switch
              size="large"
              v-model="reservationFlight.isRoundFlight"
              :disabled="
                reservationFlight.isSimpleFlight ||
                reservationFlight.isMultidestinationFlight
              "
            >
            </el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <div>
            <label> ¿Es un vuelo con multiples destinos?</label>
          </div>
          <el-form-item>
            <el-switch
              size="large"
              v-model="reservationFlight.isMultidestinationFlight"
              :disabled="
                reservationFlight.isSimpleFlight ||
                reservationFlight.isRoundFlight
              "
            >
            </el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <Field
            name="paymentMethodAgency"
            :rules="validatePaymentMethodAgency"
          >
            <el-form-item :error="errors.paymentMethodAgency" required>
              <div>
                <label>Metodo de pago de la agencia</label>
              </div>
              <el-input
                placeholder="Ingresa el método de pago que usó la agencia"
                size="large"
                v-model="reservationFlight.paymentMethodAgency"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="paymentMethodClient"
            :rules="validatePaymentMethodClient"
          >
            <el-form-item :error="errors.paymentMethodClient" required>
              <div>
                <label>Metodo de pago del cliente</label>
              </div>
              <el-input
                placeholder="Ingresa el método de pago que usó el cliente"
                size="large"
                v-model="reservationFlight.paymentMethodClient"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="contactPhone" :rules="validateContactPhone">
            <el-form-item :error="errors.contactPhone" required>
              <div>
                <label>Telefono de contacto</label>
              </div>
              <el-input
                placeholder="Ingresa el telefono de contacto"
                size="large"
                v-model="reservationFlight.contactPhone"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
      </el-row>
      <el-row v-if="reservationFlight.isMultidestinationFlight">
        <el-col :span="8">
          <el-form-item>
            <div>
              <label>Número de Destinos</label>
            </div>
            <el-input-number
              size="large"
              v-model="destinationsNumber"
              :min="1"
              @change="onChangeDestinations"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <template v-if="!reservationFlight.isSimpleFlight">
        <template
          v-for="(destination, index) in destinationsFlight"
          :key="index"
        >
          <el-row :gutter="25" class="border mt-2">
            <el-col :span="8">
              <el-form-item>
                <div>
                  <span>Fecha de reservación del vuelo</span>
                </div>
                <el-date-picker
                  v-model="destination.dateTravel"
                  class="w-100"
                  size="large"
                  placeholder="Selecciona la fecha que reservará el vuelo"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <v-select
                  class="w-100"
                  label="routeName"
                  v-model="destination.routeDepartureAirportId"
                  :options="routes"
                  :reduce="route => route.routeId"
                >
                  <template #header>
                    <span class="text-danger">*</span>
                    <label>Ruta del aeropuerto de salida</label>
                  </template>
                  <template #search="{ attributes, events }">
                    <input
                      class="vs__search"
                      :required="!destination.routeDepartureAirportId"
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
                  label="routeName"
                  v-model="destination.routeArrivalAirportId"
                  :options="routes"
                  :reduce="route => route.routeId"
                >
                  <template #header>
                    <span class="text-danger">*</span>
                    <label>Ruta del aeropuerto de llegada</label>
                  </template>
                  <template #search="{ attributes, events }">
                    <input
                      class="vs__search"
                      :required="!destination.routeArrivalAirportId"
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
                  v-model="destination.airlineId"
                  :options="airlines"
                  :reduce="airline => airline.airlineId"
                >
                  <template #selected-option="{ name, lastname }">
                    <label>{{ name }} {{ lastname }}</label>
                  </template>
                  <template #option="{ name, lastname }">
                    <label>{{ name }} {{ lastname }}</label>
                  </template>
                  <template #header>
                    <span class="text-danger">*</span>
                    <label>aerolinea</label>
                  </template>
                  <template #search="{ attributes, events }">
                    <input
                      class="vs__search"
                      :required="!destination.airlineId"
                      v-bind="attributes"
                      v-on="events"
                    />
                  </template>
                </v-select>
              </el-form-item>
            </el-col>
          </el-row>
        </template>
      </template>
      <el-divider />
      <el-row :gutter="25" justify="end">
        <el-col :span="3">
          <el-button
            color="#7367F0"
            class="w-100"
            native-type="submit"
            size="large"
            >Guardar</el-button
          >
        </el-col>
        <el-col :span="3">
          <el-button
            class="w-100"
            color="#F1F1F2"
            size="large"
            @click="
              () => {
                $router.push('/ReservacionesVuelos')
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-card>
</template>

<script>
import ReservationFlightServices from '@/Services/ReservationFlights.Services'
import CustomerServices from '@/Services/Customers.Services'
import AirlineServices from '@/Services/Airline.Services'
import RouteServices from '@/Services/Routes.Services'
import ReservationFlightDestinationsServices from '@/Services/ReservationFlightDestinations.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject, provide } from 'vue'

export default {
  setup () {
    const { getReservationFlight, updateReservationFlight } =
      ReservationFlightServices()
    const { getCustomers } = CustomerServices()
    const { getAirlines } = AirlineServices()
    const { getRoutes } = RouteServices()
    const {
      getReservationFlightDestinationByFlightId,
      createReservationFlightDestination,
      updateReservationFlightDestination,
      deleteReservationFlightDestination
    } = ReservationFlightDestinationsServices()
    const customers = ref([])
    const reservationFlight = ref([])
    const airlines = ref([])
    const routes = ref([])
    const destinationsFlight = ref([])
    const destinationsNumber = ref(null)
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    const isAddedCustomer = ref(false)
    provide('AddCustomer', isAddedCustomer)
    getReservationFlight(router.params.FlightId, data => {
      reservationFlight.value = data
    })
    getReservationFlightDestinationByFlightId(router.params.FlightId, data => {
      destinationsFlight.value = data
      destinationsNumber.value = destinationsFlight.value.length
    })
    getCustomers(data => {
      customers.value = data
    })
    getAirlines(data => {
      airlines.value = data
    })
    getRoutes(data => {
      routes.value = data
    })
    const refreshDestinations = () => {
      getReservationFlightDestinationByFlightId(
        router.params.FlightId,
        data => {
          destinationsFlight.value = data
          destinationsNumber.value = destinationsFlight.value.length
        }
      )
    }
    const onUpdateFlight = () => {
      if (!reservationFlight.value.isSimpleFlight) {
        destinationsFlight.value.forEach(response => {
          updateReservationFlightDestination(response, data => {})
        })
      }
      updateReservationFlight(reservationFlight.value, data => {
        swal
          .fire({
            title: 'vuelo modificado correctamente',
            text: 'El vuelo se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ReservacionesVuelos')
            }
          })
      })
    }
    const validateTravelDate = () => {
      if (!reservationFlight.value.dateTravel) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateDepartureAirport = () => {
      if (!reservationFlight.value.routeDepartureAirportId) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateArrivalAirport = () => {
      if (!reservationFlight.value.routeArrivalAirportId) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAirline = () => {
      if (!reservationFlight.value.airlineId) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateConfirmationKey = () => {
      if (!reservationFlight.value.confirmationKey) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePricePublic = () => {
      if (!reservationFlight.value.pricePublic) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePriceNeto = () => {
      if (!reservationFlight.value.priceNeto) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePaymentMethodAgency = () => {
      if (!reservationFlight.value.paymentMethodAgency) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePaymentMethodClient = () => {
      if (!reservationFlight.value.paymentMethodClient) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateContactPhone = () => {
      if (!reservationFlight.value.contactPhone) {
        return 'Este campo es requerido'
      }
      return true
    }
    const onChangeDestinations = () => {
      const currentDestinations = destinationsFlight.value.length
      const newDestinations = destinationsNumber.value
      const delta = newDestinations - currentDestinations
      if (delta >= 1) {
        for (let i = 0; i < delta; i++) {
          createReservationFlightDestination(
            { reservationFlightId: router.params.FlightId },
            data => {
              refreshDestinations()
            }
          )
        }
      } else {
        const destinationsToRemove = Math.abs(delta)
        for (let j = 0; j < destinationsToRemove; j++) {
          const destinationsNumber = destinationsFlight.value.length - j
          deleteReservationFlightDestination(
            destinationsFlight.value[destinationsNumber]
              .reservationFlightDestinationId,
            data => {}
          )
        }
        refreshDestinations()
      }
    }
    return {
      reservationFlight,
      customers,
      routes,
      airlines,
      destinationsFlight,
      destinationsNumber,
      isAddedCustomer,
      onUpdateFlight,
      validateTravelDate,
      validateDepartureAirport,
      validateArrivalAirport,
      validateAirline,
      validateConfirmationKey,
      validatePricePublic,
      validatePriceNeto,
      validatePaymentMethodAgency,
      validatePaymentMethodClient,
      validateContactPhone,
      onChangeDestinations
    }
  }
}
</script>

<style></style>
