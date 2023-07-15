<template>
  <el-dialog v-model="isOpenedDialog" title="Nuevo Empleado" width="80%" center>
    <Form
      ref="EmployeeForm"
      as="el-form"
      :validation-schema="validationSchema"
      @submit="onSubmit"
    >
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del empleado"
                size="large"
                v-bind="field"
                v-model="EmployeesFields.name"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="lastname" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Apellidos </label>
              </div>
              <el-input
                placeholder="Ingresa los apellidos del empleado"
                size="large"
                v-bind="field"
                v-model="EmployeesFields.lastname"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="workStation" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Puesto de trabajo</label>
              </div>
              <el-input
                placeholder="Ingresa el puesto de trabajo del empleado"
                size="large"
                v-bind="field"
                v-model="EmployeesFields.workStation"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="address" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Dirección</label>
              </div>
              <el-input
                placeholder="Ingresa la dirección del empleado"
                size="large"
                v-bind="field"
                v-model="EmployeesFields.address"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="phoneNumber" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Numero de telefono</label>
              </div>
              <el-input
                placeholder="Ingresa el numero de telefono del empleado"
                size="large"
                v-bind="field"
                v-model="EmployeesFields.phoneNumber"
                :validate-event="false"
                :model-value="value"
                type="number"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="salary" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label>Salario</label>
              </div>
              <el-input
                placeholder="Ingresa el salario del empleado"
                size="large"
                v-bind="field"
                v-model="EmployeesFields.salary"
                :validate-event="false"
                :model-value="value"
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
  </el-dialog>
</template>

<script>
import { ref, inject } from 'vue'
import { Field, Form } from 'vee-validate'
import EmployeeServices from '@/Services/Employees.Services'
import * as yup from 'yup'

export default {
  components: {
    Form,
    Field
  },
  setup () {
    const isOpenedDialog = inject('AddEmployee')
    const swal = inject('$swal')
    const { createEmployee } = EmployeeServices()
    const validationSchema = yup.object({
      name: yup.string().required().label('Nombre'),
      lastname: yup.string().required().label('Apellidos'),
      workStation: yup.string().required().label('Puesto de trabajo'),
      address: yup.string().required().label('Direccion'),
      phoneNumber: yup.string().required().min(10).label('Numero de telefono'),
      salary: yup.string().required().label('Salario')
    })
    const EmployeesFields = ref({
      employeeId: 0,
      name: '',
      lastname: '',
      workStation: '',
      address: '',
      phoneNumber: '',
      salary: null,
      isDeleted: false
    })
    const EmployeesFieldsBlank = ref(
      JSON.parse(JSON.stringify(EmployeesFields))
    )

    const onSubmit = () => {
      createEmployee(EmployeesFields.value, data => {
        swal.fire({
          title: '¡Nuevo empleado registrado!',
          text: 'El empleado se ha registrado correctamente',
          icon: 'success'
        })
        isOpenedDialog.value = false
        EmployeesFields.value = JSON.parse(JSON.stringify(EmployeesFieldsBlank))
      })
    }

    return {
      isOpenedDialog,
      onSubmit,
      validationSchema,
      EmployeesFields
    }
  }
}
</script>

<style></style>
