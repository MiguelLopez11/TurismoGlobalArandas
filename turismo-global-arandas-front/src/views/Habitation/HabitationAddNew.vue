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
          <Field name="email" v-slot="{ value, field, errorMessage }">
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
                v-model="habitationFields.minors"
                :validate-event="false"
                :model-value="value"
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
import HabitationServices from '@/Services/Habitation.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addHabitation')
    const swal = inject('$swal')
    const habitationFormRef = ref(null)
    const { createHabitation } = HabitationServices()
    const validationSchema = yup.object({
      invoice: yup.string().required('Este campo es requerido').label('Folio'),
      ages: yup.string().required('Este campo es requerido').label('Edades'),
      adults: yup.string().required('Este campo es requerido').label('Adultos'),
      minors: yup.string().required('Este campo es requerido').label('Menores'),
      purchaseDate: yup.date().required('Este campo es requerido').label('Fecha de compra'),
      cost: yup.number().required('Este campo es requerido').label('Costo')
    })
    const habitationFields = ref({
      habitationId: 0,
      invoice: null,
      advancePayment: null,
      ages: null,
      adults: null,
      minors: null,
      purchaseDate: null,
      cost: null,
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
        isOpenDialog.value = false
        habitationFields.value = JSON.parse(
          JSON.stringify(habitationFieldsBlank)
        )
        habitationFormRef.value.resetForm()
      })
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
