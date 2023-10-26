import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationVehicleServices () {
  const getReservationVehicles = (callback) => {
    axiosPrivate.get('/ReservationVehicle').then((response) => {
      callback(response.data)
    })
  }
  const getReservationVehicle = (reservationVehicleId, callback) => {
    axiosPrivate.get(`/ReservationVehicle/${reservationVehicleId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationVehicle = (data, callback) => {
    axiosPrivate.post('/ReservationVehicle', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationVehicle = (data, callback) => {
    axiosPrivate.put(`/ReservationVehicle/${data.reservationVehicleId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationVehicle = (reservationVehicleId, callback) => {
    axiosPrivate.delete(`/ReservationVehicle/${reservationVehicleId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationVehicles,
    getReservationVehicle,
    createReservationVehicle,
    updateReservationVehicle,
    deleteReservationVehicle
  }
}
