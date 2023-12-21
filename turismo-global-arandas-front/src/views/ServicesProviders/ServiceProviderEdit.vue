<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateCommission">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="provider" :rules="validateProvider">
            <el-form-item :error="errors.provider">
              <v-select
                class="w-100"
                label="name"
                v-model="serviceProvider.providerId"
                :options="providers"
                :reduce="provider => provider.providerId"
                required
              >
                <template #selected-option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #option="{ name, lastname }">
                  <label>{{ name }} {{ lastname }}</label>
                </template>
                <template #header>
                  <span class="text-danger">*</span>
                  <label>Provedor</label>
                </template>
              </v-select>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="commissionAgency" :rules="validateCommissionAgency">
            <el-form-item :error="errors.commissionAgency">
              <div>
                <label> Comision agencia </label>
              </div>
              <el-input
                placeholder="Ingresa la comisión para la agencia"
                size="large"
                v-model="serviceProvider.commissionAgency"
                type="number"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="commissionClient" :rules="validateCommissionClient">
            <el-form-item :error="errors.commissionClient">
              <div>
                <label> Comisión al cliente </label>
              </div>
              <el-input
                placeholder="Ingresa la comision para el cliente"
                size="large"
                v-model="serviceProvider.commissionClient"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="commissionEmployee" :rules="validateCommissionEmployee">
            <el-form-item :error="errors.commissionEmployee">
              <div>
                <label>Comision al empleado</label>
              </div>
              <el-input
                placeholder="Ingresa la comision para el empleado"
                size="large"
                v-model="serviceProvider.commissionEmployee"
                type="number"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="4">
          <Field name="color" :rules="validateColor" required>
            <el-form-item :error="errors.color">
              <div>
                <label>Color representativo</label>
              </div>
              {{serviceProvider}}
              <div style="display: block !important">
                <el-color-picker v-model="serviceProvider.color" size="large" />
              </div>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <el-form-item>
            <div>
              <label>Descripción</label>
            </div>
            <el-input
              placeholder="Ingresa una descripcion"
              size="large"
              v-model="serviceProvider.description"
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
                $router.push('/ServiciosProveedores')
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
import ServicesProviderServices from '@/Services/ProviderServices.Services'
import ProviderServices from '@/Services/Provider.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getServiceProvider, updateServiceProvider } = ServicesProviderServices()
    const { getProviders } = ProviderServices()
    const providers = ref([])
    const serviceProvider = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getServiceProvider(router.params.ServiceId, data => {
      serviceProvider.value = data
    })
    getProviders(data => {
      providers.value = data
    })
    const onUpdateCommission = () => {
      updateServiceProvider(serviceProvider.value, data => {
        swal
          .fire({
            title: 'Servicio modificado correctamente',
            text: 'El servicio se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ServiciosProveedores')
            }
          })
      })
    }
    const validateCommissionAgency = () => {
      if (!serviceProvider.value.commissionAgency) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCommissionClient = () => {
      if (!serviceProvider.value.commissionClient) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCommissionEmployee = () => {
      if (!serviceProvider.value.commissionEmployee) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateColor = () => {
      if (!serviceProvider.value.color) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateProvider = () => {
      if (!serviceProvider.value.providerId) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      serviceProvider,
      providers,
      onUpdateCommission,
      validateCommissionAgency,
      validateCommissionClient,
      validateCommissionEmployee,
      validateColor,
      validateProvider
    }
  }
}
</script>

<style></style>
