import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CategoryReservationServices () {
  const getCategoryReservations = (callback) => {
    axiosPrivate.get('/CategoryReservation').then((response) => {
      callback(response.data)
    })
  }
  const getCategoryReservation = (categoryReservationId, callback) => {
    axiosPrivate.get(`/CategoryReservation/${categoryReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  const createCategoryReservation = (data, callback) => {
    axiosPrivate.post('/CategoryReservation', data).then((response) => {
      callback(response.data)
    })
  }
  const updateCategoryReservation = (data, callback) => {
    axiosPrivate.put(`/CategoryReservation/${data.categoryReservationId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteCategoryReservation = (categoryReservationId, callback) => {
    axiosPrivate.delete(`/CategoryReservation/${categoryReservationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getCategoryReservations,
    getCategoryReservation,
    createCategoryReservation,
    updateCategoryReservation,
    deleteCategoryReservation
  }
}
