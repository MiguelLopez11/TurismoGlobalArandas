<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Role" width="40%" center>
    <Form
      ref="roleFormRef"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35" justify="center">
        <el-col :span="15">
          <Field name="name" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Role </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del role"
                size="large"
                v-bind="field"
                v-model="roleFields.Name"
                :validate-event="false"
                :model-value="value"
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
import RoleServices from '@/Services/Roles.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenDialog = inject('addRole')
    const swal = inject('$swal')
    const roleFormRef = ref(null)
    const { createRole } = RoleServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre')
    })
    const roleFields = ref({
      Name: null
    })
    const roleFieldsBlank = ref(
      JSON.parse(JSON.stringify(roleFields))
    )

    const onSubmit = () => {
      createRole(roleFields.value.Name, data => {
        swal.fire({
          title: '¡Nuevo role registrado!',
          text: 'El role se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        roleFields.value = JSON.parse(
          JSON.stringify(roleFieldsBlank)
        )
        roleFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      roleFields,
      roleFormRef
    }
  }
}
</script>

<style></style>
