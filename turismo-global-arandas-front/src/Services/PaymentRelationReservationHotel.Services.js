import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentsRelationReservationServices () {
  const getPaymentsRelationByReservationHotel = (ReservationHotelId, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/ReservationHotel/${ReservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentsRelationByReservationTour = (ReservationTourId, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/ReservationTour/${ReservationTourId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentsRelationByReservationVehicle = (ReservationVehicle, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/ReservationVehicle/${ReservationVehicle}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentsRelationByReservationFlight = (ReservationFlightId, callback) => {
    axiosPrivate.get(`/paymentRelationReservations/ReservationFlight/${ReservationFlightId}`).then((response) => {
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
    getPaymentsRelationByReservationVehicle,
    getPaymentsRelationByReservationFlight,
    getPaymentRelation,
    createPaymentRelation,
    updatePaymentRelation,
    deletePaymentRelation
  }
}
