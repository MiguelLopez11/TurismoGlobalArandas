import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function HabitationServices () {
  const getHabitations = (callback) => {
    axiosPrivate.get('/Habitations').then((response) => {
      callback(response.data)
    })
  }
  const getHabitation = (habitationId, callback) => {
    axiosPrivate.get(`/Habitations/${habitationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createHabitation = (data, callback) => {
    axiosPrivate.post('/Habitations', data).then((response) => {
      callback(response.data)
    })
  }
  const updateHabitation = (data, callback) => {
    axiosPrivate.put(`/Habitations/${data.habitationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteHabitation = (habitationId, callback) => {
    axiosPrivate.delete(`/Habitations/${habitationId}`).then((response) => {
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
