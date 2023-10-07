import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function TypeReservationGrupalservices () {
  const getTypeReservationGrupals = (callback) => {
    axiosPrivate.get('/TypeReservationGrupal').then((response) => {
      callback(response.data)
    })
  }
  const getTypeReservationGrupal = (typeReservationGrupalId, callback) => {
    axiosPrivate.get(`/TypeReservationGrupal/${typeReservationGrupalId}`).then((response) => {
      callback(response.data)
    })
  }
  const createTypeReservationGrupal = (data, callback) => {
    axiosPrivate.post('/TypeReservationGrupal', data).then((response) => {
      callback(response.data)
    })
  }
  const updateTypeReservationGrupal = (data, callback) => {
    axiosPrivate.put(`/TypeReservationGrupal/${data.typeReservationGrupalId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteTypeReservationGrupal = (typeReservationGrupalId, callback) => {
    axiosPrivate.delete(`/TypeReservationGrupal/${typeReservationGrupalId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getTypeReservationGrupals,
    getTypeReservationGrupal,
    createTypeReservationGrupal,
    updateTypeReservationGrupal,
    deleteTypeReservationGrupal
  }
}
