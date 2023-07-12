<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo Usuario" width="80%" center>
    <Form
      ref="EmployeeForm"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="username" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre de usuario</label>
              </div>
              <el-input
                placeholder="Ingresa el nombre de usuario"
                size="large"
                v-bind="field"
                v-model="usersFields.username"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="password" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Contraseña</label>
              </div>
              <el-input
                placeholder="Ingresa la contraseña"
                size="large"
                v-bind="field"
                v-model="usersFields.password"
                :validate-event="false"
                :model-value="value"
                type="password"
                show-password
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="email" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Correo electronico </label>
              </div>
              <el-input
                placeholder="Ingresa un correo electronico"
                size="large"
                v-bind="field"
                v-model="usersFields.email"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="role">
            <div>
              <label>Role</label>
            </div>
            <el-form-item>
              <v-select
                v-model="usersFields.roleId"
                label="name"
                class="w-100"
                :options="roles"
                :reduce="role => role.id"
              >
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!usersFields.roleId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="employee">
            <div>
              <label>Empleados</label>
            </div>
            <el-form-item>
              <v-select
                v-model="usersFields.employeeId"
                label="name"
                class="w-100"
                :options="employees"
                :reduce="employee => employee.employeeId"
              >
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!usersFields.employeeId"
                    v-bind="attributes"
                    v-on="events"
                  />
                </template>
              </v-select>
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
import UserServices from '@/Services/User.Services'
import EmployeeServices from '@/Services/Employees.Services'
import RoleServices from '@/Services/Roles.Services'
import * as yup from 'yup'

export default {
  setup () {
    const isOpenDialog = inject('AddUser')
    const swal = inject('$swal')
    const { createUser } = UserServices()
    const { getEmployees } = EmployeeServices()
    const { getRoles } = RoleServices()
    const employees = ref({})
    const roles = ref({})
    getEmployees(data => {
      employees.value = data
    })
    getRoles(data => {
      roles.value = data
    })
    const validationSchema = yup.object({
      username: yup
        .string()
        .required('este campo es requerido')
        .label('Nombre de usuario'),
      email: yup
        .string()
        .required('este campo es requerido')
        .email('ingresa un correo electronico válido')
        .label('Correo electronico'),
      password: yup
        .string()
        .required('este campo es requerido')
        .min(8, 'debe contener minimo 8 caracteres')
        .matches(/[a-z]+/, 'minimo una letra minusculo')
        .matches(/[A-Z]+/, 'minimo una letra mayuscula')
        .matches(/[@$!%*#?&]+/, 'minimo un caracter especial')
        .matches(/\d+/, 'minimo un numero')
        .label('Contraseña')
    })
    const usersFields = ref({
      username: null,
      email: null,
      phoneNumber: null,
      password: null,
      roleId: null,
      employeeId: null
    })
    const usersFieldsBlank = ref(JSON.parse(JSON.stringify(usersFields)))

    const onSubmit = () => {
      isOpenDialog.value = false
      createUser(usersFields.value, data => {
        swal
          .fire({
            title: '¡Nuevo empleado registrado!',
            text: 'El empleado se ha registrado correctamente',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              usersFields.value = JSON.parse(JSON.stringify(usersFieldsBlank))
            }
          })
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      employees,
      validationSchema,
      usersFields,
      roles
    }
  }
}
</script>

<style></style>
