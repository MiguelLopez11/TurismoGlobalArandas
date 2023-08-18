import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function HabitationReservationServices () {
  const getHabitationReservations = (callback) => {
    axiosPrivate.get('/HabitationReservation').then((response) => {
      callback(response.data)
    })
  }
  const getHabitationReservation = (habitationReservationId, callback) => {
    axiosPrivate.get(`/HabitationReservation/${habitationReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createHabitationReservation = (data, callback) => {
    axiosPrivate.post('/HabitationReservation', data).then((response) => {
      callback(response.data)
    })
  }
  const updateHabitationReservation = (data, callback) => {
    axiosPrivate.put(`/HabitationReservation/${data.habitationReservationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteHabitationReservation = (habitationReservationId, callback) => {
    axiosPrivate.delete(`/HabitationReservation/${habitationReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getHabitationReservations,
    getHabitationReservation,
    createHabitationReservation,
    updateHabitationReservation,
    deleteHabitationReservation
  }
}
