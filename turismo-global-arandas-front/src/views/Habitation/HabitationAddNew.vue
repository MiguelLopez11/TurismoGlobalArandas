<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Registrar habitación"
    width="80%"
    center
  >
    <Form
      ref="habitationFormRef"
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
                placeholder="Ingresa el Folio de la habitación"
                size="large"
                v-bind="field"
                v-model="habitationFields.invoice"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="typehabitation" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Tipo de habitación </label>
              </div>
              <el-input
                placeholder="Ingresa el tipo de habitación"
                size="large"
                v-bind="field"
                v-model="habitationFields.typeHabitation"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="advancePayment" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Anticipo </label>
              </div>
              <el-input
                placeholder="Ingresa el monto del anticipo"
                size="large"
                v-bind="field"
                v-model="habitationFields.advancePayment"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="ages" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Edades</label>
              </div>
              <el-input
                placeholder="Ingresa las edades de todos"
                size="large"
                v-bind="field"
                v-model="habitationFields.ages"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="adults" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Adultos</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de adultos que viajarán"
                size="large"
                v-bind="field"
                v-model="habitationFields.adults"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="minors" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Menores</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de adultos que viajarán"
                size="large"
                v-bind="field"
                v-model="habitationFields.minors"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="purchaseDate" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Fecha de compra</label>
              </div>
              <el-date-picker
                v-model="habitationFields.purchaseDate"
                class="w-100"
                type="date"
                placeholder="Selecciona una fecha"
                size="large"
                v-bind="field"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="cost" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Costo</label>
              </div>
              <el-input
                placeholder="Ingresa el costo de la habitación"
                size="large"
                v-bind="field"
                v-model="habitationFields.cost"
                :validate-event="false"
                :model-value="value"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="observations" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Observaciones</label>
              </div>
              <el-input
                placeholder="Ingresa las observaciones de la habitación"
                size="large"
                v-bind="field"
                v-model="habitationFields.observations"
                :validate-event="false"
                :model-value="value"
                 type="textarea"
                :autosize="{ minRows: 4, maxRows: 8 }"
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
  </el-dialog>
</template>

<script>
import { ref, inject, computed } from 'vue'
import { Field, Form } from 'vee-validate'
import HabitationServices from '@/Services/Habitation.Services'
import HabitationReservationServices from '@/Services/HabitationReservation.Services'
import { useStore } from 'vuex'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup (props) {
    const isOpenDialog = inject('addHabitation')
    const store = useStore()
    const swal = inject('$swal')
    const habitationFormRef = ref(null)
    const { createHabitation } = HabitationServices()
    const { createHabitationReservation } = HabitationReservationServices()
    const reservationHotelId = computed(
      () => store.getters.getReservationHotelId
    )
    const validationSchema = yup.object({
      invoice: yup.string().required('Este campo es requerido').label('Folio'),
      ages: yup.string().required('Este campo es requerido').label('Edades'),
      adults: yup.string().required('Este campo es requerido').label('Adultos'),
      minors: yup.string().required('Este campo es requerido').label('Menores'),
      purchaseDate: yup
        .date()
        .required('Este campo es requerido')
        .label('Fecha de compra'),
      cost: yup.number().required('Este campo es requerido').label('Costo')
    })
    const habitationFields = ref({
      habitationId: 0,
      invoice: null,
      typeHabitation: null,
      advancePayment: null,
      ages: null,
      adults: null,
      minors: null,
      purchaseDate: null,
      cost: null,
      observations: null,
      isDeleted: false
    })
    const habitationFieldsBlank = ref(
      JSON.parse(JSON.stringify(habitationFields))
    )

    const onSubmit = () => {
      createHabitation(habitationFields.value, data => {
        swal.fire({
          title: '¡Nueva habitación registrado!',
          text: 'La habitación se ha registrado correctamente',
          icon: 'success'
        })
        createHabitationReservation(
          {
            reservationHotelId: reservationHotelId.value,
            habitationId: data.habitationId,
            isDeleted: false
          },
          data => {}
        )
      })
      habitationFields.value = JSON.parse(JSON.stringify(habitationFieldsBlank))
      habitationFormRef.value.resetForm()
      isOpenDialog.value = false
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      habitationFields,
      habitationFormRef
    }
  }
}
</script>

<style></style>
