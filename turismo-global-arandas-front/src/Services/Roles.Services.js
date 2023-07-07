import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function RoleServices () {
  const getRoles = (callback) => {
    axiosPrivate.get('/Roles').then((response) => {
      callback(response.data)
    })
  }
  const getRole = (roleName, callback) => {
    axiosPrivate.get(`/Roles/${roleName}`).then((response) => {
      callback(response.data)
    })
  }
  const createRole = (data, callback) => {
    axiosPrivate.post('/Roles', data).then((response) => {
      callback(response.data)
    })
  }
  const updateRole = (data, callback) => {
    axiosPrivate.put(`/Roles/${data.name}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteRole = (roleName, callback) => {
    axiosPrivate.delete(`/Roles/${roleName}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getRoles,
    getRole,
    createRole,
    updateRole,
    deleteRole
  }
}
