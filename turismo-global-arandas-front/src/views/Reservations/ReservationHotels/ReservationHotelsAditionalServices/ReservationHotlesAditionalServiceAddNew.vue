<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Registrar servicio"
    center
    align-center
  >
    <el-row :gutter="35" justify="center">
      <el-col :span="24">
        <el-form-item>
          <v-select
            class="w-100"
            v-model="reservationAditionalFields.aditionalServiceId"
            label="name"
            :options="services"
            :reduce="service => service.aditionalServiceId"
          >
            <template #selected-option="{ name, lastname }">
              <label>{{ name }} {{ lastname }}</label>
            </template>
            <template #option="{ name, lastname }">
              <label>{{ name }} {{ lastname }}</label>
            </template>
            <template #header>
              <span class="text-danger">*</span>
              <label> Servicio </label>
            </template>
            <template #search="{ attributes, events }">
              <input
                class="vs__search"
                :required="!reservationAditionalFields.aditionalServiceId"
                v-bind="attributes"
                v-on="events"
              />
            </template>
          </v-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row v-if="reservationAditionalFields.aditionalServiceId === 1">
      <Form
        ref="ReservationFlightFormRef"
        as="el-form"
        :validation-schema="validationSchemaFlight"
        @submit="onSubmit"
      >
        <el-row :gutter="35">
          <el-col :span="8">
            <Field name="travelDate" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
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
            <Field
              name="arrivalAirport"
              v-slot="{ value, field, errorMessage }"
            >
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
          <el-col :span="8">
            <Field
              name="confirmationKey"
              v-slot="{ value, field, errorMessage }"
            >
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
            <Field name="pricePublic" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <label>Precio público</label>
                </div>
                <el-input
                  placeholder="Ingresa el precio al público"
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
            <div>
              <label> ¿Es un vuelo sencillo?</label>
            </div>
            <el-form-item>
              <el-switch
                size="large"
                v-model="reservationFlightFields.isSimpleFlight"
                :disabled="
                  reservationFlightFields.isRoundFlight ||
                  reservationFlightFields.isMultidestinationFlight
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
                v-model="reservationFlightFields.isRoundFlight"
                :disabled="
                  reservationFlightFields.isSimpleFlight ||
                  reservationFlightFields.isMultidestinationFlight
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
                v-model="reservationFlightFields.isMultidestinationFlight"
                :disabled="
                  reservationFlightFields.isSimpleFlight ||
                  reservationFlightFields.isRoundFlight
                "
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
    </el-row>
    <el-row v-if="reservationAditionalFields.aditionalServiceId === 2">
      <Form
        ref="reservationVehicleFormRef"
        as="el-form"
        :validation-schema="validationSchemaVehicle"
        @submit="onSubmit"
      >
        <el-row :gutter="35">
          <el-col :span="8">
            <Field name="invoice" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <label> Folio </label>
                </div>
                <el-input
                  placeholder="Ingresa el folio de la reservación"
                  size="large"
                  v-bind="field"
                  v-model="reservationVehiclesFields.invoice"
                  :validate-event="false"
                  :model-value="value"
                >
                </el-input>
              </el-form-item>
            </Field>
          </el-col>
          <el-col :span="8">
            <Field name="dateTravel" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <label>Fecha de reservación</label>
                </div>
                <el-date-picker
                  v-model="reservationVehiclesFields.dateTravel"
                  class="w-100"
                  type="date"
                  placeholder="Selecciona la fecha del viaje"
                  size="large"
                  :validate-event="false"
                  :model-value="value"
                  v-bind="field"
                />
              </el-form-item>
            </Field>
          </el-col>
          <el-col :span="8">
            <Field
              name="departureLocation"
              v-slot="{ value, field, errorMessage }"
            >
              <el-form-item :error="errorMessage" required>
                <div>
                  <label> Lugar de salida </label>
                </div>
                <el-input
                  placeholder="Ingresa el lugar de donde saldrá el vehiculo"
                  size="large"
                  v-bind="field"
                  v-model="reservationVehiclesFields.departureLocation"
                  :validate-event="false"
                  :model-value="value"
                >
                </el-input>
              </el-form-item>
            </Field>
          </el-col>
          <el-col :span="8">
            <Field
              name="arrivalLocation"
              v-slot="{ value, field, errorMessage }"
            >
              <el-form-item :error="errorMessage" required>
                <div>
                  <label>Lugar de llegada</label>
                </div>
                <el-input
                  placeholder="Ingresa el lugar de llegada"
                  size="large"
                  v-bind="field"
                  v-model="reservationVehiclesFields.arrivalLocation"
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
                label="name"
                v-model="reservationVehiclesFields.providerId"
                :options="providers"
                :reduce="provider => provider.providerId"
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <span class="text-danger">*</span>
                  <label>Proveedor</label>
                </template>
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!reservationVehiclesFields.providerId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
                <template #list-footer>
                  <el-button
                    v-if="!reservationVehiclesFields.providerId"
                    class="w-100"
                    @click="
                      () => {
                        isAddProvider = !isAddProvider
                      }
                    "
                  >
                    agregar nuevo proveedor</el-button
                  >
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <Field name="pricePublic" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <label>Precio al público</label>
                </div>
                <el-input
                  v-model="reservationVehiclesFields.pricePublic"
                  :validate-event="false"
                  v-bind="field"
                  :model-value="value"
                  size="large"
                  placeholder="Ingresa el precio al público"
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
                  v-model="reservationVehiclesFields.priceNeto"
                  :validate-event="false"
                  v-bind="field"
                  :model-value="value"
                />
              </el-form-item>
            </Field>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <label>Descripción</label>
              </div>
              <el-input
                placeholder="Ingresa una descripción de la reservación"
                size="large"
                v-model="reservationVehiclesFields.description"
              />
            </el-form-item>
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
    </el-row>
    <el-row
      :gutter="25"
      justify="end"
      v-if="
        reservationAditionalFields.aditionalServiceId !== 1 &&
        reservationAditionalFields.aditionalServiceId !== 2
      "
    >
      <el-col :span="3">
        <el-button color="#7367F0" class="w-100" size="large" @click="onSubmit"
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
  </el-dialog>
</template>

<script>
import { ref, inject, computed } from 'vue'
import ReservationHotelAditionalService from '@/Services/ReservationHotelAditionalService.Service'
import AditionalServiceServices from '@/Services/AditionalServices.Services'
import CustomerServices from '@/Services/Customers.Services'
import ReservationFlightServices from '@/Services/ReservationFlights.Services'
import ProviderServices from '@/Services/Provider.Services'
import ReservationVehicleServices from '@/Services/ReservationVehicle.Services'
import { useStore } from 'vuex'
import * as yup from 'yup'

export default {
  setup () {
    const isOpenDialog = inject('addReservationAditionalService')
    const store = useStore()
    const swal = inject('$swal')
    const ReservationFlightFormRef = ref(null)
    const reservationVehicleFormRef = ref(null)
    const services = ref([])
    const customers = ref([])
    const providers = ref([])
    const rangeDatesTravel = ref()
    const reservationHotelId = computed(
      () => store.getters.getReservationHotelId
    )
    const { getCustomers } = CustomerServices()
    const { createReservationFlight } = ReservationFlightServices()
    const { createReservationHotelAditionalService } =
      ReservationHotelAditionalService()
    const { getAditionalServices } = AditionalServiceServices()
    const { getProviders } = ProviderServices()
    const { createReservationVehicle } = ReservationVehicleServices()

    getCustomers(data => {
      customers.value = data
    })
    getAditionalServices(data => {
      services.value = data
    })
    getProviders(data => {
      providers.value = data
    })
    const validationSchemaFlight = yup.object({
      travelDate: yup.array().required('Este campo es requerido'),
      departureAirport: yup.string().required('Este campo es requerido'),
      arrivalAirport: yup.string().required('Este campo es requerido'),
      airline: yup.string().required('Este campo es requerido'),
      confirmationKey: yup.string().required('Este campo es requerido'),
      priceNeto: yup
        .number()
        .test('is-decimal', 'invalid decimal', value =>
          (value + '').match(/^\d*\.?\d*$/)
        )
        .required('Este campo es requerido'),
      pricePublic: yup
        .number()
        .test('is-decimal', 'invalid decimal', value =>
          (value + '').match(/^\d*\.?\d*$/)
        )
        .required('Este campo es requerido'),
      paymentMethodAgency: yup.string().required('Este campo es requerido'),
      paymentMethodClient: yup.string().required('Este campo es requerido'),
      contactPhone: yup.string().required('Este campo es requerido')
    })
    const validationSchemaVehicle = yup.object({
      invoice: yup.string().required('Este campo es requerido'),
      dateTravel: yup.date().required('Este campo es requerido'),
      departureLocation: yup.string().required('Este campo es requerido'),
      arrivalLocation: yup.string().required('Este campo es requerido'),
      pricePublic: yup
        .number()
        .test('is-decimal', 'Ingresa un valor válido', value =>
          (value + '').match(/^\d*\.?\d*$/)
        )
        .required('Este campo es requerido'),
      priceNeto: yup
        .number()
        .test('is-decimal', 'Ingresa un valor válido', value =>
          (value + '').match(/^\d*\.?\d*$/)
        )
        .required('Este campo es requerido')
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
      statusFlightId: 1,
      isDeleted: false
    })
    const reservationFlightFieldsBlank = ref(
      JSON.parse(JSON.stringify(reservationFlightFields))
    )

    const reservationVehiclesFields = ref({
      reservationVehicleId: 0,
      invoice: null,
      dateSale: null,
      dateTravel: null,
      departureLocation: null,
      arrivalLocation: null,
      proveedorId: null,
      pricePublic: null,
      priceNeto: null,
      description: null,
      isDeleted: false
    })
    const reservationVehiclesFieldsBlank = ref(
      JSON.parse(JSON.stringify(reservationVehiclesFields))
    )
    const reservationAditionalFields = ref({
      reservationHotelId: reservationHotelId.value,
      aditionalServiceId: null,
      reservationFlightId: null,
      reservationVehicleId: null,
      isDeleted: false
    })
    const reservationAditionalFieldsBlank = ref(
      JSON.parse(JSON.stringify(reservationAditionalFields.value))
    )
    const onSelectTravelDate = () => {
      reservationFlightFields.value.travelDateStart = rangeDatesTravel.value[0]
      reservationFlightFields.value.travelDateEnd = rangeDatesTravel.value[1]
    }
    const onSubmit = () => {
      switch (reservationAditionalFields.value.aditionalServiceId) {
        case 1:
          createReservationFlight(reservationFlightFields.value, data => {
            reservationAditionalFields.value.reservationFlightId = data.flightId
            createReservationHotelAditionalService(
              {
                reservationHotelId:
                  reservationAditionalFields.value.reservationHotelId,
                aditionalServiceId:
                  reservationAditionalFields.value.aditionalServiceId,
                reservationFlightId:
                  reservationAditionalFields.value.reservationFlightId,
                reservationVehicleId:
                  reservationAditionalFields.value.reservationVehicleId,
                isDeleted: reservationAditionalFields.value.isDeleted
              },
              data => {
                swal.fire({
                  title: '¡Nuevo servicio registrado!',
                  text: 'El servicio adicional se ha registrado correctamente',
                  icon: 'success'
                })
                isOpenDialog.value = false
                reservationFlightFields.value = JSON.parse(
                  JSON.stringify(reservationFlightFieldsBlank)
                )
                ReservationFlightFormRef.value.resetForm()
                reservationAditionalFields.value = JSON.parse(
                  JSON.stringify(reservationAditionalFieldsBlank)
                )
              }
            )
          })
          break
        case 2:
          createReservationVehicle(reservationVehiclesFields.value, data => {
            reservationAditionalFields.value.reservationVehicleId =
              data.reservationVehicleId
            createReservationHotelAditionalService(
              {
                reservationHotelId:
                  reservationAditionalFields.value.reservationHotelId,
                aditionalServiceId:
                  reservationAditionalFields.value.aditionalServiceId,
                reservationFlightId:
                  reservationAditionalFields.value.reservationFlightId,
                reservationVehicleId:
                  reservationAditionalFields.value.reservationVehicleId,
                isDeleted: reservationAditionalFields.value.isDeleted
              },
              data => {
                swal.fire({
                  title: '¡Nuevo servicio registrado!',
                  text: 'El servicio adicional se ha registrado correctamente',
                  icon: 'success'
                })
                isOpenDialog.value = false
                reservationVehiclesFields.value = JSON.parse(
                  JSON.stringify(reservationVehiclesFieldsBlank)
                )
                reservationAditionalFields.value = JSON.parse(
                  JSON.stringify(reservationAditionalFieldsBlank)
                )
                reservationVehicleFormRef.value.resetForm()
              }
            )
          })

          break
        default:
          createReservationHotelAditionalService(
            {
              reservationHotelId:
                reservationAditionalFields.value.reservationHotelId,
              aditionalServiceId:
                reservationAditionalFields.value.aditionalServiceId,
              reservationFlightId:
                reservationAditionalFields.value.reservationFlightId,
              reservationVehicleId:
                reservationAditionalFields.value.reservationVehicleId,
              isDeleted: reservationAditionalFields.value.isDeleted
            },
            data => {
              swal.fire({
                title: '¡Nuevo servicio registrado!',
                text: 'El servicio adicional se ha registrado correctamente',
                icon: 'success'
              })
              isOpenDialog.value = false
              reservationAditionalFields.value = JSON.parse(
                JSON.stringify(reservationAditionalFieldsBlank)
              )
            }
          )
      }
    }

    return {
      isOpenDialog,
      reservationAditionalFields,
      reservationVehiclesFields,
      reservationFlightFields,
      services,
      providers,
      onSubmit,
      onSelectTravelDate,
      rangeDatesTravel,
      validationSchemaFlight,
      ReservationFlightFormRef,
      reservationVehicleFormRef,
      validationSchemaVehicle,
      customers
    }
  }
}
</script>

<style></style>
