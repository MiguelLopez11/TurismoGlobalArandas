<template>
  <el-card>
    <Form
      ref="EmployeeForm"
      as="el-form"
      :validation-schema="editValidationSchema"
      @submit="onUpdateEmployee"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" v-slot="{ errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-model="employee.name"
                :validate-event="false"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="lastname" v-slot="{ errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Apellidos </label>
              </div>
              <el-input
                placeholder="Ingresa los apellidos del empleado"
                size="large"
                v-model="employee.lastname"
                :validate-event="false"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="workStation" v-slot="{  errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Puesto de trabajo</label>
              </div>
              <el-input
                placeholder="Ingresa el puesto de trabajo del empleado"
                size="large"
                v-model="employee.workStation"
                :validate-event="false"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="address" v-slot="{ errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Dirección</label>
              </div>
              <el-input
                placeholder="Ingresa la dirección del empleado"
                size="large"
                v-model="employee.address"
                :validate-event="false"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="phoneNumber" v-slot="{ errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Numero de telefono</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de telefono del empleado"
                size="large"
                v-model="employee.phoneNumber"
                :validate-event="false"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="salary" v-slot="{ errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Salario</label>
              </div>
              <el-input
                placeholder="Ingresa el salario del empleado"
                size="large"
                v-model="employee.salary"
                :validate-event="false"
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
                isOpenedDialog = !isOpenedDialog
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

    return {
      employee,
      onUpdateEmployee
    }
  }
}
</script>

<style></style>
