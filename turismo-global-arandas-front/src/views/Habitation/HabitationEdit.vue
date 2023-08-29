<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Registrar habitación"
    width="80%"
    center
  >
    <Form v-slot="{ errors }" @submit="onUpdateHabitation">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="invoice" :rules="validateInvoice" as="text">
            <el-form-item :error="errors.invoice" required>
              <div>
                <label> Folio </label>
              </div>
              <el-input
                placeholder="Ingresa el Folio de la habitación"
                size="large"
                v-model="habitation.invoice"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="typehabitation"
            :rules="validateTypeHabitation"
            as="text"
          >
            <el-form-item :error="errors.typehabitation" required>
              <div>
                <label> Tipo de habitación </label>
              </div>
              <el-input
                placeholder="Ingresa el tipo de habitación"
                size="large"
                v-model="habitation.typeHabitation"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="advancePayment"
            :rules="validateAdvancePayment"
            as="text"
          >
            <el-form-item :error="errors.advancePayment" required>
              <div>
                <label> Anticipo </label>
              </div>
              <el-input
                placeholder="Ingresa el monto del anticipo"
                size="large"
                v-model="habitation.advancePayment"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="ages" :rules="validateAges" as="text">
            <el-form-item :error="errors.ages" required>
              <div>
                <label>Edades</label>
              </div>
              <el-input
                placeholder="Ingresa las edades de todos"
                size="large"
                v-model="habitation.ages"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="adults" :rules="validateAdults" as="text">
            <el-form-item :error="errors.adults" required>
              <div>
                <label>Adultos</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de adultos que viajarán"
                size="large"
                v-model="habitation.adults"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="minors" :rules="validateMinors" as="text">
            <el-form-item :error="errors.minors" required>
              <div>
                <label>Menores</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de menores que viajarán"
                size="large"
                v-model="habitation.minors"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="purchaseDate" :rules="validatePurchaseDate" as="text">
            <el-form-item :error="errors.purchaseDate" required>
              <div>
                <label>Fecha de compra</label>
              </div>
              <el-date-picker
                v-model="habitation.purchaseDate"
                class="w-100"
                type="date"
                placeholder="Selecciona una fecha"
                size="large"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="cost" :rules="validateCost" as="text">
            <el-form-item :error="errors.cost" required>
              <div>
                <label>Costo</label>
              </div>
              <el-input
                placeholder="Ingresa el costo de la habitación"
                size="large"
                v-model="habitation.cost"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="observations" :rules="validateObservations" as="text">
            <el-form-item :error="errors.observations" required>
              <div>
                <label>Observaciones</label>
              </div>
              <el-input
                placeholder="Ingresa las observaciones de la habitación"
                size="large"
                v-model="habitation.observations"
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
import { ref, inject, computed, watch } from 'vue'
import { Field, Form } from 'vee-validate'
import HabitationServices from '@/Services/Habitation.Services'
import { useStore } from 'vuex'

export default {
  components: {
    Form,
    Field
  },
  setup (props) {
    const isOpenDialog = inject('editHabitation')
    // const swal = inject('$swal')
    const habitationFormRef = ref(null)
    const habitation = ref([])
    const store = useStore()
    const { getHabitation, updateHabitation } = HabitationServices()
    watch(isOpenDialog, newValue => {
      if (newValue) {
        const hotelId = computed(() => store.getters.getHotelId)
        getHabitation(hotelId.value, data => {
          habitation.value = data
        })
      }
    })

    const onUpdateHabitation = () => {
      updateHabitation(habitation.value, data => {
        isOpenDialog.value = false
        // swal
        //   .fire({
        //     title: 'Habitación modificada correctamente',
        //     text: 'La habitación se ha modificado satisfactoriamente.',
        //     icon: 'success'
        //   })
        //   .then(result => {
        //     if (result.isConfirmed) {
        //       //
        //     }
        //   })
      })
    }
    const validateInvoice = () => {
      if (!habitation.value.invoice) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateTypeHabitation = () => {
      if (!habitation.value.typeHabitation) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAdvancePayment = () => {
      if (!habitation.value.advancePayment) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAges = () => {
      if (!habitation.value.ages) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAdults = () => {
      if (!habitation.value.adults) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateMinors = () => {
      if (!habitation.value.minors) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePurchaseDate = () => {
      if (!habitation.value.purchaseDate) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCost = () => {
      if (!habitation.value.cost) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateObservations = () => {
      if (!habitation.value.observations) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      isOpenDialog,
      habitation,
      habitationFormRef,
      onUpdateHabitation,
      validateInvoice,
      validateTypeHabitation,
      validateAdvancePayment,
      validateAges,
      validateAdults,
      validateMinors,
      validatePurchaseDate,
      validateCost,
      validateObservations
    }
  }
}
</script>

<style></style>
