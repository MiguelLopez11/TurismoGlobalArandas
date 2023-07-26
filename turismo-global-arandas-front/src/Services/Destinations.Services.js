import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function DestinationServices () {
  const getDestinations = (callback) => {
    axiosPrivate.get('/Destinations').then((response) => {
      callback(response.data)
    })
  }
  const getDestination = (destinationId, callback) => {
    axiosPrivate.get(`/Destinations/${destinationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createDestination = (data, callback) => {
    axiosPrivate.post('/Destinations', data).then((response) => {
      callback(response.data)
    })
  }
  const updateDestination = (data, callback) => {
    axiosPrivate.put(`/Destinations/${data.destinationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteDestination = (destinationId, callback) => {
    axiosPrivate.delete(`/Destinations/${destinationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getDestinations,
    getDestination,
    createDestination,
    updateDestination,
    deleteDestination
  }
}
