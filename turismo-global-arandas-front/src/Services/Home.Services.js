import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function HomeServices () {
  const getReservationsByMonth = (callback) => {
    axiosPrivate.get('/Home/ReservationsByMonth').then((response) => {
      callback(response.data)
    })
  }
  const getAllReservations = (callback) => {
    axiosPrivate.post('/Home/AllReservations').then((response) => {
      callback(response.data)
    })
  }
  const EmployeeWithMostReservations = (callback) => {
    axiosPrivate.get('/Home/EmployeeWithMostReservations').then((response) => {
      callback(response.data)
    })
  }
  const getTotalRevenue = (callback) => {
    axiosPrivate.post('/Home/TotalRevenue').then((response) => {
      callback(response.data)
    })
  }
  const MostPopularDestination = (callback) => {
    axiosPrivate.get('/Home/MostPopularDestination').then((response) => {
      callback(response.data)
    })
  }
  const getReservationsByEmployee = (callback) => {
    axiosPrivate.get('/Home/ReservationsByEmployee').then((response) => {
      callback(response.data)
    })
  }

  return {
    getReservationsByMonth,
    getAllReservations,
    EmployeeWithMostReservations,
    getTotalRevenue,
    MostPopularDestination,
    getReservationsByEmployee
  }
}
