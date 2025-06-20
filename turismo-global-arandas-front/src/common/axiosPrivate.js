import axios from 'axios'
import { memoizedRefreshToken } from './refreshToken'

axios.defaults.baseURL = 'https://localhost:7207/api/'

axios.interceptors.request.use(
  async (config) => {
    const token = window.sessionStorage.getItem('Token')
    if (token) {
      config.headers = {
        ...config.headers,
        authorization: `Bearer ${token}`
      }
    }

    return config
  },
  (error) => Promise.reject(error)
)

axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const config = error?.config

    if (error?.response?.status === 401 && !config?.sent) {
      config.sent = true

      const result = await memoizedRefreshToken()

      if (result) {
        config.headers = {
          ...config.headers,
          authorization: `Bearer ${result.accessToken}`
        }
      }

      return axios(config)
    }
    return Promise.reject(error)
  }
)

export const axiosPrivate = axios
