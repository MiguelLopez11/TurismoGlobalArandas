import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ReservationFlightDestinationsServices () {
  const getReservationFlightDestinations = (callback) => {
    axiosPrivate.get('/ReservationFlightDestinations').then((response) => {
      callback(response.data)
    })
  }
  const getReservationFlightDestination = (reservationFlightDestinationId, callback) => {
    axiosPrivate.get(`/ReservationFlightDestinations/${reservationFlightDestinationId}`).then((response) => {
      callback(response.data)
    })
  }
  const getReservationFlightDestinationByFlightId = (FlightId, callback) => {
    axiosPrivate.get(`/ReservationFlightDestinations/Flight/${FlightId}`).then((response) => {
      callback(response.data)
    })
  }
  const createReservationFlightDestination = (data, callback) => {
    axiosPrivate.post('/ReservationFlightDestinations', data).then((response) => {
      callback(response.data)
    })
  }
  const updateReservationFlightDestination = (data, callback) => {
    axiosPrivate.put(`/ReservationFlightDestinations/${data.reservationFlightDestinationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteReservationFlightDestination = (reservationFlightDestinationId, callback) => {
    axiosPrivate.delete(`/ReservationFlightDestinations/${reservationFlightDestinationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getReservationFlightDestinations,
    getReservationFlightDestination,
    getReservationFlightDestinationByFlightId,
    createReservationFlightDestination,
    updateReservationFlightDestination,
    deleteReservationFlightDestination
  }
}
