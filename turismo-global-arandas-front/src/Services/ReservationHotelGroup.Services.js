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
    }).catch((exception) => {
      callback(exception.response.status)
    })
  }
  const createReservationHotelGroup = (data, callback) => {
    axiosPrivate.post('/ReservationHotelGroup', data).then((response) => {
      callback(response.data)
    })
  }
  const comprobateIfExist = (ReservationHotelId, callback) => {
    axiosPrivate.post(`/ReservationHotelGroup/IfExist/${ReservationHotelId}`).then((response) => {
      callback(response.data)
    }).catch((exception) => {
      callback(exception.response)
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
    comprobateIfExist,
    updateReservationHotelGroup,
    deleteReservationHotelGroup
  }
}
