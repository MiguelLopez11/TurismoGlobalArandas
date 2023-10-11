import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentsRelationReservationServices () {
  const getPaymentsRelationByReservationHotel = (reservationHotelId, callback) => {
    axiosPrivate.get(`/paymentRelationReservationHotel/ReservationHotel/${reservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentRelation = (PaymentReservationHotelId, callback) => {
    axiosPrivate.get(`/paymentRelationReservationHotel/${PaymentReservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentRelation = (data, callback) => {
    axiosPrivate.post('/paymentRelationReservationHotel', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentRelation = (data, callback) => {
    axiosPrivate.put(`/paymentRelationReservationHotel/${data.paymentReservationHotelId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentRelation = (PaymentReservationHotelId, callback) => {
    axiosPrivate.delete(`/paymentRelationReservationHotel/${PaymentReservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentsRelationByReservationHotel,
    getPaymentRelation,
    createPaymentRelation,
    updatePaymentRelation,
    deletePaymentRelation
  }
}
