import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentProviders () {
  const getPaymentProviders = (callback) => {
    axiosPrivate.get('/PaymentProviders').then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProvider = (PaymentMethodId, callback) => {
    axiosPrivate.get(`/PaymentProviders/${PaymentMethodId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentProvider = (data, callback) => {
    axiosPrivate.post('/PaymentProviders', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentProvider = (data, callback) => {
    axiosPrivate.put(`/PaymentProviders/${data.paymentMethodId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentProviderd = (PaymentMethodId, callback) => {
    axiosPrivate.delete(`/PaymentProviders/${PaymentMethodId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentProviders,
    getPaymentProvider,
    createPaymentProvider,
    updatePaymentProvider,
    deletePaymentProviderd
  }
}
