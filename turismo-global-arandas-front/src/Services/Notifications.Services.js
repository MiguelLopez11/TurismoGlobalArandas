import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function NotificationServices () {
  const getNotifications = (callback) => {
    axiosPrivate.get('/Notifications').then((response) => {
      callback(response.data)
    })
  }
  const isReadNotification = (NotificationId, callback) => {
    axiosPrivate.put(`/Notifications/Readed/${NotificationId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getNotifications,
    isReadNotification
  }
}
