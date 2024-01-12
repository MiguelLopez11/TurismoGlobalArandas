<template>
  <el-dialog
    v-model="isOpenDialog"
    title="Registrar habitación"
    width="80%"
    center
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
            v-model="groupRate.namesCompanions"
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
            v-model="groupRate.adults"
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
            v-model="groupRate.juniors"
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
            v-model="groupRate.minorsCharge"
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
            v-model="groupRate.minorsWithoutCharge"
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
            v-model="groupRate.agesMinors"
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
            v-model="groupRate.rangeNight"
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
            v-model="groupRate.rangeTotal"
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
            v-model="groupRate.observations"
            type="textarea"
            :autosize="{ minRows: 4, maxRows: 8 }"
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
          size="large"
          @click="onUpdateGroupRate"
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
// import { useStore } from 'vuex'

import GroupRateServices from '@/Services/GroupRate.Services'
// import ReservationHotelGroupServices from '@/Services/ReservationHotelGroup.Services'

export default {
  props: {
    groupRateId: {
      type: Number,
      required: false
    }
  },
  setup (props) {
    const isOpenDialog = inject('editHabitation')
    // const store = useStore()
    const { getGroupRate, updateGroupRate } = GroupRateServices()
    // const { getReservationHotelGroup } = ReservationHotelGroupServices()
    // const reservationHotelGroupId = ref(
    //   parseInt(store.getters.getReservationHotelGroupId)
    // )
    const habitationFormRef = ref(null)
    const groupRate = ref([])
    const reservationHotelGroup = ref([])

    // getReservationHotelGroup(reservationHotelGroupId.value, data => {
    //   reservationHotelGroup.value = data
    // })
    const onUpdateGroupRate = () => {
      updateGroupRate(groupRate.value, data => {
        isOpenDialog.value = false
      })
    }
    watch(isOpenDialog, NewValue => {
      if (NewValue) {
        getGroupRate(props.groupRateId, data => {
          groupRate.value = data
        })
      }
    })
    return {
      isOpenDialog,
      groupRate,
      reservationHotelGroup,
      habitationFormRef,
      onUpdateGroupRate
    }
  }
}
</script>

<style></style>
