import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function ProviderServices () {
  const getProviders = (callback) => {
    axiosPrivate.get('/Providers').then((response) => {
      callback(response.data)
    })
  }
  const getProvider = (providerId, callback) => {
    axiosPrivate.get(`/Providers/${providerId}`).then((response) => {
      callback(response.data)
    })
  }
  const createProvider = (data, callback) => {
    axiosPrivate.post('/Providers', data).then((response) => {
      callback(response.data)
    })
  }
  const updateProvider = (data, callback) => {
    axiosPrivate.put(`/Providers/${data.providerId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteProvider = (providerId, callback) => {
    axiosPrivate.delete(`/Providers/${providerId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getProviders,
    getProvider,
    createProvider,
    updateProvider,
    deleteProvider
  }
}
