import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationHotelServices () {
  const getReservationHotels = (callback) => {
    axiosPrivate.get('/ReservationHotel').then((response) => {
      callback(response.data)
    })
  }
  const getReservationHotel = (reservationHotelId, callback) => {
    axiosPrivate.get(`/ReservationHotel/${reservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationHotel = (data, callback) => {
    axiosPrivate.post('/ReservationHotel', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationHotel = (data, callback) => {
    axiosPrivate.put(`/ReservationHotel/${data.reservationHotelId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationHotel = (reservationHotelId, callback) => {
    axiosPrivate.delete(`/ReservationHotel/${reservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationHotels,
    getReservationHotel,
    createReservationHotel,
    updateReservationHotel,
    deleteReservationHotel
  }
}
