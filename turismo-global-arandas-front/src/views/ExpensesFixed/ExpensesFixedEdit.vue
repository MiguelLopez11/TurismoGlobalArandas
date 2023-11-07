<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateExpenseEventual">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del concepto"
                size="large"
                v-model="expenseEventual.name"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="cost" :rules="validateCost">
            <el-form-item :error="errors.cost" required>
              <div>
                <label> Costo </label>
              </div>
              <el-input
                placeholder="Ingresa el costo..."
                size="large"
                v-model="expenseEventual.cost"
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
                placeholder="Ingresa una descripción del concepto"
                size="large"
                v-model="expenseEventual.description"
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
                $router.push('/GastosFijos')
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
import ExpenseFixedServices from '@/Services/ExpensesFixed.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getExpense, updateExpense } = ExpenseFixedServices()
    const expenseEventual = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getExpense(router.params.ExpenseFixedId, data => {
      expenseEventual.value = data
    })
    const onUpdateExpenseEventual = () => {
      updateExpense(expenseEventual.value, data => {
        swal
          .fire({
            title: 'Gasto fijo modificado correctamente',
            text: 'El gasto fijo se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/GastosFijos')
            }
          })
      })
    }
    const validateName = () => {
      if (!expenseEventual.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateCost = () => {
      if (!expenseEventual.value.cost) {
        return 'Este campo es requerido'
      }
      if (!/^\d*\.?\d*$/.test(expenseEventual.value.cost)) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      expenseEventual,
      onUpdateExpenseEventual,
      validateName,
      validateCost
    }
  }
}
</script>

<style></style>
