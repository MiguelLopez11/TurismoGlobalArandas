import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CustomerServices () {
  const getCustomers = (callback) => {
    axiosPrivate.get('/Customer').then((response) => {
      callback(response.data)
    })
  }
  const getCustomer = (customerId, callback) => {
    axiosPrivate.get(`/Customer/${customerId}`).then((response) => {
      callback(response.data)
    })
  }
  const createCustomer = (data, callback) => {
    axiosPrivate.post('/Customer', data).then((response) => {
      callback(response.data)
    })
  }
  const updateCustomer = (data, callback) => {
    axiosPrivate.put(`/Customer/${data.customerId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteCustomer = (customerId, callback) => {
    axiosPrivate.delete(`/Customer/${customerId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getCustomers,
    getCustomer,
    createCustomer,
    updateCustomer,
    deleteCustomer
  }
}
