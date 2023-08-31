<template>
    <Form
      ref="individualRateRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="publicRate" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
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
                isOpenedDialog = !isOpenedDialog
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
</template>

<script>
import { ref, inject, computed } from 'vue'
import { Field, Form } from 'vee-validate'
import EmployeeServices from '@/Services/Employees.Services'
import * as yup from 'yup'
import { useStore } from 'vuex'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenedDialog = inject('AddEmployee')
    const swal = inject('$swal')
    const store = useStore()
    const reservationHotelId = computed(
      () => store.getters.getReservationHotelId
    )
    const individualRateRef = ref(null)
    const { createEmployee } = EmployeeServices()
    const validationSchema = yup.object({
      publicRate: yup.string().required().label('Tarifa publica'),
      clientRate: yup.string().required().label('Tarifa cliente'),
      extraDiscount: yup.string().required().label('Descuento extra')
    })
    const individualRateFields = ref({
      individualRateId: 0,
      reservationHotelId: reservationHotelId.value,
      publicRate: null,
      clientRate: null,
      extraDiscount: null,
      isDeleted: false
    })
    const individualRateFieldsBlank = ref(
      JSON.parse(JSON.stringify(individualRateFields))
    )

    const onSubmit = () => {
      createEmployee(individualRateFields.value, data => {
        swal.fire({
          title: '¡Nueva Tarifa registrado!',
          text: 'La tarifa se ha registrado correctamente',
          icon: 'success'
        })
        isOpenedDialog.value = false
        individualRateFields.value = JSON.parse(
          JSON.stringify(individualRateFieldsBlank)
        )
        individualRateRef.value.resetForm()
      })
    }

    return {
      isOpenedDialog,
      onSubmit,
      reservationHotelId,
      validationSchema,
      individualRateFields,
      individualRateRef
    }
  }
}
</script>

<style></style>
