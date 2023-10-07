import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationHotelGroupServices () {
  const getReservationsHotelGroup = (callback) => {
    axiosPrivate.get('/ReservationHotelGroup').then((response) => {
      callback(response.data)
    })
  }
  const getReservationHotelGroup = (reservationHotelGroupId, callback) => {
    axiosPrivate.get(`/ReservationHotelGroup/${reservationHotelGroupId}`).then((response) => {
      callback(response.data)
    })
  }
  const getReservationHotelGroupByreservationHotel = (reservationHotelGroupId, callback) => {
    axiosPrivate.get(`/ReservationHotelGroup/ReservationHotel/${reservationHotelGroupId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationHotelGroup = (data, callback) => {
    axiosPrivate.post('/ReservationHotelGroup', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationHotelGroup = (data, callback) => {
    axiosPrivate.put(`/ReservationHotelGroup/${data.reservationHotelGroupId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationHotelGroup = (reservationHotelGroupId, callback) => {
    axiosPrivate.delete(`/ReservationHotelGroup/${reservationHotelGroupId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationsHotelGroup,
    getReservationHotelGroup,
    getReservationHotelGroupByreservationHotel,
    createReservationHotelGroup,
    updateReservationHotelGroup,
    deleteReservationHotelGroup
  }
}
