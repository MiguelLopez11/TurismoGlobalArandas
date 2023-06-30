import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function AuthenticationServices () {
  const LogIn = (user, callback) => {
    axiosPrivate.post('Users/Login', user).then((response) => {
      callback(response.data)
    }).catch((exception) => {
      callback(exception.response)
    })
  }
  return {
    LogIn
  }
}
