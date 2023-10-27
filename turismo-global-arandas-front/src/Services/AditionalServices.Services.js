import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function AditionalServiceServices () {
  const getAditionalServices = (callback) => {
    axiosPrivate.get('/AditionalServices').then((response) => {
      callback(response.data)
    })
  }
  const getAditionalService = (aditionalServiceId, callback) => {
    axiosPrivate.get(`/AditionalServices/${aditionalServiceId}`).then((response) => {
      callback(response.data)
    })
  }
  const createAditionalService = (data, callback) => {
    axiosPrivate.post('/AditionalServices', data).then((response) => {
      callback(response.data)
    })
  }
  const updateAditionalService = (data, callback) => {
    axiosPrivate.put(`/AditionalServices/${data.aditionalServiceId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteAditionalService = (aditionalServiceId, callback) => {
    axiosPrivate.delete(`/AditionalServices/${aditionalServiceId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getAditionalServices,
    getAditionalService,
    createAditionalService,
    updateAditionalService,
    deleteAditionalService
  }
}
