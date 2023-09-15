<template>
  <el-dialog v-model="isOpenDialog" title="Nueva comisión" width="80%" center>
    <Form
      ref="commisionFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <el-form-item>
            <v-select
              class="w-100"
              label="name"
              v-model="commisionFields.providerId"
              :options="providers"
              :reduce="provider => provider.providerId"
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
              <template #search="{ attributes, events }">
                <input
                  class="vs__search"
                  :required="!commisionFields.providerId"
                  v-bind="attributes"
                  v-on="events"
                />
              </template>
            </v-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <Field
            name="commissionAgency"
            v-slot="{ value, field, errorMessage }"
          >
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Comision agencia </label>
              </div>
              <el-input
                placeholder="Ingresa la comisión para la agencia"
                size="large"
                v-bind="field"
                v-model="commisionFields.commissionAgency"
                :validate-event="false"
                :model-value="value"
                type="number"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="commissionClient"
            v-slot="{ value, field, errorMessage }"
          >
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Comisión al cliente </label>
              </div>
              <el-input
                placeholder="Ingresa la comision para el cliente"
                size="large"
                v-bind="field"
                v-model="commisionFields.commissionClient"
                :validate-event="false"
                :model-value="value"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field
            name="commissionEmployee"
            v-slot="{ value, field, errorMessage }"
          >
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Comision al empleado</label>
              </div>
              <el-input
                placeholder="Ingresa la comision para el empleado"
                size="large"
                v-bind="field"
                v-model="commisionFields.commissionEmployee"
                :validate-event="false"
                :model-value="value"
                type="number"
              >
                <template #append>%</template>
              </el-input>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="4">
          <Field name="color" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Color representativo</label>
              </div>
              <div style="display: block !important">
                <el-color-picker
                  v-model="commisionFields.color"
                  :validate-event="false"
                  v-bind="field"
                  :model-value="value"
                  size="large"
                />
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
              v-model="commisionFields.description"
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
import { ref, inject } from 'vue'
import { Field, Form } from 'vee-validate'
import CommissionServices from '@/Services/Commissions.Services'
import ProviderServices from '@/Services/Provider.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addCommission')
    const swal = inject('$swal')
    const commisionFormRef = ref(null)
    const providers = ref([])
    const { createCommission } = CommissionServices()
    const { getProviders } = ProviderServices()
    const validationSchema = yup.object({
      commissionAgency: yup
        .number()
        .required('Este campo es requerido')
        .label('Comision agencia'),
      commissionClient: yup
        .number()
        .required('Este campo es requerido')
        .label('Comision cliente'),
      commissionEmployee: yup
        .number()
        .required('Este campo es requerido')
        .label('Comision empleado'),
      color: yup
        .string()
        .required('Este campo es requerido')
        .label('Comision empleado')
    })
    const commisionFields = ref({
      commissionId: 0,
      providerId: null,
      color: null,
      commissionAgency: null,
      commissionClient: null,
      commissionEmployee: null,
      description: null,
      isDeleted: false
    })
    const commisionFieldsBlank = ref(
      JSON.parse(JSON.stringify(commisionFields))
    )
    getProviders(data => {
      providers.value = data
    })
    const onSubmit = () => {
      commisionFields.value.commissionAgency =
        commisionFields.value.commissionAgency / 100
      commisionFields.value.commissionClient =
        commisionFields.value.commissionClient / 100
      commisionFields.value.commissionEmployee =
        commisionFields.value.commissionEmployee / 100
      createCommission(commisionFields.value, data => {
        swal.fire({
          title: '¡Nueva comisión registrada!',
          text: 'La nueva comisión se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        commisionFields.value = JSON.parse(JSON.stringify(commisionFieldsBlank))
        commisionFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      commisionFormRef,
      providers,
      onSubmit,
      commisionFields,
      validationSchema
    }
  }
}
</script>

<style>
.el-form-item__content {
  display: block;
}
</style>
