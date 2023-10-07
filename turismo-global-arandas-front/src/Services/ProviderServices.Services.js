import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ServicesProviderServices () {
  const getServicesProvider = (callback) => {
    axiosPrivate.get('/ProviderServices').then((response) => {
      callback(response.data)
    })
  }
  const getServiceProvider = (serviceId, callback) => {
    axiosPrivate.get(`/ProviderServices/${serviceId}`).then((response) => {
      callback(response.data)
    })
  }
  const getServiceProviderByProviderId = (providerId, callback) => {
    axiosPrivate.get(`/ProviderServices/Provider/${providerId}`).then((response) => {
      callback(response.data)
    })
  }
  const createServiceProvider = (data, callback) => {
    axiosPrivate.post('/ProviderServices', data).then((response) => {
      callback(response.data)
    })
  }
  const updateServiceProvider = (data, callback) => {
    axiosPrivate.put(`/ProviderServices/${data.serviceId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteServiceProvider = (serviceId, callback) => {
    axiosPrivate.delete(`/ProviderServices/${serviceId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getServicesProvider,
    getServiceProvider,
    getServiceProviderByProviderId,
    createServiceProvider,
    updateServiceProvider,
    deleteServiceProvider
  }
}
