<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateCommission">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="travelDate">
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
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="departureAirport">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Aeropuerto de salida </label>
              </div>
              <el-input
                placeholder="Ingresa la comisión para la agencia"
                size="large"
                v-model="reservationFlightFields.departureAirport"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="arrivalAirport">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Aeropuerto de salida </label>
              </div>
              <el-input
                placeholder="Ingresa la comision para el cliente"
                size="large"
                v-model="reservationFlightFields.arrivalAirport"
              >
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="airline">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>AeroLinea</label>
              </div>
              <el-input
                placeholder="Ingresa la aerolinea en la que viajará el cliente"
                size="large"
                v-model="reservationFlightFields.airline"
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
          <Field name="confirmationKey">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Clave de confirmación</label>
              </div>
              <el-input
                v-model="reservationFlightFields.confirmationKey"
                size="large"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="priceNeto">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Precio neto</label>
              </div>
              <el-input
                placeholder="Ingresa el precio neto"
                size="large"
                v-model="reservationFlightFields.priceNeto"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="pricePublic">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Precio neto</label>
              </div>
              <el-input
                placeholder="Ingresa el precio neto"
                size="large"
                v-model="reservationFlightFields.pricePublic"
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
          <Field name="paymentMethodAgency">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Metodo de pago de la agencia</label>
              </div>
              <el-input
                placeholder="Ingresa una descripcion"
                size="large"
                v-model="reservationFlightFields.paymentMethodAgency"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="paymentMethodClient">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Metodo de pago del cliente</label>
              </div>
              <el-input
                placeholder="Ingresa una descripcion"
                size="large"
                v-model="reservationFlightFields.paymentMethodClient"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="contactPhone">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Telefono de contacto</label>
              </div>
              <el-input
                placeholder="Ingresa el telefono de contacto"
                size="large"
                v-model="reservationFlightFields.contactPhone"
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
                $router.push('/comisiones')
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
import CommissionServices from '@/Services/Commissions.Services'
import ProviderServices from '@/Services/Provider.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getCommission, updateCommission } = CommissionServices()
    const { getProviders } = ProviderServices()
    const providers = ref([])
    const commission = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getCommission(router.params.CommissionId, data => {
      commission.value = data
    })
    getProviders(data => {
      providers.value = data
    })
    const onUpdateCommission = () => {
      updateCommission(commission.value, data => {
        swal
          .fire({
            title: 'Comisión modificado correctamente',
            text: 'La comision se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/comisiones')
            }
          })
      })
    }
    const validateCommissionAgency = () => {
      if (!commission.value.commissionAgency) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCommissionClient = () => {
      if (!commission.value.commissionClient) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCommissionEmployee = () => {
      if (!commission.value.commissionEmployee) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateColor = () => {
      if (!commission.value.color) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateProvider = () => {
      if (!commission.value.providerId) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      commission,
      providers,
      onUpdateCommission,
      validateCommissionAgency,
      validateCommissionClient,
      validateCommissionEmployee,
      validateColor,
      validateProvider
    }
  }
}
</script>

<style></style>
