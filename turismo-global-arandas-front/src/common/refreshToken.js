import mem from 'mem'

import { axiosPublic } from './axiosPublic'

const refreshTokenFn = async () => {
  const token = window.sessionStorage.getItem('Token')
  const refreshTokens = window.sessionStorage.getItem('RefreshToken')

  try {
    const response = await axiosPublic.post('/Authenticate/refresh-token', {
      accessToken: token,
      refreshToken: refreshTokens
    })

    const { accessToken, refreshToken } = response.data
    if (!accessToken && !refreshToken) {
      window.sessionStorage.removeItem('Token')
      window.sessionStorage.removeItem('RefreshToken')
    }

    window.sessionStorage.setItem('Token', accessToken)
    window.sessionStorage.setItem('RefreshToken', refreshToken)

    return { accessToken: accessToken, refres: refreshToken }
  } catch (error) {
    window.sessionStorage.removeItem('Token')
    window.sessionStorage.removeItem('RefreshToken')
  }
}

const maxAge = 1000000

export const memoizedRefreshToken = mem(refreshTokenFn, {
  maxAge
})
