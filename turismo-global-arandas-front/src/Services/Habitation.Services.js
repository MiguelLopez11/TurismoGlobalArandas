import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function HabitationServices () {
  const getHabitations = (callback) => {
    axiosPrivate.get('/Habitations').then((response) => {
      callback(response.data)
    })
  }
  const getHabitation = (HabitationId, callback) => {
    axiosPrivate.get(`/Habitations/${HabitationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createHabitation = (data, callback) => {
    axiosPrivate.post('/Habitations', data).then((response) => {
      callback(response.data)
    })
  }
  const updateHabitation = (data, callback) => {
    axiosPrivate.put(`/Habitations/${data.HabitationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteHabitation = (HabitationId, callback) => {
    axiosPrivate.delete(`/Habitations/${HabitationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getHabitations,
    getHabitation,
    createHabitation,
    updateHabitation,
    deleteHabitation
  }
}
