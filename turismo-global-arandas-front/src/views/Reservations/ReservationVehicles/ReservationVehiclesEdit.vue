<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateCommission">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="invoice" :rules="validateInvoice">
            <el-form-item :errors="errors.invoice" required>
              <div>
                <label> Folio </label>
              </div>
              <el-input
                placeholder="Ingresa el folio de la reservación"
                size="large"
                v-model="reservationVehicle.invoice"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="dateTravel" :rules="validateDateTravel">
            <el-form-item :error="errors.dateTravel">
              <div>
                <label>Fecha de reservación</label>
              </div>
              <el-date-picker
                v-model="reservationVehicle.dateTravel"
                class="w-100"
                type="date"
                placeholder="Selecciona la fecha del viaje"
                size="large"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="departureLocation" :rules="validateDepartureLocation">
            <el-form-item :error="errors.departureLocation">
              <div>
                <label> Lugar de salida </label>
              </div>
              <el-input
                placeholder="Ingresa el lugar de donde saldrá el vehiculo"
                size="large"
                v-model="reservationVehicle.departureLocation"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="arrivalLocation" :rules="validateArrivalLocation">
            <el-form-item :error="errors.arrivalLocation">
              <div>
                <label>Lugar de llegada</label>
              </div>
              <el-input
                placeholder="Ingresa el lugar de llegada"
                size="large"
                v-model="reservationVehicle.arrivalLocation"
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
              v-model="reservationVehicle.providerId"
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
                  :required="!reservationVehicle.providerId"
                  v-bind="attributes"
                  v-on="events"
                />
              </template>
              <template #list-footer>
                <el-button
                  v-if="!reservationVehicle.providerId"
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
          <Field name="pricePublic" :rules="validatePricePublic">
            <el-form-item :error="errors.pricePublic">
              <div>
                <label>Precio al público</label>
              </div>
              <el-input
                v-model="reservationVehicle.pricePublic"
                size="large"
                placeholder="Ingresa el precio al público"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="priceNeto" :rules="validatePriceNeto">
            <el-form-item :error="errors.priceNeto">
              <div>
                <label>Precio neto</label>
              </div>
              <el-input
                placeholder="Ingresa el precio neto"
                size="large"
                v-model="reservationVehicle.priceNeto"
                type="number"
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
              v-model="reservationVehicle.description"
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
                $router.push('/ReservacionesVehiculos')
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
import ReservationVehicleServices from '@/Services/ReservationVehicle.Services'
import ProviderServices from '@/Services/Provider.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getReservationVehicle, updateReservationVehicle } =
      ReservationVehicleServices()
    const { getProviders } = ProviderServices()
    const providers = ref([])
    const reservationVehicle = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getReservationVehicle(router.params.ReservationVehicleId, data => {
      reservationVehicle.value = data
    })
    getProviders(data => {
      providers.value = data
    })
    const onUpdateCommission = () => {
      updateReservationVehicle(reservationVehicle.value, data => {
        swal
          .fire({
            title: 'vuelo modificado correctamente',
            text: 'El vuelo se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ReservacionesVehiculos')
            }
          })
      })
    }
    const validateInvoice = () => {
      if (!reservationVehicle.value.invoice) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateDateTravel = () => {
      if (!reservationVehicle.value.dateTravel) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateDepartureLocation = () => {
      if (!reservationVehicle.value.departureLocation) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateArrivalLocation = () => {
      if (!reservationVehicle.value.arrivalLocation) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePricePublic = () => {
      if (!reservationVehicle.value.pricePublic) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePriceNeto = () => {
      if (!reservationVehicle.value.priceNeto) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      reservationVehicle,
      providers,
      onUpdateCommission,
      validateInvoice,
      validateDateTravel,
      validateDepartureLocation,
      validateArrivalLocation,
      validatePricePublic,
      validatePriceNeto
    }
  }
}
</script>

<style></style>
