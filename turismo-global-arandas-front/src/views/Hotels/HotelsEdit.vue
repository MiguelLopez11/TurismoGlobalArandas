<template>
  <el-card>
    <Form as="el-form" v-slot="{ errors }" @submit="onUpdateHotel">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="destination">
            <el-form-item>
              <v-select
                v-model="hotel.destinationId"
                label="name"
                class="w-100"
                :options="destinations"
                :reduce="item => item.destinationId"
              >
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!hotel.destinationId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
                <template #header>
                  <label> Destino </label>
                </template>
              </v-select>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-model="hotel.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <div>
              <label> Descripción </label>
            </div>
            <el-input
              placeholder="Ingresa una descripción del destino"
              size="large"
              v-model="hotel.description"
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
                $router.push('/Hoteles')
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
import DestinationServices from '@/Services/Destinations.Services'
import HotelsServices from '@/Services/Hotels.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getDestinations } = DestinationServices()
    const { getHotel, updateHotel } = HotelsServices()
    const destinations = ref([])
    const hotel = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getHotel(router.params.HotelId, data => {
      hotel.value = data
    })
    getDestinations(data => {
      destinations.value = data
    })
    const onUpdateHotel = () => {
      updateHotel(hotel.value, data => {
        swal
          .fire({
            title: 'Hotel modificado correctamente',
            text: 'El hotel se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Hoteles')
            }
          })
      })
    }
    const validateName = () => {
      if (!hotel.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      hotel,
      destinations,
      onUpdateHotel,
      validateName
    }
  }
}
</script>

<style></style>
