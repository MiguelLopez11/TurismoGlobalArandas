import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function GroupRateServices () {
  const getGroupRates = (callback) => {
    axiosPrivate.get('/GroupRate').then((response) => {
      callback(response.data)
    })
  }
  const getGroupRate = (groupRateId, callback) => {
    axiosPrivate.get(`/GroupRate/${groupRateId}`).then((response) => {
      callback(response.data)
    })
  }
  const getGroupRateByReservationHotelGroup = (reservationHotelGroupId, callback) => {
    axiosPrivate.get(`/GroupRate/ReservationHotelGroup/${reservationHotelGroupId}`).then((response) => {
      callback(response.data)
    })
  }
  const createGroupRate = (data, callback) => {
    axiosPrivate.post('/GroupRate', data).then((response) => {
      callback(response.data)
    })
  }
  const updateGroupRate = (data, callback) => {
    axiosPrivate.put(`/GroupRate/${data.groupRateId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deleteGroupRate = (groupRateId, callback) => {
    axiosPrivate.delete(`/GroupRate/${groupRateId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getGroupRates,
    getGroupRate,
    getGroupRateByReservationHotelGroup,
    createGroupRate,
    updateGroupRate,
    deleteGroupRate
  }
}
