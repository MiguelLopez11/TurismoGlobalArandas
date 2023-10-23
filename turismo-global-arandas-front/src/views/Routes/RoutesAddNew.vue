<template>
  <el-dialog v-model="isOpenDialog" title="Nueva ruta" width="80%" center>
    <Form
      ref="routeFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="routeName" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre de la ruta </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del destino"
                size="large"
                v-bind="field"
                v-model="routeFields.routeName"
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
              v-model="routeFields.description"
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
import RouteServices from '@/Services/Routes.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addPaymentMethod')
    const swal = inject('$swal')
    const routeFormRef = ref(null)
    const { createRoute } = RouteServices()
    const validationSchema = yup.object({
      routeName: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const routeFields = ref({
      routeId: 0,
      routeName: null,
      description: null,
      isDeleted: false
    })
    const routeFieldsBlank = ref(
      JSON.parse(JSON.stringify(routeFields))
    )

    const onSubmit = () => {
      createRoute(routeFields.value, data => {
        swal.fire({
          title: '¡Nuevo método de pago registrado!',
          text: 'El método de pago se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        routeFields.value = JSON.parse(
          JSON.stringify(routeFieldsBlank)
        )
        routeFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      routeFields,
      routeFormRef
    }
  }
}
</script>

<style></style>
