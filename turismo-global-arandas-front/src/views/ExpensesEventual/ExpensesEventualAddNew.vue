<template>
  <el-dialog v-model="isOpenDialog" title="Nuevo gasto" width="50%" center>
    <Form
      ref="expenseFormRef"
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
                placeholder="Ingresa un nombre del gasto"
                size="large"
                v-model="expensesFields.name"
                v-bind="field"
                :validate-event="false"
                :model-value="value"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
          <Field name="cost" v-slot="{ value, field, errorMessage }">
            <el-form-item :error="errorMessage" required>
              <div>
                <label> Costo </label>
              </div>
              <el-input
                placeholder="Ingresa el costo..."
                size="large"
                v-model="expensesFields.cost"
                v-bind="field"
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
              placeholder="Ingresa una descripción..."
              size="large"
              v-model="expensesFields.description"
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
import ExpenseEventualServices from '@/Services/ExpensesEventual.Services'
import * as yup from 'yup'

export default {
  setup () {
    const isOpenDialog = inject('addExpenseEventual')
    const swal = inject('$swal')
    const expenseFormRef = ref(null)
    const { createExpense } = ExpenseEventualServices()
    const validationSchema = yup.object({
      name: yup.string().required('Este campo es requerido').label('Nombre'),
      cost: yup
        .number()
        .test('is-decimal', 'Ingresa un valor válido', value =>
          (value + '').match(/^\d*\.?\d*$/)
        )
        .required('Este campo es requerido')
    })
    const expensesFields = ref({
      expenseId: 0,
      name: null,
      description: null,
      cost: null,
      isDeleted: false
    })
    const expensesFieldsBlank = ref(JSON.parse(JSON.stringify(expensesFields)))

    const onSubmit = () => {
      createExpense(expensesFields.value, data => {
        swal.fire({
          title: '¡Nuevo gasto registrado!',
          text: 'El gasto se ha registrado correctamente',
          icon: 'success'
        })
        isOpenDialog.value = false
        expensesFields.value = JSON.parse(JSON.stringify(expensesFieldsBlank))
        expenseFormRef.value.resetForm()
      })
    }

    return {
      isOpenDialog,
      onSubmit,
      validationSchema,
      expensesFields,
      expenseFormRef
    }
  }
}
</script>

<style></style>
