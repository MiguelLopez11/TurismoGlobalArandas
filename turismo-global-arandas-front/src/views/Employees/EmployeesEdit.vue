<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateEmployee">
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
                v-model="employee.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="lastname" :rules="validateLastname" as="text">
            <el-form-item :error="errors.lastname" required>
              <div>
                <label> Apellidos </label>
              </div>
              <el-input
                placeholder="Ingresa los apellidos del empleado"
                size="large"
                v-model="employee.lastname"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="workStation" :rules="validateWorkStation" as="text">
            <el-form-item :error="errors.workStation" required>
              <div>
                <label>Puesto de trabajo</label>
              </div>
              <el-input
                placeholder="Ingresa el puesto de trabajo del empleado"
                size="large"
                v-model="employee.workStation"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="address" :rules="validateAddress" as="text">
            <el-form-item :error="errors.address" required>
              <div>
                <label>Dirección</label>
              </div>
              <el-input
                placeholder="Ingresa la dirección del empleado"
                size="large"
                v-model="employee.address"
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
                v-model="employee.phoneNumber"
                type="number"
                min="10"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="salary" :rules="validateSalary" as="text">
            <el-form-item :error="errors.salary" required>
              <div>
                <label>Salario</label>
              </div>
              <el-input
                placeholder="Ingresa el salario del empleado"
                size="large"
                v-model="employee.salary"
                type="number"
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
                $router.push('/Empleados')
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
import EmployeeServices from '@/Services/Employees.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getEmployee, updateEmployee } = EmployeeServices()
    const employee = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getEmployee(router.params.EmployeeId, data => {
      employee.value = data
    })
    const onUpdateEmployee = () => {
      updateEmployee(employee.value, data => {
        swal
          .fire({
            title: 'Empleado modificado correctamente',
            text: 'El empleado se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Empleados')
            }
          })
      })
    }
    const validateName = () => {
      if (!employee.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateLastname = () => {
      if (!employee.value.lastname) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateWorkStation = () => {
      if (!employee.value.workStation) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateAddress = () => {
      if (!employee.value.address) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validatePhoneNumber = () => {
      if (!employee.value.phoneNumber) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateSalary = () => {
      if (!employee.value.salary) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      employee,
      onUpdateEmployee,
      validateName,
      validateLastname,
      validateWorkStation,
      validateAddress,
      validatePhoneNumber,
      validateSalary
    }
  }
}
</script>

<style></style>
