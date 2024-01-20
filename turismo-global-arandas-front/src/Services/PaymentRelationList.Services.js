import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentsRelationListServices () {
  const getPaymentRelationListByPaymentReservationHotel = (PaymentReservationHotelId, callback) => {
    axiosPrivate.get(`/PaymentRelationList/PaymentReservationHotel/${PaymentReservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentRelationList = (paymentId, callback) => {
    axiosPrivate.get(`/PaymentRelationList/${paymentId}`).then((response) => {
      callback(response.data)
    })
  }
  const downloadPayment = (paymentId, callback) => {
    axiosPrivate.get(`/PaymentRelationList/DescargarReciboEnPDF/${paymentId}`, {
      responseType: 'blob'
    }).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentRelationList = (data, callback) => {
    axiosPrivate.post('/PaymentRelationList', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentRelationList = (data, callback) => {
    axiosPrivate.put(`/PaymentRelationList/${data.paymentId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentRelationList = (paymentId, callback) => {
    axiosPrivate.delete(`/PaymentRelationList/${paymentId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentRelationListByPaymentReservationHotel,
    getPaymentRelationList,
    downloadPayment,
    createPaymentRelationList,
    updatePaymentRelationList,
    deletePaymentRelationList
  }
}
