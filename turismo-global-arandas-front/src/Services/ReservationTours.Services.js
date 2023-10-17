import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationTourServices () {
  const getReservationTours = (callback) => {
    axiosPrivate.get('/ReservationTours').then((response) => {
      callback(response.data)
    })
  }
  const getReservationTour = (reservationTourId, callback) => {
    axiosPrivate.get(`/ReservationTours/${reservationTourId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationTour = (data, callback) => {
    axiosPrivate.post('/ReservationTours', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationTour = (data, callback) => {
    axiosPrivate.put(`/ReservationTours/${data.reservationTourId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationTour = (reservationTourId, callback) => {
    axiosPrivate.delete(`/ReservationTours/${reservationTourId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationTours,
    getReservationTour,
    createReservationTour,
    updateReservationTour,
    deleteReservationTour
  }
}
