import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationHotelAditionalService () {
  const getReservationHotelAditionalServices = (callback) => {
    axiosPrivate.get('/ReservationHotelAditionalServices').then((response) => {
      callback(response.data)
    })
  }
  const getReservationHotelAditionalService = (id, callback) => {
    axiosPrivate.get(`/ReservationHotelAditionalServices/${id}`).then((response) => {
      callback(response.data)
    })
  }
  const getReservationAditionalServiceByReservationHotelId = (ReservationHotelId, callback) => {
    axiosPrivate.get(`/ReservationHotelAditionalServices/ReservationHotel/${ReservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationHotelAditionalService = (data, callback) => {
    axiosPrivate.post('/ReservationHotelAditionalServices', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationHotelAditionalService = (data, callback) => {
    axiosPrivate.put(`/ReservationHotelAditionalServices/${data.id}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationHotelAditionalService = (id, callback) => {
    axiosPrivate.delete(`/ReservationHotelAditionalServices/${id}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationHotelAditionalServices,
    getReservationHotelAditionalService,
    getReservationAditionalServiceByReservationHotelId,
    createReservationHotelAditionalService,
    updateReservationHotelAditionalService,
    deleteReservationHotelAditionalService
  }
}
