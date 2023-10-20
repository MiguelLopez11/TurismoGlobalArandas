import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentsRelationReservationServices () {
  const getPaymentsRelationByReservationHotel = (reservationHotelId, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/ReservationHotel/${reservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentsRelationByReservationTour = (reservationTourId, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/ReservationTour/${reservationTourId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentRelation = (PaymentReservationId, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/${PaymentReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentRelation = (data, callback) => {
    axiosPrivate.post('/paymentRelationReservations', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentRelation = (data, callback) => {
    axiosPrivate.put(`/paymentRelationReservations/${data.paymentReservationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentRelation = (PaymentReservationId, callback) => {
    axiosPrivate.delete(`/paymentRelationReservations/${PaymentReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentsRelationByReservationHotel,
    getPaymentsRelationByReservationTour,
    getPaymentRelation,
    createPaymentRelation,
    updatePaymentRelation,
    deletePaymentRelation
  }
}
