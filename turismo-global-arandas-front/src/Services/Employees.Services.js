import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function EmployeeServices () {
  const getEmployees = (callback) => {
    axiosPrivate.get('/Employees').then((response) => {
      callback(response.data)
    })
  }
  const getEmployee = (employeeId, callback) => {
    axiosPrivate.get(`/Employees/${employeeId}`).then((response) => {
      callback(response.data)
    })
  }
  const getReservationsByEmployee = (employeeId, callback) => {
    axiosPrivate.get(`/Employees/ReservationsByEmployee/${employeeId}`).then((response) => {
      callback(response.data)
    })
  }
  const createEmployee = (data, callback) => {
    axiosPrivate.post('/Employees', data).then((response) => {
      callback(response.data)
    })
  }
  const updateEmployee = (data, callback) => {
    axiosPrivate.put(`/Employees/${data.employeeId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteEmployee = (employeeId, callback) => {
    axiosPrivate.delete(`/Employees/${employeeId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getEmployees,
    getEmployee,
    getReservationsByEmployee,
    createEmployee,
    updateEmployee,
    deleteEmployee
  }
}
