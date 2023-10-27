<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Servicio adicional" width="80%" center>
    <Form
      ref="AditionalServiceFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre del servicio adicional </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del servicio"
                size="large"
                v-bind="field"
                v-model="AditionalServiceFields.name"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="cost" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Costo </label>
              </div>
              <el-input
                placeholder="Ingresa el costo del servicio"
                size="large"
                v-bind="field"
                v-model="AditionalServiceFields.cost"
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
              v-model="AditionalServiceFields.description"
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
import AditionalServiceServices from '@/Services/AditionalServices.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addAditionalService')
    const swal = inject('$swal')
    const AditionalServiceFormRef = ref(null)
    const { createAditionalService } = AditionalServiceServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre'),
      cost: yup.number()
        .test('is-decimal', 'Ingresa un valor válido', value =>
          (value + '').match(/^\d*\.?\d*$/)
        )
        .required('Este campo es requerido')
    })
    const AditionalServiceFields = ref({
      destinationId: 0,
      name: null,
      description: null,
      isDeleted: false
    })
    const AditionalServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(AditionalServiceFields))
    )

    const onSubmit = () => {
      createAditionalService(AditionalServiceFields.value, data => {
        swal.fire({
          title: '¡Nuevo servicio adicional registrado!',
          text: 'El servicio adicional se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        AditionalServiceFields.value = JSON.parse(
          JSON.stringify(AditionalServiceFieldsBlank)
        )
        AditionalServiceFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      AditionalServiceFields,
      AditionalServiceFormRef
    }
  }
}
</script>

<style></style>
