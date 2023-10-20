import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentMethodsServices () {
  const getPaymentMethods = (callback) => {
    axiosPrivate.get('/PaymentMethods').then((response) => {
      callback(response.data)
    })
  }
  const getPaymentMethod = (PaymentMethodId, callback) => {
    axiosPrivate.get(`/PaymentMethods/${PaymentMethodId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentMethod = (data, callback) => {
    axiosPrivate.post('/PaymentMethods', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentMethod = (data, callback) => {
    axiosPrivate.put(`/PaymentMethods/${data.paymentMethodId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentMethod = (PaymentMethodId, callback) => {
    axiosPrivate.delete(`/PaymentMethods/${PaymentMethodId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentMethods,
    getPaymentMethod,
    createPaymentMethod,
    updatePaymentMethod,
    deletePaymentMethod
  }
}
