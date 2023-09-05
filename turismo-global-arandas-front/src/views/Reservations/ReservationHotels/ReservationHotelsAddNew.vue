<template>
  <el-card>
    <form-wizard
      nextButtonText="Siguiente"
      backButtonText="Atras"
      doneButtonText="Finalizar"
      color="#7367F0"
      @on-complete="onComplete"
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
                <label> ¿Es un cliente nuevo?</label>
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
                    <span class="text-danger">*</span>
                    <label> Cliente</label>
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
                  <span class="text-danger">*</span>
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
                  <span class="text-danger">*</span>
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
                  <span class="text-danger">*</span>
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
              <span class="text-danger">*</span>
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
          <el-col :span="8">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationHotel.providerId"
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
                  <label>Promotora</label>
                </template>
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!reservationHotel.providerId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
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
                <span class="text-danger">*</span>
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
              <span class="text-danger">*</span>
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
              <span class="text-danger">*</span>
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
      <tab-content lazy title="Habitaciones" icon="bi bi-door-closed">
        <habitation-reservation-list />
      </tab-content>
      <tab-content
        lazy
        title="Tarifas"
        icon="bi bi-cash-stack"
        :beforeChange="validationRates"
      >
        <el-row :gutter="35">
          <el-col :span="8">
            <Field name="publicRate" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <span class="text-danger">*</span>
                  <label> Tarifa Pública </label>
                </div>
                <el-input
                  placeholder="Ingresa la tarifa pública"
                  size="large"
                  v-bind="field"
                  v-model="individualRateFields.publicRate"
                  :validate-event="false"
                  :model-value="value"
                />
              </el-form-item>
            </Field>
          </el-col>
          <el-col :span="8">
            <Field name="extraDiscount" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <label>Descuento extra</label>
                </div>
                <el-input
                  placeholder="Ingresa el descuento extra"
                  size="large"
                  v-bind="field"
                  v-model="individualRateFields.extraDiscount"
                  :validate-event="false"
                  :model-value="value"
                >
                  <template #append>%</template>
                </el-input>
              </el-form-item>
            </Field>
          </el-col>
          <el-col :span="8">
            <Field name="clientRate" v-slot="{ value, field, errorMessage }">
              <el-form-item :error="errorMessage" required>
                <div>
                  <label> Tarifa al cliente </label>
                </div>
                <el-input
                  placeholder="Ingresa la tarifa al cliente"
                  size="large"
                  v-bind="field"
                  v-model="individualRateFields.clientRate"
                  :validate-event="false"
                  :model-value="value"
                />
              </el-form-item>
            </Field>
          </el-col>
        </el-row>
        <el-divider />
      </tab-content>
      <tab-content
        lazy
        title="Cierre reservación"
        icon="bi bi-receipt"
        :beforeChange="validationClosure"
      >
        <el-row :gutter="25" align="middle">
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label>Politicas de cancelación</label>
              </div>
              <el-input
                placeholder="Ingresa el descuento extra"
                size="large"
                v-model="reservationHotel.cancellationPolicy"
                type="textarea"
                :autosize="{ minRows: 4, maxRows: 8 }"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label>Codigo de voucher</label>
              </div>
              <el-input
                placeholder="Ingresa el descuento extra"
                size="large"
                v-model="reservationHotel.codeVoicher"
                type="textarea"
                :autosize="{ minRows: 4, maxRows: 8 }"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationHotel.employeeId"
                :options="employees"
                :reduce="employee => employee.employeeId"
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <span class="text-danger">*</span>
                  <label>Agente emisor</label>
                </template>
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!reservationHotel.employeeId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
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
import ProviderServices from '@/Services/Provider.Services'
import EmployeeServices from '@/Services/Employees.Services'
// Components
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
import HabitationReservationList from '@/views/HabitationReservation/HabitationReservationList'
// Libraries
import { useStore } from 'vuex'
import { ref, inject } from 'vue'
import { useRouter } from 'vue-router'
export default {
  components: {
    CustomersAddNew,
    HabitationReservationList
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
    const { getProviders } = ProviderServices()
    const { getEmployees } = EmployeeServices()
    const store = useStore()
    const redirect = useRouter()
    const swal = inject('$swal')
    const isNewCustomer = ref(false)
    const customers = ref([])
    const destinations = ref([])
    const hotels = ref([])
    const typeReservations = ref([])
    const providers = ref([])
    const employees = ref([])
    const destinationId = ref(null)
    const reservationHotel = ref([])
    const reservationHotelId = ref()
    const reservationHotelFields = ref({
      reservationHotelId: 0,
      isDeleted: false
    })
    const individualRateFields = ref({
      individualRateId: 0,
      reservationHotelId: reservationHotelId.value,
      publicRate: null,
      clientRate: null,
      extraDiscount: null,
      isDeleted: false
    })
    const employyeId = window.sessionStorage.getItem('EmployeeId')

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
    getProviders(data => {
      providers.value = data
    })
    getEmployees(data => {
      employees.value = data
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
    const onUpdateReservation = () => {
      updateReservationHotel(reservationHotel.value, data => {
        reservationHotel.value = data
        refreshReservationHotel()
      })
    }
    const onMessageErrorSteps = () => {
      swal.fire({
        title: 'Datos requeridos',
        text: 'Verifique los datos requeridos con un * e intentelo de nuevo.',
        icon: 'error'
      })
    }
    const onComplete = () => {
      swal
        .fire({
          title: 'Reservación registrada correctamente',
          text: 'La reservación se ha cargado al sistema satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ReservacionesHoteleria')
          }
        })
    }
    const validationClient = () => {
      return new Promise((resolve, reject) => {
        if (reservationHotel.value.customerId) {
          reservationHotel.value.employeeId = employyeId
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationGeneral = () => {
      return new Promise((resolve, reject) => {
        if (
          reservationHotel.value.hotelId &&
          reservationHotel.value.travelDate &&
          reservationHotel.value.typeReservationId &&
          destinationId.value &&
          reservationHotel.value.hotelId &&
          reservationHotel.value.travelDate &&
          reservationHotel.value.providerId &&
          reservationHotel.value.agent &&
          reservationHotel.value.paymentPeriod &&
          reservationHotel.value.paymentLimitDate &&
          reservationHotel.value.paymentLimitDateProvider
        ) {
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationRates = () => {
      return new Promise((resolve, reject) => {
        if (individualRateFields.value.publicRate) {
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationClosure = () => {
      return new Promise((resolve, reject) => {
        if (
          individualRateFields.value.cancellationPolicy &&
          individualRateFields.value.codeVoicher &&
          individualRateFields.value.employeeId
        ) {
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    return {
      reservationHotelFields,
      individualRateFields,
      reservationHotel,
      typeReservations,
      customers,
      destinations,
      hotels,
      providers,
      employees,
      isNewCustomer,
      destinationId,
      onAddedCustomer,
      onGetHotel,
      onComplete,
      validationClient,
      validationGeneral,
      validationRates,
      validationClosure
    }
  }
}
</script>

<style>
.vs__dropdown-toggle {
  height: 40px;
}
</style>
