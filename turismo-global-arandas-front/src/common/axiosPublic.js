import axios from 'axios'

export const axiosPublic = axios.create({
  baseURL: 'https://turismoglobalarandasservices.somee.com/api',
  headers: {
    'Content-Type': 'application/json'
  }
})
