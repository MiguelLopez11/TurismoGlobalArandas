<template>
  <el-card>
    <!--------------------------------------------------------------
      -------------------------WIZARD STEPS-----------------------------
      ------------------------------------------------------------ -->
    <form-wizard
      nextButtonText="Siguiente"
      backButtonText="Atras"
      doneButtonText="Finalizar"
      color="#7367F0"
      @on-complete="onComplete"
    >
      <!-- ------------------------------------------------------------
      -------------------------STEP 1-----------------------------
      ------------------------------------------------------------ -->
      <tab-content
        title="Datos generales"
        icon="bi bi-clipboard2-pulse"
        lazy
        :beforeChange="validationGeneralData"
      >
        <el-card>
          <el-row :gutter="35">
            <el-col :span="8">
              <el-form-item>
                <div>
                  <span class="text-danger">*</span>
                  <label> Nombre del tour </label>
                </div>
                <el-input
                  placeholder="Ingresa el nombre de el agente "
                  size="large"
                  v-model="reservationTour.tourName"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <v-select
                  class="w-100"
                  label="name"
                  v-model="reservationTour.destinationId"
                  :options="destinations"
                  :reduce="destination => destination.destinationId"
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
                      :required="!reservationTour.destinationId"
                      v-bind="attributes"
                      v-on="events"
                    />
                  </template>
                  <template #list-footer>
                    <el-button
                      v-if="!reservationTour.destinationId"
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
              <div class="mb-2">
                <span class="text-danger">*</span>
                <span>Fecha de reservación</span>
              </div>
              <el-form-item>
                <el-date-picker
                  v-model="reservationTour.dateActivity"
                  class="w-100"
                  type="date"
                  placeholder="Selecciona la fecha limite del pago"
                  size="large"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-card>
      </tab-content>
      <!-- ------------------------------------------------------------
      -------------------------STEP 2-----------------------------
      ------------------------------------------------------------ -->
      <tab-content
        title="Datos del cliente"
        icon="bi bi-person-circle"
        lazy
        :beforeChange="validationClientData"
      >
        <el-card>
          <el-row :gutter="35">
            <el-col :span="8">
              <el-form-item>
                <div>
                  <span class="text-danger">*</span>
                  <label> Nombre del titular </label>
                </div>
                <el-input
                  placeholder="Ingresa el nombre de el agente "
                  size="large"
                  v-model="reservationTour.ownerName"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <div>
                  <label>Adultos</label>
                </div>
                <el-input
                  placeholder="Ingresa el numero de adultos que viajarán"
                  size="large"
                  v-model="reservationTour.numberAdults"
                  type="number"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <div>
                  <label>Menores</label>
                </div>
                <el-input
                  placeholder="Ingresa el numero de menores que viajarán"
                  size="large"
                  v-model="reservationTour.numberMinors"
                  type="number"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <div>
                  <label>costo por menor</label>
                </div>
                <el-input
                  placeholder="Ingresa el costo por menor"
                  v-model="reservationTour.minorCost"
                  type="number"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <div>
                  <label>Costo por adulto</label>
                </div>
                <el-input
                  placeholder="Ingresa el costo por adulto"
                  size="large"
                  v-model="reservationTour.adultCost"
                  type="number"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <div>
                <label> ¿Incluye transporte?</label>
              </div>
              <el-form-item>
                <el-switch
                  size="large"
                  v-model="reservationTour.includeTransportation"
                >
                </el-switch>
              </el-form-item>
            </el-col>
          </el-row>
        </el-card>
      </tab-content>
      <!-- ------------------------------------------------------------
      -------------------------STEP 3-----------------------------
      ------------------------------------------------------------ -->
      <tab-content
        title="Cierre de reserva"
        icon="bi bi-filter-square"
        lazy
        :beforeChange="validationCloseReservation"
      >
        <el-card>
          <el-row :gutter="35">
            <el-col :span="8">
              <el-form-item>
                <div>
                  <label>Precio al público</label>
                </div>
                <el-input
                  placeholder="Ingresa el numero de adultos que viajarán"
                  size="large"
                  v-model="reservationTour.publicRate"
                  type="number"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <div>
                  <label>Precio neto</label>
                </div>
                <el-input
                  placeholder="Ingresa el numero de adultos que viajarán"
                  size="large"
                  v-model="reservationTour.netPrice"
                  type="number"
                />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <div>
                <label> ¿Es un tour nacional?</label>
              </div>
              <el-form-item>
                <el-switch
                  size="large"
                  v-model="reservationTour.isNational"
                  :disabled="reservationTour.isInternational"
                >
                </el-switch>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <div>
                <label> ¿Es un tour internacional?</label>
              </div>
              <el-form-item>
                <el-switch
                  size="large"
                  v-model="reservationTour.isInternational"
                  :disabled="reservationTour.isNational"
                >
                </el-switch>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item>
                <div>
                  <span class="text-danger">*</span>
                  <label> Tipo de cambio </label>
                </div>
                <el-input
                  placeholder="Ingresa el nombre de el agente "
                  size="large"
                  v-model="reservationTour.exchangeRate"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-card>
      </tab-content>
    </form-wizard>
  </el-card>
  <destination-add-new />
</template>

<script>
import { ref, provide, watch, inject } from 'vue'
// SERVICES
import ReservationTourServices from '@/Services/ReservationTours.Services'
import DestinationServices from '@/Services/Destinations.Services'
import PaymentsRelationReservationServices from '@/Services/PaymentRelationReservationHotel.Services'
// COMPONENTS
import DestinationAddNew from '@/views/Destinations/DestinationAddNew.vue'
// LIBRARIES
import { useRouter } from 'vue-router'
export default {
  props: {
    ReservationTourId: {
      type: Number,
      default: null
    }
  },
  components: {
    DestinationAddNew
  },
  setup (props) {
    const { getReservationTour, createReservationTour, updateReservationTour } =
      ReservationTourServices()
    const { getDestinations } = DestinationServices()
    const { createPaymentRelation } = PaymentsRelationReservationServices()
    const redirect = useRouter()
    //   DATA
    const reservationTour = ref([])
    const destinations = ref([])
    const isAddDestination = ref(false)
    const reservationTourId = ref(0)
    // OPEN CLOSE COMPONENT
    provide('addDestination', isAddDestination)
    const swal = inject('$swal')
    // WATCH
    watch(isAddDestination, newValue => {
      if (!newValue) {
        refreshDataSelect()
      }
    })
    // Fields to create reservation Tour blank
    const reservationTourFields = ref({
      reservationTourId: 0,
      isInternational: false,
      isNational: false,
      includeTransportation: false,
      isDeleted: false
    })
    // if im not editing, create a new reservation else, get a reservationTour from the API
    if (!props.ReservationTourId) {
      createReservationTour(reservationTourFields.value, data => {
        getReservationTour(data.reservationTourId, data => {
          reservationTour.value = data
          reservationTourId.value = data.reservationTourId
          createPaymentRelation(
            {
              amountTotal: null,
              amountMissing: null,
              reservationTourId: data.reservationTourId,
              statusPaymentRelationId: 1,
              isDeleted: false
            },
            data => {
              console.log('relacion de pagos creado')
            }
          )
        })
      })
    } else {
      getReservationTour(props.ReservationTourId, data => {
        reservationTour.value = data
        reservationTourId.value = data.reservationTourId
      })
    }
    // METHODS
    getDestinations(data => {
      destinations.value = data
    })
    const refreshReservationTour = () => {
      getReservationTour(
        props.ReservationTourId || reservationTourId.value,
        data => {
          reservationTour.value = data
        }
      )
    }
    const refreshDataSelect = () => {
      getDestinations(data => {
        destinations.value = data
      })
    }
    const onUpdateReservation = () => {
      updateReservationTour(reservationTour.value, data => {
        reservationTour.value = data
        refreshReservationTour()
      })
    }
    const onMessageErrorSteps = () => {
      swal.fire({
        title: 'Datos requeridos',
        text: 'Verifique los datos requeridos con un * e intentelo de nuevo.',
        icon: 'error'
      })
    }
    // VALIDATIONS PROMISES
    const validationGeneralData = () => {
      return new Promise((resolve, reject) => {
        if (
          reservationTour.value.tourName &&
          reservationTour.value.destinationId &&
          reservationTour.value.dateActivity
        ) {
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationClientData = () => {
      return new Promise((resolve, reject) => {
        if (
          reservationTour.value.ownerName &&
          reservationTour.value.numberAdults &&
          reservationTour.value.numberMinors &&
          reservationTour.value.minorCost &&
          reservationTour.value.adultCost
        ) {
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
      })
    }
    const validationCloseReservation = () => {
      return new Promise((resolve, reject) => {
        if (
          reservationTour.value.publicRate &&
          reservationTour.value.netPrice
        ) {
          onUpdateReservation()
          resolve(true)
        } else {
          onMessageErrorSteps()
          reject(new Error('Error'))
        }
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
            redirect.push('/ReservacionesTours')
          }
        })
    }
    return {
      reservationTour,
      destinations,
      isAddDestination,
      validationGeneralData,
      validationClientData,
      validationCloseReservation,
      onComplete
    }
  }
}
</script>

<style>
.vs--searchable .vs__dropdown-toggle {
  height: 40px;
}
</style>
