import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ExpenseEventualServices () {
  const getExpenses = (callback) => {
    axiosPrivate.get('/ExpensesEventual').then((response) => {
      callback(response.data)
    })
  }
  const getExpense = (ExpenseId, callback) => {
    axiosPrivate.get(`/ExpensesEventual/${ExpenseId}`).then((response) => {
      callback(response.data)
    })
  }
  const createExpense = (data, callback) => {
    axiosPrivate.post('/ExpensesEventual', data).then((response) => {
      callback(response.data)
    })
  }
  const updateExpense = (data, callback) => {
    axiosPrivate.put(`/ExpensesEventual/${data.expenseId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteExpense = (ExpenseId, callback) => {
    axiosPrivate.delete(`/ExpensesEventual/${ExpenseId}`).then((response) => {
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
