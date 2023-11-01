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
                    Agregar nuevo tipo de reserva</el-button
                  >
                </template>
              </v-select>
            </el-form-item>
          </el-col>
          <el-col :span="8" v-if="reservationHotel.typeReservationId === 2">
            <el-form-item>
              <v-select
                class="w-100"
                label="name"
                v-model="reservationHotel.typeReservationGroupId"
                :options="typeReservationsgrupal"
                :reduce="
                  typeReservationGroup =>
                    typeReservationGroup.typeReservationGrupalId
                "
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <span class="text-danger">*</span>
                  <label>Tipo de reserva grupal</label>
                </template>
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!reservationHotel.typeReservationGroupId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
                <template #list-footer>
                  <el-button
                    v-if="!reservationHotel.typeReservationGroupId"
                    class="w-100"
                    @click="
                      () => {
                        isAddedTypeReservation = !isAddedTypeReservation
                      }
                    "
                  >
                    Agregar nuevo tipo de reserva</el-button
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
      <tab-content
        lazy
        title="Tarifas Y Habitaciones"
        icon="bi bi-door-closed"
        :beforeChange="validationRatesAndHabitations"
      >
        <el-tabs v-if="reservationHotel.typeReservationGroupId !== 1">
          <el-tab-pane label="Habitaciones">
            <habitation-reservation-list />
          </el-tab-pane>
          <el-tab-pane>
            <template #label>
              <span class="custom-tabs-label">
                <i class="bi bi-building"></i>
                <span>Tarifas </span>
              </span>
            </template>
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
              </el-col> </el-row
          ></el-tab-pane>
        </el-tabs>
        <el-row
          :gutter="35"
          v-if="reservationHotel.typeReservationGroupId === 1"
        >
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label> Nombre del grupo </label>
              </div>
              <el-input
                placeholder="Ingresa un nombre para el grupo"
                size="large"
                v-model="reservationHotelGroup.groupName"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label> Confirmación </label>
              </div>
              <el-input
                placeholder="Ingresa una clave de confirmación"
                size="large"
                v-model="reservationHotelGroup.confirmationKey"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <div class="mb-2">
              <span class="text-danger">*</span>
              <span>Fecha y hora de llegada</span>
            </div>
            <el-form-item>
              <VueDatePicker
                v-model="reservationHotelGroup.dateArrival"
                placeholder="Seleccionar ..."
                selectText="Seleccionar"
                cancelText="Cancelar"
                modelType="yyyy-MM-dd HH:mm"
                @update:model-value="
                  onSelectedDateArrival(reservationHotelGroup.dateArrival)
                "
              >
                <template #input-icon>
                  <el-row class="m-3">
                    <i class="bi bi-calendar-event" />
                  </el-row>
                </template>
              </VueDatePicker>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label> Coordinador de grupo </label>
              </div>
              <el-input
                placeholder="Ingresa una clave de confirmación"
                size="large"
                v-model="reservationHotelGroup.coordinator"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item>
              <div>
                <span class="text-danger">*</span>
                <label> Numero de contacto </label>
              </div>
              <el-input
                placeholder="Ingresa una clave de confirmación"
                size="large"
                v-model="reservationHotelGroup.phoneNumber"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row v-if="reservationHotel.typeReservationGroupId === 1">
          <el-col :md="24" :lg="24">
            <group-rate-list
              :ReservationHotelGroupId="reservationHotelGroupId"
            />
          </el-col>
        </el-row>
      </tab-content>
      <tab-content lazy title="Servicios adicionales" icon="bi bi-receipt">
        <ReservationHotelsAditionalServiceList />
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
                disabled
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
import TypeReservationGroupServices from '@/Services/TypeReservationGroup.Services'
import ProviderServices from '@/Services/Provider.Services'
import EmployeeServices from '@/Services/Employees.Services'
import IndividualRateServices from '@/Services/IndividualRate.Services'
import ServicesProviderServices from '@/Services/ProviderServices.Services'
import ReservationHotelGroupServices from '@/Services/ReservationHotelGroup.Services'
import PaymentsRelationReservationServices from '@/Services/PaymentRelationReservationHotel.Services'
// Components
import CustomersAddNew from '@/views/Customers/CustomersAddNew'
import HabitationReservationList from '@/views/HabitationReservation/HabitationReservationList'
import TypeReservationAddNew from '@/views/TypeReservations/TypeReservationAddNew.vue'
import DestinationAddNew from '@/views/Destinations/DestinationAddNew.vue'
import HotelsAddNew from '@/views/Hotels/HotelsAddNew.vue'
import ProviderAddNew from '@/views/Providers/ProviderAddNew.vue'
import GroupRateList from '@/views/Rates/GroupRate/GroupRateList.vue'
import ReservationHotelsAditionalServiceList from '../ReservationHotels/ReservationHotelsAditionalServices/ReservationHotelsAditionalServiceList.vue'
// Libraries
import { useStore } from 'vuex'
import { ref, inject, provide, watch } from 'vue'
import { useRouter } from 'vue-router'
import { format } from 'date-fns'

export default {
  components: {
    CustomersAddNew,
    HabitationReservationList,
    TypeReservationAddNew,
    DestinationAddNew,
    HotelsAddNew,
    ProviderAddNew,
    GroupRateList,
    ReservationHotelsAditionalServiceList
  },
  props: {
    reservationHotelId: {
      type: Number,
      required: false,
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
    const { getTypeReservationGrupals } = TypeReservationGroupServices()
    const { getProviders } = ProviderServices()
    const { getEmployees } = EmployeeServices()
    const {
      createIndividualRate,
      getIndividualRate,
      getIndividualRateByReservationHotel,
      updateIndividualRate
    } = IndividualRateServices()
    const { getServiceProviderByProviderId } = ServicesProviderServices()
    const {
      getReservationHotelGroupByreservationHotel,
      createReservationHotelGroup,
      updateReservationHotelGroup,
      comprobateIfExist
    } = ReservationHotelGroupServices()
    const {
      createPaymentRelation,
      getPaymentRelation,
      updatePaymentRelation,
      getPaymentsRelationByReservationHotel
    } = PaymentsRelationReservationServices()
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
    const typeReservationsgrupal = ref([])
    const providers = ref([])
    const employees = ref([])
    const reservationHotel = ref([])
    const individualRate = ref([])
    const reservationHotelId = ref()
    const rangeDatesTravel = ref([])
    const reservationHotelGroup = ref([])
    const employyeId = window.sessionStorage.getItem('EmployeeId')
    const reservationHotelGroupId = ref(0)
    const paymentReservationId = ref(0)
    let dateArrival = new Date()
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
      reservationHotelId: null,
      publicRate: null,
      clientRate: null,
      extraDiscount: null,
      isDeleted: false
    })
    const reservationHotelGroupfields = ref({
      reservationHotelGroupId: 0,
      groupName: null,
      confirmationKey: null,
      dateArrival: null,
      coordinator: null,
      phoneNumber: null,
      reservationHotelId: reservationHotelId.value || props.reservationHotelId,
      isDeleted: false
    })
    // METHODS
    watch(
      [isAddedTypeReservation, isAddDestination, isAddHotel, isAddProvider],
      ([newValueA, newValueB, newValueC, newValueD]) => {
        if (!newValueA || !newValueB || !newValueC || !newValueD) {
          refreshDataSelect()
        }
      }
    )
    if (props.reservationHotelId === null) {
      createReservationHotel(reservationHotelFields.value, data => {
        reservationHotelId.value = data.reservationHotelId
        store.commit('setReservationHotelId', data.reservationHotelId)
        createPaymentRelation(
          {
            amountTotal: null,
            amountMissing: null,
            reservationHotelId: data.reservationHotelId,
            statusPaymentRelationId: 1,
            isDeleted: false
          },
          data => {
            paymentReservationId.value = data.paymentReservationId
          }
        )
        refreshReservationHotel()
      })
    } else {
      getReservationHotel(props.reservationHotelId, data => {
        store.commit('setReservationHotelId', data.reservationHotelId)
        reservationHotel.value = data
        rangeDatesTravel.value.push(data.travelDateStart)
        rangeDatesTravel.value.push(data.travelDateEnd)
        getPaymentsRelationByReservationHotel(data.reservationHotelId, data => {
          paymentReservationId.value = data.paymentReservationId
        })
        if (data.destinationId) {
          getHotelByDestinationId(data.destinationId, data => {
            hotels.value = data
          })
        }
        if (data.typeReservationId === 2 && data.typeReservationGroupId === 1) {
          getReservationHotelGroupByreservationHotel(
            props.reservationHotelId,
            data => {
              reservationHotelGroup.value = data
              reservationHotelGroup.value.dateArrival = format(
                new Date(reservationHotelGroup.value.dateArrival),
                'yyyy-MM-dd HH:mm'
              )
            }
          )
          comprobateIfExist(data.reservationHotelId, data => {
            const { status } = data
            if (status === 404) {
              createReservationHotelGroup(
                reservationHotelGroupfields.value,
                data => {
                  getReservationHotelGroupByreservationHotel(
                    props.reservationHotelId,
                    data => {
                      reservationHotelGroup.value = data
                      reservationHotelGroup.value.dateArrival = format(
                        new Date(reservationHotelGroup.value.dateArrival),
                        'yyyy-MM-dd HH:mm'
                      )
                    }
                  )
                }
              )
            }
          })
        }
      })
      getIndividualRateByReservationHotel(props.reservationHotelId, data => {
        if (data.individualRateId) {
          individualRate.value = data
        } else {
          individualRateFields.value.reservationHotelId = reservationHotelId.value || props.reservationHotelId
          createIndividualRate(individualRateFields.value, data => {
            getIndividualRate(data.individualRateId, items => {
              individualRate.value = items
            })
          })
        }
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
      if (reservationHotel.value.destinationId !== null) {
        getHotelByDestinationId(reservationHotel.value.destinationId, data => {
          hotels.value = data
        })
      }
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
    getTypeReservationGrupals(data => {
      typeReservationsgrupal.value = data
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
      if (reservationHotel.value.destinationId !== null) {
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
        getServiceProviderByProviderId(
          reservationHotel.value.providerId,
          data => {
            commissionPercentage.value = parseFloat(
              data.commissionClient
            ).toFixed(2)
            commission.value =
              individualRate.value.publicRate *
              (commissionPercentage.value / 100)
            commissionClient.value =
              individualRate.value.publicRate - commission.value
            individualRate.value.clientRate = commissionClient.value
          }
        )
      }
    }
    const onSelectedDateArrival = modelData => {
      dateArrival = new Date(modelData)
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
          if (
            reservationHotel.value.typeReservationId === 2 &&
            reservationHotel.value.typeReservationGroupId === 1
          ) {
            comprobateIfExist(
              reservationHotelId.value || props.reservationHotelId,
              data => {
                const { status } = data
                if (status === 404) {
                  reservationHotelGroupfields.value.reservationHotelId =
                    reservationHotelId.value
                  createReservationHotelGroup(
                    reservationHotelGroupfields.value,
                    data => {
                      reservationHotelGroupId.value =
                        data.reservationHotelGroupId
                      getReservationHotelGroupByreservationHotel(
                        reservationHotelId.value,
                        data => {
                          reservationHotelGroup.value = data
                          reservationHotelGroup.value.dateArrival = format(
                            new Date(reservationHotelGroup.value.dateArrival),
                            'yyyy-MM-dd HH:mm'
                          )
                        }
                      )
                    }
                  )
                }
              }
            )
          } else {
            if (!individualRate.value.individualRateId) {
              individualRateFields.value.reservationHotelId = reservationHotelId.value || props.reservationHotelId
              createIndividualRate(individualRateFields.value, data => {
                getIndividualRate(data.individualRateId, items => {
                  individualRate.value = items
                })
              })
            }
          }
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationRatesAndHabitations = () => {
      return new Promise((resolve, reject) => {
        if (reservationHotel.value.typeReservationGroupId !== 1) {
          if (
            individualRate.value.publicRate &&
            individualRate.value.clientRate
          ) {
            onUpdateReservation()
            updateIndividualRate(
              {
                individualRateId: individualRate.value.individualRateId,
                reservationHotelId: individualRate.value.reservationHotelId,
                publicRate: individualRate.value.publicRate,
                clientRate: individualRate.value.clientRate,
                extraDiscount: individualRate.value.extraDiscount,
                isDeleted: false
              },
              data => {}
            )
            resolve(true)
          } else {
            onMessageErrorSteps()
            reject(new Error('Error'))
          }
        } else {
          if (reservationHotelGroup.value.groupName) {
            reservationHotelGroup.value.dateArrival = dateArrival.toISOString()
            updateReservationHotelGroup(reservationHotelGroup.value, data => {})
            resolve(true)
          } else {
            onMessageErrorSteps()
            reject(new Error('Error'))
          }
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
          getPaymentRelation(paymentReservationId.value, data => {
            data.amountTotal = reservationHotel.value.totalCost
            updatePaymentRelation(data, response => {})
          })
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
      typeReservationsgrupal,
      customers,
      destinations,
      hotels,
      providers,
      employees,
      individualRate,
      reservationHotelGroup,
      isNewCustomer,
      isAutomaticCalculation,
      rangeDatesTravel,
      isAddedTypeReservation,
      isAddDestination,
      isAddHotel,
      isAddProvider,
      reservationHotelGroupId,
      onAddedCustomer,
      onGetHotel,
      onComplete,
      refreshDataSelect,
      onSelectTravelDate,
      onSelectedDateArrival,
      onCalculateRate,
      validationClient,
      validationGeneral,
      validationRatesAndHabitations,
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
