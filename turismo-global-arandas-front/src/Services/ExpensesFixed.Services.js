import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ExpenseFixedServices () {
  const getExpenses = (callback) => {
    axiosPrivate.get('/ExpenseFixed').then((response) => {
      callback(response.data)
    })
  }
  const getExpense = (expensesFixedId, callback) => {
    axiosPrivate.get(`/ExpenseFixed/${expensesFixedId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpense = (data, callback) => {
    axiosPrivate.post('/ExpenseFixed', data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpense = (data, callback) => {
    axiosPrivate.put(`/ExpenseFixed/${data.expensesFixedId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpense = (expensesFixedId, callback) => {
    axiosPrivate.delete(`/ExpenseFixed/${expensesFixedId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getExpenses,
    getExpense,
    createExpense,
    updateExpense,
    deleteExpense
  }
}
