import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function TypeReservationServices () {
  const getTypeReservations = (callback) => {
    axiosPrivate.get('/TypeReservation').then((response) => {
      callback(response.data)
    })
  }
  const getTypeReservation = (typeReservationId, callback) => {
    axiosPrivate.get(`/TypeReservation/${typeReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createTypeReservation = (data, callback) => {
    axiosPrivate.post('/TypeReservation', data).then((response) => {
      callback(response.data)
    })
  }
  const updateTypeReservation = (data, callback) => {
    axiosPrivate.put(`/TypeReservation/${data.typeReservationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteTypeReservation = (typeReservationId, callback) => {
    axiosPrivate.delete(`/TypeReservation/${typeReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getTypeReservations,
    getTypeReservation,
    createTypeReservation,
    updateTypeReservation,
    deleteTypeReservation
  }
}
