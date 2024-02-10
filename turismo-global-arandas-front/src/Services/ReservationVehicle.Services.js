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
  const downloadPDFReport = (reservationVehicleId, callback) => {
    axiosPrivate.get(`/ReservationVehicle/DescargarDatosEnPDF/${reservationVehicleId}`, {
      responseType: 'blob'
    }).then((response) => {
      callback(response.data)
    })
  }
  const RemoveReservationVehicle = (reservationTourId, callback) => {
    axiosPrivate.delete(`/ReservationVehicle/Delete/${reservationTourId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationVehicles,
    getReservationVehicle,
    createReservationVehicle,
    updateReservationVehicle,
    deleteReservationVehicle,
    downloadPDFReport,
    RemoveReservationVehicle
  }
}
