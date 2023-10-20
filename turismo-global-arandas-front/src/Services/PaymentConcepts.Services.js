import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentConceptsServices () {
  const getPaymentConcepts = (callback) => {
    axiosPrivate.get('/PaymentConcepts').then((response) => {
      callback(response.data)
    })
  }
  const getPaymentConcept = (PaymentConceptId, callback) => {
    axiosPrivate.get(`/PaymentConcepts/${PaymentConceptId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentConcept = (data, callback) => {
    axiosPrivate.post('/PaymentConcepts', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentConcept = (data, callback) => {
    axiosPrivate.put(`/PaymentConcepts/${data.paymentConceptId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentConcept = (PaymentConceptId, callback) => {
    axiosPrivate.delete(`/PaymentConcepts/${PaymentConceptId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentConcepts,
    getPaymentConcept,
    createPaymentConcept,
    updatePaymentConcept,
    deletePaymentConcept
  }
}
