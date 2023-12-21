import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function AirlineServices () {
  const getAirlines = (callback) => {
    axiosPrivate.get('/Airline').then((response) => {
      callback(response.data)
    })
  }
  const getAirline = (AirlineId, callback) => {
    axiosPrivate.get(`/Airline/${AirlineId}`).then((response) => {
      callback(response.data)
    })
  }
  const createAirline = (data, callback) => {
    axiosPrivate.post('/Airline', data).then((response) => {
      callback(response.data)
    })
  }
  const updateAirline = (data, callback) => {
    axiosPrivate.put(`/Airline/${data.airlineId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteAirline = (AirlineId, callback) => {
    axiosPrivate.delete(`/Airline/${AirlineId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getAirlines,
    getAirline,
    createAirline,
    updateAirline,
    deleteAirline
  }
}
