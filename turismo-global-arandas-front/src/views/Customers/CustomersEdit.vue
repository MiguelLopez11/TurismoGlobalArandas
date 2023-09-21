<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateCustomer">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-model="customer.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="lastname" :rules="validateLastname" as="text">
            <el-form-item :error="errors.lastname" required>
              <div>
                <label> Apellidos </label>
              </div>
              <el-input
                placeholder="Ingresa los apellidos del empleado"
                size="large"
                v-model="customer.lastname"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="phoneNumber" :rules="validatePhoneNumber" as="text">
            <el-form-item :error="errors.phoneNumber" required>
              <div>
                <label>Numero de telefono</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de telefono del empleado"
                size="large"
                v-model="customer.phoneNumber"
                type="number"
                min="10"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="nationality" :rules="validateNationality" as="text">
            <el-form-item :error="errors.nationality" required>
              <div>
                <label>Nacionalidad</label>
              </div>
              <el-input
                placeholder="Ingresa la nacionalidad del cliente"
                size="large"
                v-model="customer.nationality"
                type="number"
                min="10"
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
                $router.push('/Clientes')
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
import CustomerServices from '@/Services/Customers.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getCustomer, updateCustomer } = CustomerServices()
    const customer = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getCustomer(router.params.CustomerId, data => {
      customer.value = data
    })
    const onUpdateCustomer = () => {
      updateCustomer(customer.value, data => {
        swal
          .fire({
            title: 'Cliente modificado correctamente',
            text: 'El cliente se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Clientes')
            }
          })
      })
    }
    const validateName = () => {
      if (!customer.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateLastname = () => {
      if (!customer.value.lastname) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePhoneNumber = () => {
      if (!customer.value.phoneNumber) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateNationality = () => {
      if (!customer.value.nationality) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      customer,
      onUpdateCustomer,
      validateName,
      validateLastname,
      validatePhoneNumber,
      validateNationality
    }
  }
}
</script>

<style></style>
