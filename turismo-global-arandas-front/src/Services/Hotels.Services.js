import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function HotelsServices () {
  const getHotels = (callback) => {
    axiosPrivate.get('/Hotels').then((response) => {
      callback(response.data)
    })
  }
  const getHotel = (hotelId, callback) => {
    axiosPrivate.get(`/Hotels/${hotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const createHotel = (data, callback) => {
    axiosPrivate.post('/Hotels', data).then((response) => {
      callback(response.data)
    })
  }
  const updateHotel = (data, callback) => {
    axiosPrivate.put(`/Hotels/${data.hotelId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteHotel = (hotelId, callback) => {
    axiosPrivate.delete(`/Hotels/${hotelId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getHotels,
    getHotel,
    createHotel,
    updateHotel,
    deleteHotel
  }
}
