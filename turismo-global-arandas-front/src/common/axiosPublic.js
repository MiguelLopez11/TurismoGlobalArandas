import axios from 'axios'

export const axiosPublic = axios.create({
  baseURL: 'https://www.turismoglobalarandasservices.somee.com/api/',
  headers: {
    'Content-Type': 'application/json'
  }
})
