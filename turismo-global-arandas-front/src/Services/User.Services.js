import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function UserServices () {
  const getUsers = (callback) => {
    axiosPrivate.get('/Users').then((response) => {
      callback(response.data)
    })
  }
  const getUser = (userName, callback) => {
    axiosPrivate.get(`/Users/${userName}`).then((response) => {
      callback(response.data)
    })
  }
  const createUser = (data, callback) => {
    axiosPrivate.post('/Users/Register', data).then((response) => {
      callback(response.data)
    })
  }
  const updateUser = (data, callback) => {
    axiosPrivate.put(`/Authenticate/Users/${data.userName}`, data).then((response) => {
      callback(response.data)
    })
  }
  const updatePasswordUser = (userName, data, callback) => {
    axiosPrivate.put(`/Users/${userName}/Password/${data.oldPassword}/${data.newPassword}`).then((response) => {
      callback(response.data)
    })
  }
  const deleteUser = (userName, callback) => {
    axiosPrivate.delete(`/Users/${userName}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getUsers,
    getUser,
    createUser,
    updateUser,
    updatePasswordUser,
    deleteUser
  }
}
