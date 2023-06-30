import axios from 'axios'

export const axiosPublic = axios.create({
  baseURL: 'http://localhost:7207/api',
  headers: {
    'Content-Type': 'application/json'
  }
})
