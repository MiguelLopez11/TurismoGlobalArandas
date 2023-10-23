import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function RouteServices () {
  const getRoutes = (callback) => {
    axiosPrivate.get('/Routes').then((response) => {
      callback(response.data)
    })
  }
  const getRoute = (RouteId, callback) => {
    axiosPrivate.get(`/Routes/${RouteId}`).then((response) => {
      callback(response.data)
    })
  }
  const createRoute = (data, callback) => {
    axiosPrivate.post('Routes', data).then((response) => {
      callback(response.data)
    })
  }
  const updateRoute = (data, callback) => {
    axiosPrivate.put(`/Routes/${data.routeId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteRoute = (RouteId, callback) => {
    axiosPrivate.delete(`/Routes/${RouteId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getRoutes,
    getRoute,
    createRoute,
    updateRoute,
    deleteRoute
  }
}
