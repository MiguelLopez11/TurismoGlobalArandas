import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function CommissionServices () {
  const getCommissions = (callback) => {
    axiosPrivate.get('/Commissions').then((response) => {
      callback(response.data)
    })
  }
  const getCommission = (commissionId, callback) => {
    axiosPrivate.get(`/Commissions/${commissionId}`).then((response) => {
      callback(response.data)
    })
  }
  const getCommissionByProvider = (providerId, callback) => {
    axiosPrivate.get(`/Commissions/${providerId}`).then((response) => {
      callback(response.data)
    })
  }
  const createCommission = (data, callback) => {
    axiosPrivate.post('/Commissions', data).then((response) => {
      callback(response.data)
    })
  }
  const updateCommission = (data, callback) => {
    axiosPrivate.put(`/Commissions/${data.commissionId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteCommission = (commissionId, callback) => {
    axiosPrivate.delete(`/Commissions/${commissionId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getCommissions,
    getCommission,
    getCommissionByProvider,
    createCommission,
    updateCommission,
    deleteCommission
  }
}
