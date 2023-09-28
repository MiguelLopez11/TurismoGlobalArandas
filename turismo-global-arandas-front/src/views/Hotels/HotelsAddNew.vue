<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Hotel" width="80%" center>
    <Form
      ref="hotelFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="destination">
            <el-form-item>
              <v-select
                v-model="hotelfields.destinationId"
                label="name"
                class="w-100"
                :options="destinations"
                :reduce="item => item.destinationId"
              >
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!hotelfields.destinationId"
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
          <Field name="name" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del destino"
                size="large"
                v-bind="field"
                v-model="hotelfields.name"
                :validate-event="false"
                :model-value="value"
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
              v-model="hotelfields.description"
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
import { ref, inject, watch } from 'vue'
import { Field, Form } from 'vee-validate'
import hotelsServices from '@/Services/Hotels.Services'
import DestinationServices from '@/Services/Destinations.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const hotelFormRef = ref(null)
    const isOpenDialog = inject('addHotel')
    const swal = inject('$swal')
    const { getDestinations } = DestinationServices()
    const { createHotel } = hotelsServices()
    const destinations = ref([])
    watch(isOpenDialog, newValue => {
      if (newValue) {
        getDestinations(data => {
          destinations.value = data
        })
      }
    })

    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const hotelfields = ref({
      hotelId: 0,
      name: null,
      destinationId: null,
      description: null,
      isDeleted: false
    })
    const hotelfieldsBlank = ref(JSON.parse(JSON.stringify(hotelfields.value)))

    const onSubmit = () => {
      createHotel(hotelfields.value, data => {
        swal.fire({
          title: '¡Nuevo hotel registrado!',
          text: 'El hotel se ha registrado correctamente',
          icon: 'success'
        })
        hotelfields.value = JSON.parse(JSON.stringify(hotelfieldsBlank.value))
        isOpenDialog.value = false
        hotelFormRef.value.resetForm()
      })
    }

    return {
      destinations,
      isOpenDialog,
      onSubmit,
      validationSchema,
      hotelfields,
      hotelFormRef
    }
  }
}
</script>

<style></style>
