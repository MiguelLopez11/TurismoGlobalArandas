import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function IndividualRateServices () {
  const getIndividualRates = (callback) => {
    axiosPrivate.get('/IndividualRate').then((response) => {
      callback(response.data)
    })
  }
  const getIndividualRate = (individualRateId, callback) => {
    axiosPrivate.get(`/IndividualRate/${individualRateId}`).then((response) => {
      callback(response.data)
    })
  }
  const getIndividualRateByReservationHotel = (reservationHotelId, callback) => {
    axiosPrivate.get(`/IndividualRate/ReservationHotel/${reservationHotelId}`).then((response) => {
      callback(response.data)
    })
  }
  const createIndividualRate = (data, callback) => {
    axiosPrivate.post('/IndividualRate', data).then((response) => {
      callback(response.data)
    })
  }
  const updateIndividualRate = (data, callback) => {
    axiosPrivate.put(`/IndividualRate/${data.individualRateId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteIndividualRate = (individualRateId, callback) => {
    axiosPrivate.delete(`/IndividualRate/${individualRateId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getIndividualRates,
    getIndividualRate,
    getIndividualRateByReservationHotel,
    createIndividualRate,
    updateIndividualRate,
    deleteIndividualRate
  }
}
