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
                v-model="commission.providerId"
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
                v-model="commission.commissionAgency"
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
                v-model="commission.commissionClient"
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
                v-model="commission.commissionEmployee"
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
              <div style="display: block !important">
                <el-color-picker v-model="commission.color" size="large" />
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
              v-model="commission.description"
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
                $router.push('/comisiones')
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
import CommissionServices from '@/Services/Commissions.Services'
import ProviderServices from '@/Services/Provider.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getCommission, updateCommission } = CommissionServices()
    const { getProviders } = ProviderServices()
    const providers = ref([])
    const commission = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getCommission(router.params.CommissionId, data => {
      commission.value = data
    })
    getProviders(data => {
      providers.value = data
    })
    const onUpdateCommission = () => {
      updateCommission(commission.value, data => {
        swal
          .fire({
            title: 'Comisión modificado correctamente',
            text: 'La comision se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/comisiones')
            }
          })
      })
    }
    const validateCommissionAgency = () => {
      if (!commission.value.commissionAgency) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCommissionClient = () => {
      if (!commission.value.commissionClient) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCommissionEmployee = () => {
      if (!commission.value.commissionEmployee) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateColor = () => {
      if (!commission.value.color) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateProvider = () => {
      if (!commission.value.providerId) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      commission,
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
