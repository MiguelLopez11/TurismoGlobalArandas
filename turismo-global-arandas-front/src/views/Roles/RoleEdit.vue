<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateRole">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del role"
                size="large"
                v-model="role.name"
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
                $router.push('/Roles')
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
import RoleServices from '@/Services/Roles.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getRole, updateRole } = RoleServices()
    const role = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getRole(router.params.roleName, data => {
      role.value = data
    })
    const onUpdateRole = () => {
      updateRole(router.params.roleName, role.value.name, data => {
        swal
          .fire({
            title: 'Role modificado correctamente',
            text: 'El role se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Roles')
            }
          })
      })
    }
    const validateName = () => {
      if (!role.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      role,
      onUpdateRole,
      validateName
    }
  }
}
</script>

<style></style>
