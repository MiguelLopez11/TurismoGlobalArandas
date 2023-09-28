<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Nueva reservation de vuelo"
    width="80%"
    center
  >
    <Form
      ref="ReservationFlightFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="travelDate" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div class="mb-2">
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
                :validate-event="false"
                :model-value="value"
                v-bind="field"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="departureAirport"
            v-slot="{ value, field, errorMessage }"
          >
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Aeropuerto de salida </label>
              </div>
              <el-input
                placeholder="Ingresa el aeropuerto de salida para el cliente"
                size="large"
                v-bind="field"
                v-model="reservationFlightFields.departureAirport"
                :validate-event="false"
                :model-value="value"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="arrivalAirport" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Aeropuerto de llegada </label>
              </div>
              <el-input
                placeholder="Ingresa el aeropuerto de llegada para el cliente"
                size="large"
                v-bind="field"
                v-model="reservationFlightFields.arrivalAirport"
                :validate-event="false"
                :model-value="value"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="airline" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>AeroLinea</label>
              </div>
              <el-input
                placeholder="Ingresa la aerolinea en la que viajará el cliente"
                size="large"
                v-bind="field"
                v-model="reservationFlightFields.airline"
                :validate-event="false"
                :model-value="value"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <v-select
              class="w-100"
              v-model="reservationFlightFields.customerId"
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
                  :required="!reservationFlightFields.customerId"
                  v-bind="attributes"
                  v-on="events"
                />
              </template>
            </v-select>
          </el-form-item>
        </el-col>
        <el-col :span="4">
          <Field name="confirmationKey" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Clave de confirmación</label>
              </div>
              <el-input
                v-model="reservationFlightFields.confirmationKey"
                :validate-event="false"
                v-bind="field"
                :model-value="value"
                size="large"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="priceNeto" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Precio neto</label>
              </div>
              <el-input
                placeholder="Ingresa el precio neto"
                size="large"
                v-model="reservationFlightFields.priceNeto"
                :validate-event="false"
                v-bind="field"
                :model-value="value"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="pricePublic" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Precio neto</label>
              </div>
              <el-input
                placeholder="Ingresa el precio neto"
                size="large"
                v-model="reservationFlightFields.pricePublic"
                :validate-event="false"
                v-bind="field"
                :model-value="value"
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
              v-model="reservationFlightFields.isSimpleFlight"
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
              v-model="reservationFlightFields.isRoundFlight"
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
              v-model="reservationFlightFields.isMultidestinationFlight"
            >
            </el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <Field
            name="paymentMethodAgency"
            v-slot="{ value, field, errorMessage }"
          >
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Metodo de pago de la agencia</label>
              </div>
              <el-input
                placeholder="Ingresa una descripcion"
                size="large"
                v-model="reservationFlightFields.paymentMethodAgency"
                v-bind="field"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="paymentMethodClient"
            v-slot="{ value, field, errorMessage }"
          >
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Metodo de pago del cliente</label>
              </div>
              <el-input
                placeholder="Ingresa una descripcion"
                size="large"
                v-model="reservationFlightFields.paymentMethodClient"
                v-bind="field"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="contactPhone" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Telefono de contacto</label>
              </div>
              <el-input
                placeholder="Ingresa el telefono de contacto"
                size="large"
                v-model="reservationFlightFields.contactPhone"
                type="number"
                v-bind="field"
                :validate-event="false"
                :model-value="value"
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
                isOpenDialog = !isOpenDialog
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-dialog>
</template>

<script>
import { ref, inject } from 'vue'
import { Field, Form } from 'vee-validate'
import CustomerServices from '@/Services/Customers.Services'
import ReservationFlightServices from '@/Services/ReservationFlights.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addReservationFlight')
    const swal = inject('$swal')
    const ReservationFlightFormRef = ref(null)
    const customers = ref([])
    const rangeDatesTravel = ref()
    const { getCustomers } = CustomerServices()
    const { createReservationFlight } = ReservationFlightServices()
    const validationSchema = yup.object({
      travelDate: yup.array().required('Este campo es requerido'),
      departureAirport: yup.string().required('Este campo es requerido'),
      arrivalAirport: yup.string().required('Este campo es requerido'),
      airline: yup.string().required('Este campo es requerido'),
      confirmationKey: yup.string().required('Este campo es requerido'),
      priceNeto: yup.number().required('Este campo es requerido'),
      pricePublic: yup.number().required('Este campo es requerido'),
      paymentMethodAgency: yup.string().required('Este campo es requerido'),
      paymentMethodClient: yup.string().required('Este campo es requerido'),
      contactPhone: yup.string().required('Este campo es requerido')
    })
    const reservationFlightFields = ref({
      flightId: 0,
      invoice: null,
      travelDateStart: null,
      travelDateEnd: null,
      dateSale: null,
      departureAirport: null,
      arrivalAirport: null,
      customerId: null,
      airline: null,
      confirmationKey: null,
      priceNeto: null,
      pricePublic: null,
      isSimpleFlight: false,
      isRoundFlight: false,
      isMultidestinationFlight: false,
      paymentMethodAgency: null,
      paymentMethodClient: null,
      contactPhone: null,
      statusFlightId: null,
      isDeleted: false
    })
    const reservationFlightFieldsBlank = ref(
      JSON.parse(JSON.stringify(reservationFlightFields))
    )
    getCustomers(data => {
      customers.value = data
    })
    const onSelectTravelDate = () => {
      reservationFlightFields.value.travelDateStart = rangeDatesTravel.value[0]
      reservationFlightFields.value.travelDateEnd = rangeDatesTravel.value[1]
    }
    const onSubmit = () => {
      console.log('holi')
      createReservationFlight(reservationFlightFields.value, data => {
        swal.fire({
          title: '¡Nueva comisión registrada!',
          text: 'La nueva comisión se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        reservationFlightFields.value = JSON.parse(
          JSON.stringify(reservationFlightFieldsBlank)
        )
        ReservationFlightFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      ReservationFlightFormRef,
      customers,
      onSubmit,
      rangeDatesTravel,
      reservationFlightFields,
      onSelectTravelDate,
      validationSchema
    }
  }
}
</script>

<style>
.el-form-item__content {
  display: block;
}
</style>
