<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Registrar nueva tarifa grupal"
    width="80%"
    top
  >
    <el-row :gutter="35">
      <el-col :span="8">
        <el-form-item>
          <div>
            <label> Nombre acompañante </label>
          </div>
          <el-input
            placeholder="Ingresa el nombre del acompañante"
            size="large"
            v-model="groupRateFields.namesCompanions"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label> Adultos </label>
          </div>
          <el-input
            placeholder="Ingresa el numero de adultos"
            size="large"
            v-model="groupRateFields.adults"
            type="number"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label> Juniors </label>
          </div>
          <el-input
            placeholder="Ingresa la cantidad de juniors"
            size="large"
            v-model="groupRateFields.juniors"
            type="number"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label> Menores con cargo </label>
          </div>
          <el-input
            placeholder="Ingresa el numero de menores con cargo"
            size="large"
            v-model="groupRateFields.minorsCharge"
            type="number"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Menores sin cargo</label>
          </div>
          <el-input
            placeholder="Ingresa las edades de los menores"
            size="large"
            v-model="groupRateFields.minorsWithoutCharge"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Edades de los menores</label>
          </div>
          <el-input
            placeholder="Ingresa las edades de los menores que viajarán"
            size="large"
            v-model="groupRateFields.agesMinors"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <div class="mb-2">
          <span class="text-danger">*</span>
          <span>Fecha de salida</span>
        </div>
        <el-form-item>
          <el-date-picker
            v-model="reservationHotelGroup.dateStart"
            class="w-100"
            type="date"
            placeholder="Selecciona la fecha limite del pago"
            size="large"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <div class="mb-2">
          <span class="text-danger">*</span>
          <span>Fecha de llegada</span>
        </div>
        <el-form-item>
          <el-date-picker
            v-model="reservationHotelGroup.dateEnd"
            class="w-100"
            type="date"
            placeholder="Selecciona la fecha limite del pago"
            size="large"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>TARIFA X PAX BASE DBL </label>
          </div>
          <el-input
            placeholder="Ingresa el costo publico del cliente"
            size="large"
            v-model="reservationHotelGroup.rangePublicClient"
            type="number"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Tarifa por junior </label>
          </div>
          <el-input
            placeholder="Ingresa la tarifa por junior"
            size="large"
            v-model="reservationHotelGroup.rangeJunior"
            type="number"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Tarifa por menor </label>
          </div>
          <el-input
            placeholder="Ingresa la tarifa por menor"
            size="large"
            v-model="reservationHotelGroup.rangeMinor"
            type="number"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Numero de noches </label>
          </div>
          <el-input
            placeholder="Ingresa la cantidad de noches"
            size="large"
            v-model="reservationHotelGroup.nightsNumber"
            type="number"
            disabled
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Tarifa por noche </label>
          </div>
          <el-input
            placeholder="Ingresa la tarifa por noche"
            size="large"
            v-model="groupRateFields.rangeNight"
            type="number"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Tarifa total por habitación </label>
          </div>
          <el-input
            placeholder="Ingresa la tarifa total noche"
            size="large"
            v-model="groupRateFields.rangeTotal"
            type="number"
          />
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item>
          <div>
            <label>Observaciones</label>
          </div>
          <el-input
            placeholder="Ingresa las observaciones de la habitación"
            size="large"
            v-model="groupRateFields.observations"
            type="textarea"
            :autosize="{ minRows: 4, maxRows: 8 }"
          />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider />
    <el-row :gutter="25" justify="end">
      <el-col :span="3">
        <el-button color="#7367F0" class="w-100" size="large" @click="onSubmit"
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
  </el-dialog>
</template>

<script>
import { ref, inject, watch } from 'vue'
import GroupRateServices from '@/Services/GroupRate.Services'
import ReservationHotelGroupServices from '@/Services/ReservationHotelGroup.Services'
import { useStore } from 'vuex'

export default {
  setup () {
    const isOpenDialog = inject('addGroupRate')
    const swal = inject('$swal')
    const store = useStore()
    const reservationHotelGroup = ref([])
    const groupRateFormRef = ref(null)
    const { createGroupRate } = GroupRateServices()
    const { getReservationHotelGroup } = ReservationHotelGroupServices()
    const reservationHotelGroupId = ref(
      parseInt(store.getters.getReservationHotelGroupId)
    )
    const groupRateFields = ref({
      groupRateId: 0,
      namesCompanions: null,
      adults: null,
      juniors: null,
      minorsCharge: null,
      minorsWithoutCharge: null,
      agesMinors: null,
      rangeNight: null,
      rangeTotal: null,
      observations: null,
      reservationHotelGroupId: reservationHotelGroupId.value,
      isDeleted: false
    })
    getReservationHotelGroup(reservationHotelGroupId.value, data => {
      reservationHotelGroup.value = data
    })
    const groupRateFieldsBlank = ref(
      JSON.parse(JSON.stringify(groupRateFields))
    )

    const onSubmit = () => {
      createGroupRate(groupRateFields.value, data => {
        swal.fire({
          title: '¡Nueva tarifa registrada!',
          text: 'La tarifa de habitacion grupal se ha registrado correctamente',
          icon: 'success'
        })
      })
      groupRateFields.value = JSON.parse(JSON.stringify(groupRateFieldsBlank))
      isOpenDialog.value = false
    }
    watch(groupRateFields.value, NewValue => {
      if (
        NewValue.adults !== null &&
        NewValue.juniors !== null &&
        NewValue.minorsCharge !== null &&
        NewValue.minorsWithoutCharge !== null
      ) {
        const rateNight =
          (reservationHotelGroup.value.rangePublicClient !== null
            ? parseFloat(reservationHotelGroup.value.rangePublicClient) *
              parseFloat(NewValue.adults)
            : 0) +
          (reservationHotelGroup.value.rangeJunior !== null
            ? parseFloat(reservationHotelGroup.value.rangeJunior) *
              parseFloat(NewValue.juniors)
            : 0) +
          (reservationHotelGroup.value.rangeMinor !== null
            ? parseFloat(reservationHotelGroup.value.rangeMinor) *
              parseFloat(NewValue.minorsCharge)
            : 0)
        const rateTotal = rateNight * reservationHotelGroup.value.nightsNumber
        groupRateFields.value.rangeNight = rateNight
        groupRateFields.value.rangeTotal = rateTotal
      }
    })
    return {
      isOpenDialog,
      onSubmit,
      reservationHotelGroup,
      groupRateFields,
      groupRateFormRef
    }
  }
}
</script>

<style></style>
