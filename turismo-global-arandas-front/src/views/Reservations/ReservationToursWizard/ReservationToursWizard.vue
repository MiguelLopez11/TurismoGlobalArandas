<template>
  <el-card>
    <!-- wizard STEPS -->
    <form-wizard
      nextButtonText="Siguiente"
      backButtonText="Atras"
      doneButtonText="Finalizar"
      color="#7367F0"
    >
      <!-- STEP 1 -->
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
      <!-- STEP 2 -->
      <tab-content title="Datos del cliente" icon="bi bi-person-circle" lazy>
        en proceso
      </tab-content>
      <!-- STEP 3 -->
      <tab-content title="Cierre de reserva" icon="bi bi-filter-square" lazy>
        en proceso
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
// COMPONENTS
import DestinationAddNew from '@/views/Destinations/DestinationAddNew.vue'
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
    return {
      reservationTour,
      destinations,
      isAddDestination,
      validationGeneralData
    }
  }
}
</script>

<style>
.vs--searchable .vs__dropdown-toggle {
  height: 40px;
}
</style>
