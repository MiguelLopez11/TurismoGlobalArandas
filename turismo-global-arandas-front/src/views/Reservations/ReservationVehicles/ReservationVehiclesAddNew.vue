<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Nueva reservation por transporte vehicular"
    width="80%"
    center
  >
    <Form
      ref="reservationVehicleFormRef"
      as="el-form"
      :validation-schema="validationSchema"
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
          <Field name="arrivalLocation" v-slot="{ value, field, errorMessage }">
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
  </el-dialog>
</template>

<script>
import { ref, inject } from 'vue'
import { Field, Form } from 'vee-validate'
import ProviderServices from '@/Services/Provider.Services'
import ReservationVehicleServices from '@/Services/ReservationVehicle.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('AddReservationVehicle')
    const swal = inject('$swal')
    const reservationVehicleFormRef = ref(null)
    const providers = ref([])
    const { getProviders } = ProviderServices()
    const { createReservationVehicle } = ReservationVehicleServices()
    const employeeId = parseInt(window.sessionStorage.getItem('EmployeeId'))
    const validationSchema = yup.object({
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
      employeeId: employeeId,
      isDeleted: false
    })
    const reservationVehiclesFieldsBlank = ref(
      JSON.parse(JSON.stringify(reservationVehiclesFields))
    )
    getProviders(data => {
      providers.value = data
    })
    const onSubmit = () => {
      createReservationVehicle(reservationVehiclesFields.value, data => {
        swal.fire({
          title: '¡Nueva comisión registrada!',
          text: 'La nueva comisión se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        reservationVehiclesFields.value = JSON.parse(
          JSON.stringify(reservationVehiclesFieldsBlank)
        )
        reservationVehicleFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      reservationVehicleFormRef,
      providers,
      onSubmit,
      reservationVehiclesFields,
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
