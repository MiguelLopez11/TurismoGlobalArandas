<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateCommission">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="travelDate" :rules="validateTravelDate">
            <el-form-item :error="errors.travelDate" required>
              <div>
                <span>Fecha del viaje</span>
              </div>
              <el-date-picker
                v-model="rangeDatesTravel"
                class="w-100"
                type="daterange"
                range-separator="A"
                start-placeholder="Fecha de salida"
                end-placeholder="Fecha de llegada"
                size="large"
                @change="onSelectTravelDate"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="departureAirport" :rules="validateDepartureAirport">
            <el-form-item :error="errors.departureAirport" required>
              <div>
                <label> Aeropuerto de salida </label>
              </div>
              <el-input
                placeholder="Ingresa el aeropuerto de salida para el cliente"
                size="large"
                v-model="reservationFlight.departureAirport"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="arrivalAirport" :rules="validateArrivalAirport">
            <el-form-item :error="errors.arrivalAirport" required>
              <div>
                <label> Aeropuerto de llegada </label>
              </div>
              <el-input
                placeholder="Ingresa el aeropuerto de llegada para el cliente"
                size="large"
                v-model="reservationFlight.arrivalAirport"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="airline" :rules="validateAirline">
            <el-form-item :error="errors.airline" required>
              <div>
                <label>AeroLinea</label>
              </div>
              <el-input
                placeholder="Ingresa la aerolinea en la que viajará el cliente"
                size="large"
                v-model="reservationFlight.airline"
              >
              </el-input>
            </el-form-item>
          </Field>
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
        <el-col :span="4">
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
import { useRoute, useRouter } from 'vue-router'
import { ref, inject, provide } from 'vue'

export default {
  setup () {
    const { getReservationFlight, updateReservationFlight } =
      ReservationFlightServices()
    const { getCustomers } = CustomerServices()
    const rangeDatesTravel = ref([])
    const customers = ref([])
    const reservationFlight = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    const isAddedCustomer = ref(false)
    provide('AddCustomer', isAddedCustomer)
    getReservationFlight(router.params.FlightId, data => {
      reservationFlight.value = data
      rangeDatesTravel.value.push(data.travelDateStart)
      rangeDatesTravel.value.push(data.travelDateEnd)
    })
    getCustomers(data => {
      customers.value = data
    })
    const onSelectTravelDate = () => {
      reservationFlight.value.travelDateStart = rangeDatesTravel.value[0]
      reservationFlight.value.travelDateEnd = rangeDatesTravel.value[1]
    }
    const onUpdateCommission = () => {
      updateReservationFlight(reservationFlight.value, data => {
        swal
          .fire({
            title: 'vuelo modificado correctamente',
            text: 'El vuelo se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              rangeDatesTravel.value = []
              redirect.push('/ReservacionesVuelos')
            }
          })
      })
    }
    const validateTravelDate = () => {
      if (
        !reservationFlight.value.travelDateStart &&
        !reservationFlight.value.travelDateEnd
      ) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateDepartureAirport = () => {
      if (!reservationFlight.value.departureAirport) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateArrivalAirport = () => {
      if (!reservationFlight.value.arrivalAirport) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAirline = () => {
      if (!reservationFlight.value.airline) {
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

    return {
      reservationFlight,
      customers,
      isAddedCustomer,
      rangeDatesTravel,
      onSelectTravelDate,
      onUpdateCommission,
      validateTravelDate,
      validateDepartureAirport,
      validateArrivalAirport,
      validateAirline,
      validateConfirmationKey,
      validatePricePublic,
      validatePriceNeto,
      validatePaymentMethodAgency,
      validatePaymentMethodClient,
      validateContactPhone
    }
  }
}
</script>

<style></style>
