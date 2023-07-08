<template>
  <el-card>
    <Form
      ref="EmployeeForm"
      @submit="onUpdateUser"
      v-slot="{ errors }"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="username" :rules="validateUsername">
            <el-form-item :error="errors.username" required>
              <div>
                <label> Nombre de usuario</label>
              </div>
              <el-input
                placeholder="Ingresa el nombre de usuario"
                size="large"
                v-model="user.userName"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="email" :rules="validateEmail">
            <el-form-item :error="errors.email" required>
              <div>
                <label> Correo electronico </label>
              </div>
              <el-input
                placeholder="Ingresa un correo electronico"
                size="large"
                v-model="user.email"
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
                v-model="user.roleId"
                label="name"
                class="w-100"
                :options="roles"
                :reduce="role => role.id"
              >
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!user.roleId"
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
                v-model="user.employeeId"
                label="name"
                class="w-100"
                :options="employees"
                :reduce="employee => employee.employeeId"
              >
                <template #search="{ attributes, events }">
                  <input
                    class="vs__search"
                    :required="!user.employeeId"
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
  </el-card>
</template>

<script>
import UserServices from '@/Services/User.Services'
import EmployeeServices from '@/Services/Employees.Services'
import RoleServices from '@/Services/Roles.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'
export default {
  setup () {
    const { getUser, updateUser } = UserServices()
    const { getEmployees } = EmployeeServices()
    const { getRoles } = RoleServices()
    const user = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    const employees = ref([])
    const roles = ref([])

    getEmployees(data => {
      employees.value = data
    })
    getRoles(data => {
      roles.value = data
    })

    getUser(router.params.UserId, data => {
      user.value = data
    })

    const validateUsername = () => {
      if (!user.value.userName) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateEmail = () => {
      if (!user.value.email) {
        return 'Este campo es requerido'
      }
      return true
    }
    const onUpdateUser = () => {
      updateUser(user.value, data => {
        swal
          .fire({
            title: 'Usuario modificado correctamente',
            text: 'El usuario se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Usuarios')
            }
          })
      })
    }
    return {
      user,
      roles,
      employees,
      validateUsername,
      validateEmail,
      onUpdateUser
    }
  }
}
</script>

<style></style>
