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
        lazy
        :beforeChange="validationClient"
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
        lazy
        :beforeChange="validationGeneral"
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
                <template #list-footer>
                  <el-button
                    v-if="!reservationHotel.typeReservationId"
                    class="w-100"
                    @click="
                      () => {
                        isAddedTypeReservation = !isAddedTypeReservation
                      }
                    "
                  >
                    agregar nuevo tipo de reserva</el-button
                  >
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationHotel.destinationId"
                :options="destinations"
                :reduce="destination => destination.destinationId"
                @close="onGetHotel(reservationHotel.destinationId)"
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
                    :required="!reservationHotel.destinationId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
                <template #list-footer>
                  <el-button
                    v-if="!reservationHotel.destinationId"
                    class="w-100"
                    @click="
                      () => {
                        isAddDestination = !isAddDestination
                      }
                    "
                  >
                    agregar nuevo destino</el-button
                  >
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
                <template #list-footer>
                  <el-button
                    v-if="!reservationHotel.hotelId"
                    class="w-100"
                    @click="
                      () => {
                        isAddHotel = !isAddHabitation
                      }
                    "
                  >
                    agregar nuevo hotel</el-button
                  >
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
                <template #list-footer>
                  <el-button
                    v-if="!reservationHotel.providerId"
                    class="w-100"
                    @click="
                      () => {
                        isAddProvider = !isAddProvider
                      }
                    "
                  >
                    agregar nuevo promotor</el-button
                  >
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
            <div>
              <label> ¿Desea que se calcule en automatico el costo?</label>
            </div>
            <el-form-item>
              <el-switch size="large" v-model="isAutomaticCalculation">
              </el-switch>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label> Tarifa Pública </label>
              </div>
              <el-input
                placeholder="Ingresa la tarifa pública"
                size="large"
                v-model="individualRate.publicRate"
                @input="onCalculateRate"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <label>Descuento extra</label>
              </div>
              <el-input
                placeholder="Ingresa el descuento extra"
                size="large"
                v-model="individualRate.extraDiscount"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <label> Tarifa al cliente </label>
              </div>
              <el-input
                placeholder="Ingresa la tarifa al cliente"
                size="large"
                v-model="individualRate.clientRate"
              />
            </el-form-item>
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
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label>Costo Total</label>
              </div>
              <el-input
                placeholder="Ingresa el costo total de la reserva"
                size="large"
                v-model="reservationHotel.totalCost"
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
  <type-reservation-add-new />
  <destination-add-new />
  <hotels-add-new />
  <provider-add-new />
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
import IndividualRateServices from '@/Services/IndividualRate.Services'
import CommissionServices from '@/Services/Commissions.Services'
// Components
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
import HabitationReservationList from '@/views/HabitationReservation/HabitationReservationList'
import TypeReservationAddNew from '@/views/TypeReservations/TypeReservationAddNew.vue'
import DestinationAddNew from '@/views/Destinations/DestinationAddNew.vue'
import HotelsAddNew from '@/views/Hotels/HotelsAddNew.vue'
import ProviderAddNew from '@/views/Providers/ProviderAddNew.vue'
// Libraries
import { useStore } from 'vuex'
import { ref, inject, provide, watch } from 'vue'
import { useRouter } from 'vue-router'
export default {
  components: {
    CustomersAddNew,
    HabitationReservationList,
    TypeReservationAddNew,
    DestinationAddNew,
    HotelsAddNew,
    ProviderAddNew
  },
  props: {
    reservationHotelId: {
      type: Number,
      required: true,
      default: null
    }
  },
  setup (props) {
    // SERVICES
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
    const {
      createIndividualRate,
      getIndividualRate,
      getIndividualRateByReservationHotel
    } = IndividualRateServices()
    const { getCommissionByProvider } = CommissionServices()
    const store = useStore()
    const redirect = useRouter()
    // DATA
    const swal = inject('$swal')
    const isAddedTypeReservation = ref(false)
    const isAddDestination = ref(false)
    const isAddHotel = ref(false)
    const isNewCustomer = ref(false)
    const isAutomaticCalculation = ref(true)
    const isAddProvider = ref(false)
    const customers = ref([])
    const destinations = ref([])
    const hotels = ref([])
    const typeReservations = ref([])
    const providers = ref([])
    const employees = ref([])
    const reservationHotel = ref([])
    const individualRate = ref([])
    const reservationHotelId = ref()
    const rangeDatesTravel = ref()
    const employyeId = window.sessionStorage.getItem('EmployeeId')
    provide('AddTypeReservation', isAddedTypeReservation)
    provide('addDestination', isAddDestination)
    provide('addHotel', isAddHotel)
    provide('addProvider', isAddProvider)
    // MODELS DATA
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
    // METHODS
    watch(
      [isAddedTypeReservation, isAddDestination, isAddHotel, isAddProvider],
      ([newValueA, newValueB, newValueC, newValueD]) => {
        if (!newValueA || newValueB || newValueC || newValueD) {
          refreshDataSelect()
        }
      }
    )
    if (props.reservationHotelId === null) {
      createReservationHotel(reservationHotelFields.value, data => {
        reservationHotelId.value = data.reservationHotelId
        store.commit('setReservationHotelId', data.reservationHotelId)
        refreshReservationHotel()
      })
    } else {
      getReservationHotel(props.reservationHotelId, data => {
        store.commit('setReservationHotelId', data.reservationHotelId)
        reservationHotel.value = data
      })
      getIndividualRateByReservationHotel(props.reservationHotelId, data => {
        individualRate.value = data
      })
    }
    const refreshDataSelect = () => {
      getDestinations(data => {
        destinations.value = data
      })
      getTypeReservations(data => {
        typeReservations.value = data
      })
      getProviders(data => {
        providers.value = data
      })
      getHotelByDestinationId(reservationHotel.value.destinationId, data => {
        hotels.value = data
      })
    }

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
      if (destinationId !== null || destinationId !== 0) {
        getHotelByDestinationId(destinationId, data => {
          hotels.value = data
        })
      }
    }
    const refreshReservationHotel = () => {
      getReservationHotel(
        reservationHotelId.value || props.reservationHotelId,
        data => {
          reservationHotel.value = data
        }
      )
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
    const onSelectTravelDate = () => {
      reservationHotel.value.travelDateStart = rangeDatesTravel.value[0]
      reservationHotel.value.travelDateEnd = rangeDatesTravel.value[1]
    }
    const onCalculateRate = () => {
      if (isAutomaticCalculation.value) {
        const commissionPercentage = ref()
        const commission = ref()
        const commissionClient = ref()
        getCommissionByProvider(reservationHotel.value.providerId, data => {
          commissionPercentage.value = parseFloat(
            data.commissionClient
          ).toFixed(2)
          commission.value =
            individualRate.value.publicRate * commissionPercentage.value
          commissionClient.value =
            individualRate.value.publicRate - commission.value
          individualRate.value.clientRate = commissionClient.value
        })
      }
    }
    // VALIDATIONS
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
          reservationHotel.value.typeReservationId &&
          reservationHotel.value.destinationId &&
          reservationHotel.value.hotelId &&
          reservationHotel.value.travelDateStart &&
          reservationHotel.value.travelDateEnd &&
          reservationHotel.value.providerId &&
          reservationHotel.value.agent &&
          reservationHotel.value.paymentPeriod &&
          reservationHotel.value.paymentLimitDate &&
          reservationHotel.value.paymentLimitDateProvider
        ) {
          onUpdateReservation()
          if (!reservationHotel.value.typeReservationId !== 1 && individualRate.value.length > 0) {
            createIndividualRate(individualRateFields.value, data => {
              getIndividualRate(data.individualRateId, items => {
                individualRate.value = items
              })
            })
          }
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationRates = () => {
      return new Promise((resolve, reject) => {
        if (individualRate.value.publicRate) {
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
          reservationHotel.value.cancellationPolicy &&
          reservationHotel.value.codeVoicher &&
          reservationHotel.value.employeeId
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
      individualRate,
      isNewCustomer,
      isAutomaticCalculation,
      rangeDatesTravel,
      isAddedTypeReservation,
      isAddDestination,
      isAddHotel,
      isAddProvider,
      onAddedCustomer,
      onGetHotel,
      onComplete,
      refreshDataSelect,
      onSelectTravelDate,
      onCalculateRate,
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
