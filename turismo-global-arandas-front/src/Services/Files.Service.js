import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function FileServices () {
  const getFiles = (PaymentProviderId, callback) => {
    axiosPrivate.get(`/Files/${PaymentProviderId}`).then((response) => {
      callback(response.data)
    })
  }
  const downloadFile = (FileId, callback) => {
    axiosPrivate.get(`/Files/Download/${FileId}`).then((response) => {
      callback(response.data)
    })
  }
  const uploadFile = (PaymentProviderId, Files, callback) => {
    axiosPrivate.post(`/Files/${PaymentProviderId}`, Files).then((response) => {
      callback(response.data)
    }).catch((exception) => {
      callback(exception.response)
    })
  }
  const updateFile = (PaymentProviderId, File, callback) => {
    axiosPrivate.put(`/Files/${PaymentProviderId}`, File).then((response) => {
      callback(response.data)
    })
  }
  const deleteFile = (FileId, callback) => {
    axiosPrivate.delete(`/Files/${FileId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getFiles,
    downloadFile,
    uploadFile,
    updateFile,
    deleteFile
  }
}
