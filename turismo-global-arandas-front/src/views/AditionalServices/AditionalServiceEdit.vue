<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateAditionalService">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre del servicio adicional </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del servicio"
                size="large"
                v-model="aditionalService.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="cost" :rules="validateCost" as="text">
            <el-form-item :error="errors.cost" required>
              <div>
                <label> Costo </label>
              </div>
              <el-input
                placeholder="Ingresa el costo del servicio"
                size="large"
                v-model="aditionalService.cost"
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
                placeholder="Ingresa el numero de telefono del empleado"
                size="large"
                v-model="aditionalService.description"
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
                $router.push('/ServiciosAdicionales')
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
import AditionalServiceServices from '@/Services/AditionalServices.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getAditionalService, updateAditionalService } = AditionalServiceServices()
    const aditionalService = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getAditionalService(router.params.AditionalServiceId, data => {
      aditionalService.value = data
    })
    const onUpdateAditionalService = () => {
      updateAditionalService(aditionalService.value, data => {
        swal
          .fire({
            title: 'Servicio adicional modificado correctamente',
            text: 'El servicio adicional se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ServiciosAdicionales')
            }
          })
      })
    }
    const validateName = () => {
      if (!aditionalService.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCost = () => {
      if (!aditionalService.value.cost) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      aditionalService,
      onUpdateAditionalService,
      validateName,
      validateCost
    }
  }
}
</script>

<style></style>
