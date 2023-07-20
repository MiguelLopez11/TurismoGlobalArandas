<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateProvider">
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
                v-model="provider.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="email" :rules="validateEmail" as="text">
            <el-form-item :error="errors.email" required>
              <div>
                <label> Correo electronico </label>
              </div>
              <el-input
                placeholder="Ingresa los apellidos del empleado"
                size="large"
                v-model="provider.email"
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
                v-model="provider.phoneNumber"
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
                $router.push('/Proveedores')
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
import ProviderServices from '@/Services/Provider.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getProvider, updateProvider } = ProviderServices()
    const provider = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getProvider(router.params.ProviderId, data => {
      provider.value = data
    })
    const onUpdateProvider = () => {
      updateProvider(provider.value, data => {
        swal
          .fire({
            title: 'Proveedor modificado correctamente',
            text: 'El proveedor se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Proveedores')
            }
          })
      })
    }
    const validateName = () => {
      if (!provider.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateEmail = () => {
      if (!provider.value.email) {
        return 'Este campo es requerido'
      }
      if (
        !/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/.test(
          provider.value.email
        )
      ) {
        return 'Ingrese un correo electronico válido'
      }
      return true
    }
    const validatePhoneNumber = () => {
      if (!provider.value.phoneNumber) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      provider,
      onUpdateProvider,
      validateName,
      validateEmail,
      validatePhoneNumber
    }
  }
}
</script>

<style></style>
