import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationServices () {
  const getReservations = (callback) => {
    axiosPrivate.get('/ReservationHotel').then((response) => {
      callback(response.data)
    })
  }
  const getReservation = (reservationHotelId, callback) => {
    axiosPrivate.get(`/ReservationHotel/${reservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservation = (data, callback) => {
    axiosPrivate.post('/ReservationHotel', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservation = (data, callback) => {
    axiosPrivate.put(`/ReservationHotel/${data.reservationHotelId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservation = (reservationHotelId, callback) => {
    axiosPrivate.delete(`/ReservationHotel/${reservationHotelId}`).then((response) => {
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
