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
        as="el-form"
        :validation-schema="validationSchemaFlight"
        @submit="onSubmit"
      >
        <el-row :gutter="35">
          <el-col :span="8" v-if="reservationFlightFields.isSimpleFlight">
            <el-form-item>
              <div>
                <span>Fecha de reservación del vuelo</span>
              </div>
              <el-date-picker
                v-model="reservationFlightFields.dateTravel"
                class="w-100"
                size="large"
                placeholder="Selecciona la fecha que reservará el vuelo"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8" v-if="reservationFlightFields.isSimpleFlight">
            <el-form-item>
              <v-select
                class="w-100"
                label="routeName"
                v-model="reservationFlightFields.routeDepartureAirportId"
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
                    :required="!reservationFlightFields.routeDepartureAirportId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8" v-if="reservationFlightFields.isSimpleFlight">
            <el-form-item>
              <v-select
                class="w-100"
                label="routeName"
                v-model="reservationFlightFields.routeArrivalAirportId"
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
                    :required="!reservationFlightFields.routeArrivalAirportId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8" v-if="reservationFlightFields.isSimpleFlight">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationFlightFields.airlineId"
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
                    :required="!reservationFlightFields.airlineId"
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
                @change="onSelectRoundFlight"
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
                @change="onSelectMultiDestinationFlight"
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
                  type="textarea"
                  :autosize="{ minRows: 4, maxRows: 8 }"
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
                  type="textarea"
                  :autosize="{ minRows: 4, maxRows: 8 }"
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
        <el-row v-if="reservationFlightFields.isMultidestinationFlight">
          <el-col :span="8">
            <el-form-item>
              <div>
                <label>Número de Destinos</label>
              </div>
              <el-input
                placeholder="Ingresa el número de destinos"
                size="large"
                v-model="destinationsNumber"
                type="number"
                min="1"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <template v-if="!reservationFlightFields.isSimpleFlight">
          <template
            v-for="(destination, index) in destinationsFlightFields"
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
  <el-dialog v-model="isAddedCustomer">
    <customers-add-new @add-customer="onAddedCustomer" />
  </el-dialog>
</template>

<script>
import { ref, inject, computed, watch } from 'vue'
import ReservationHotelAditionalService from '@/Services/ReservationHotelAditionalService.Service'
import AditionalServiceServices from '@/Services/AditionalServices.Services'
import CustomerServices from '@/Services/Customers.Services'
import ReservationFlightServices from '@/Services/ReservationFlights.Services'
import ProviderServices from '@/Services/Provider.Services'
import ReservationVehicleServices from '@/Services/ReservationVehicle.Services'
import ReservationFlightDestinationsServices from '@/Services/ReservationFlightDestinations.Services'
import PaymentsRelationReservationServices from '@/Services/PaymentRelationReservationHotel.Services'
import PaymentProviders from '@/Services/paymentProviders.Services'
import AirlineServices from '@/Services/Airline.Services'
import RouteServices from '@/Services/Routes.Services'
import { useStore } from 'vuex'
import * as yup from 'yup'
import CustomersAddNew from '@/views/Customers/CustomersAddNew.vue'

export default {
  components: {
    CustomersAddNew
  },
  setup () {
    const isOpenDialog = inject('addReservationAditionalService')
    const store = useStore()
    const swal = inject('$swal')
    const reservationVehicleFormRef = ref()
    const services = ref([])
    const customers = ref([])
    const providers = ref([])
    const airlines = ref([])
    const routes = ref([])
    const rangeDatesTravel = ref()
    const destinationsNumber = ref(1)
    const isAddedCustomer = ref(false)
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
    const { createReservationFlightDestination } =
      ReservationFlightDestinationsServices()
    const { createPaymentRelation } = PaymentsRelationReservationServices()
    const { createPaymentProvider } = PaymentProviders()
    const { getAirlines } = AirlineServices()
    const { getRoutes } = RouteServices()

    getCustomers(data => {
      customers.value = data
    })
    getAditionalServices(data => {
      services.value = data
    })
    getProviders(data => {
      providers.value = data
    })
    getAirlines(data => {
      airlines.value = data
    })
    getRoutes(data => {
      routes.value = data
    })
    const destinationsFlightFields = ref([])

    watch(destinationsNumber, newNumber => {
      destinationsFlightFields.value = Array.from(
        { length: newNumber },
        () => ({
          reservationFlightDestinationId: 0,
          routeDepartureAirportId: null,
          routeArrivalAirportId: null,
          airlineId: null,
          reservationFlightId: null,
          dateTravel: null
        })
      )
    })
    const validationSchemaFlight = yup.object({
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
      isReservadedByHotel: true,
      isDeleted: false
    })
    const reservationFlightFieldsBlank = ref(
      JSON.parse(JSON.stringify(reservationFlightFields))
    )
    const destinationsFlightFieldsBlank = ref(
      JSON.parse(JSON.stringify(destinationsFlightFields.value))
    )
    const reservationVehiclesFields = ref({
      reservationVehicleId: 0,
      invoice: null,
      dateSale: null,
      dateTravel: null,
      paymentLimitDate: null,
      paymentLimitDateProvider: null,
      departureLocation: null,
      arrivalLocation: null,
      providerId: null,
      pricePublic: null,
      priceNeto: null,
      description: null,
      isSoldOut: false,
      isReservadedByHotel: true,
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
    const onAddedCustomer = value => {
      isAddedCustomer.value = !isAddedCustomer.value
      reservationFlightFields.value.customerId = value
      getCustomers(data => {
        customers.value = data
      })
    }
    const onSubmit = () => {
      switch (reservationAditionalFields.value.aditionalServiceId) {
        case 1:
          createReservationFlight(reservationFlightFields.value, data => {
            if (
              reservationFlightFields.value.isRoundFlight ||
              reservationFlightFields.value.isMultidestinationFlight
            ) {
              destinationsFlightFields.value.forEach(item => {
                item.reservationFlightId = data.flightId
                createReservationFlightDestination(item, response => {})
              })
            }
            createPaymentRelation(
              {
                amountTotal: reservationFlightFields.value.priceNeto,
                amountMissing: null,
                reservationFlightId: data.flightId,
                statusPaymentRelationId: 1,
                isDeleted: false
              },
              data => {}
            )
            createPaymentProvider(
              {
                reservationFlightId: data.flightId,
                amountTotal: reservationFlightFields.value.priceNeto,
                amountMissing: reservationFlightFields.value.priceNeto,
                isDeleted: false
              },
              data => {}
            )
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
              data => {}
            )
            swal.fire({
              title: '¡Nuevo servicio registrado!',
              text: 'El servicio adicional se ha registrado correctamente',
              icon: 'success'
            })
            reservationFlightFields.value = JSON.parse(
              JSON.stringify(reservationFlightFieldsBlank)
            )
            reservationAditionalFields.value = JSON.parse(
              JSON.stringify(reservationAditionalFieldsBlank)
            )
            destinationsFlightFields.value = JSON.parse(
              JSON.stringify(destinationsFlightFieldsBlank)
            )

            isOpenDialog.value = false
          })
          break
        case 2:
          createReservationVehicle(reservationVehiclesFields.value, data => {
            reservationAditionalFields.value.reservationVehicleId =
              data.reservationVehicleId
            createPaymentProvider(
              {
                reservationVehicleId: data.reservationVehicleId,
                amountTotal: reservationFlightFields.value.priceNeto,
                amountMissing: reservationFlightFields.value.priceNeto,
                isDeleted: false
              },
              data => {}
            )
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
              data => {}
            )
            swal.fire({
              title: '¡Nuevo servicio registrado!',
              text: 'El servicio adicional se ha registrado correctamente',
              icon: 'success'
            })
            reservationVehiclesFields.value = JSON.parse(
              JSON.stringify(reservationVehiclesFieldsBlank)
            )
            reservationAditionalFields.value = JSON.parse(
              JSON.stringify(reservationAditionalFieldsBlank)
            )

            isOpenDialog.value = false
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
    const onSelectMultiDestinationFlight = () => {
      if (reservationFlightFields.value.isMultidestinationFlight) {
        destinationsNumber.value = 4
      } else {
        destinationsNumber.value = 0
      }
    }
    const onSelectRoundFlight = () => {
      if (reservationFlightFields.value.isRoundFlight) {
        destinationsNumber.value = 2
      } else {
        destinationsNumber.value = 0
      }
    }
    return {
      isOpenDialog,
      reservationAditionalFields,
      reservationVehiclesFields,
      reservationFlightFields,
      destinationsFlightFields,
      services,
      providers,
      airlines,
      routes,
      onSubmit,
      destinationsNumber,
      onSelectTravelDate,
      onAddedCustomer,
      isAddedCustomer,
      rangeDatesTravel,
      validationSchemaFlight,
      reservationVehicleFormRef,
      validationSchemaVehicle,
      customers,
      onSelectMultiDestinationFlight,
      onSelectRoundFlight
    }
  }
}
</script>

<style></style>
