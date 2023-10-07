<template>
  <el-card>
    <Form
      ref="reservationHotelGroupFormRef"
      v-slot="{ errors }"
      as="el-form"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="groupName" rules="">
            <el-form-item :error="errors.groupName">
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-model="reservationHotelGroup.groupName"
              />
            </el-form-item>
          </Field>
        </el-col>
      </el-row>
    </Form>
  </el-card>
</template>

<script>
import ReservationHotelGroupServices from '@/Services/ReservationHotelGroup.Services'
import * as yup from 'yup'
import { ref, computed } from 'vue'
import { useStore } from 'vuex'
export default {
  props: {
    reservationHotelId: {
      type: Number,
      required: true,
      default: null
    }
  },
  setup (props) {
    const {
      getReservationHotelGroupByreservationHotel,
      createReservationHotelGroup
    } = ReservationHotelGroupServices()
    const reservationHotelGroup = ref([])
    const store = useStore()
    const reservationHotelId = computed(
      () => store.getters.getReservationHotelId
    )
    const validationSchema = yup.object({
      groupName: yup
        .string()
        .required('Este campo es requerido')
        .label('Nombre del grupo')
    })

    const reservationHotelGroupfields = ref({
      reservationHotelGroupId: 0,
      groupName: null,
      confirmationKey: null,
      dateArrival: null,
      coordinator: null,
      phoneNumber: null,
      reservationHotelId: reservationHotelId.value,
      isDeleted: false
    })
    if (!props.reservationHotelId) {
      createReservationHotelGroup(reservationHotelGroupfields.value, data => {
        getReservationHotelGroupByreservationHotel(
          data.reservationHotelId,
          data => {
            reservationHotelGroup.value = data
            console.log(data)
          }
        )
      })
    } else {
      getReservationHotelGroupByreservationHotel(
        props.reservationHotelId,
        data => {
          reservationHotelGroup.value = data
        }
      )
    }
    const validateGroupName = () => {
      if (!reservationHotelGroup.value.groupName) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      reservationHotelGroup,
      validationSchema,
      validateGroupName
    }
  }
}
</script>

<style></style>
