import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationFlightServices () {
  const getReservationFlights = (callback) => {
    axiosPrivate.get('/ReservationFlight').then((response) => {
      callback(response.data)
    })
  }
  const getReservationFlightsReservadedByReservationHotel = (callback) => {
    axiosPrivate.get('/ReservationFlight/ReservadedByReservationHotel').then((response) => {
      callback(response.data)
    })
  }
  const getReservationFlight = (reservationFlightId, callback) => {
    axiosPrivate.get(`/ReservationFlight/${reservationFlightId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationFlight = (data, callback) => {
    axiosPrivate.post('/ReservationFlight', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationFlight = (data, callback) => {
    axiosPrivate.put(`/ReservationFlight/${data.flightId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationFlight = (reservationFlightId, callback) => {
    axiosPrivate.delete(`/ReservationFlight/${reservationFlightId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationFlights,
    getReservationFlightsReservadedByReservationHotel,
    getReservationFlight,
    createReservationFlight,
    updateReservationFlight,
    deleteReservationFlight
  }
}
