<template>
  <el-dialog v-model="isOpenedDialog" title="Nuevo Empleado" width="75%" center>
    <el-form label-position="top" size="large" :model="employeeFields">
      <el-row :gutter="35">
        <el-col :lg="8">
          <el-form-item label="Nombre" required>
            <el-input v-model="employeeFields.name" />
          </el-form-item>
        </el-col>
        <el-col :lg="8">
          <el-form-item label="Apellidos">
            <el-input v-model="employeeFields.lastname" />
          </el-form-item>
        </el-col>
        <el-col :lg="8">
          <el-form-item label="Puesto de trabajo">
            <el-input v-model="employeeFields.workStation" />
          </el-form-item>
        </el-col>
        <el-col :lg="8">
          <el-form-item label="Direccion">
            <el-input v-model="employeeFields.address" />
          </el-form-item>
        </el-col>
        <el-col :lg="8">
          <el-form-item label="Numero de celular">
            <el-input v-model="employeeFields.phoneNumber" />
          </el-form-item>
        </el-col>
        <el-col :lg="8">
          <el-form-item label="Salario">
            <el-input v-model="employeeFields.salary" />
          </el-form-item>
        </el-col>
        <el-col>
          <el-form-item>
            <el-button type="primary" @click="onAddEmployee">Create</el-button>
            <el-button>Cancel</el-button>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import { ref, inject } from 'vue'
export default {
  setup () {
    const isOpenedDialog = inject('AddEmployee')
    const employeeFields = ref({
      employeeId: 0,
      name: null,
      lastname: null,
      workStation: null,
      address: null,
      phoneNumber: null,
      salary: null,
      archived: false
    })
    const onAddEmployee = () => {
      console.log('holi')
    }
    return {
      isOpenedDialog,
      employeeFields,
      onAddEmployee
    }
  }
}
</script>

<style scoped></style>
