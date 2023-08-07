import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationServices () {
  const getReservations = (callback) => {
    axiosPrivate.get('/Reservations').then((response) => {
      callback(response.data)
    })
  }
  const getReservation = (reservationId, callback) => {
    axiosPrivate.get(`/Reservations/${reservationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservation = (data, callback) => {
    axiosPrivate.post('/Reservations', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservation = (data, callback) => {
    axiosPrivate.put(`/Reservations/${data.reservationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservation = (reservationId, callback) => {
    axiosPrivate.delete(`/Reservations/${reservationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservations,
    getReservation,
    createReservation,
    updateReservation,
    deleteReservation
  }
}
