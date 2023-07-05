import axios from 'axios'

export const axiosPublic = axios.create({
  baseURL: 'https://localhost:7207/api/',
  headers: {
    'Content-Type': 'application/json'
  }
})
