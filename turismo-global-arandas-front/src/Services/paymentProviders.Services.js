import { axiosPrivate } from '@/common/axiosPrivate.js'

export default function PaymentProvidersServices () {
  const getPaymentProviders = (callback) => {
    axiosPrivate.get('/PaymentProviders').then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProvider = (PaymentMethodId, callback) => {
    axiosPrivate.get(`/PaymentProviders/${PaymentMethodId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProviderByReservationHotel = (ReservationHolelId, callback) => {
    axiosPrivate.get(`/PaymentProviders/ReservacionHotel/${ReservationHolelId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProviderByReservationTour = (ReservationTourId, callback) => {
    axiosPrivate.get(`/PaymentProviders/ReservacionTour/${ReservationTourId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProviderByReservationFlight = (ReservationFlightId, callback) => {
    axiosPrivate.get(`/PaymentProviders/ReservacionVuelo/${ReservationFlightId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProviderByReservationVehicle = (ReservationVehicleId, callback) => {
    axiosPrivate.get(`/PaymentProviders/ReservacionVehiculo/${ReservationVehicleId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentProvider = (data, callback) => {
    axiosPrivate.post('/PaymentProviders', data).then((response) => {
      callback(response.data)
    })
  }
  const updatePaymentProvider = (data, callback) => {
    axiosPrivate.put(`/PaymentProviders/${data.paymentMethodId}`, data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentProvider = (PaymentMethodId, callback) => {
    axiosPrivate.delete(`/PaymentProviders/${PaymentMethodId}`).then((response) => {
      callback(response.data)
    })
  }
  const getPaymentProviderList = (paymentProviderId, callback) => {
    axiosPrivate.get(`PaymentProviderList/PaymentsProvider/${paymentProviderId}`).then((response) => {
      callback(response.data)
    })
  }
  const createPaymentProviderList = (data, callback) => {
    axiosPrivate.post('/PaymentProviderList', data).then((response) => {
      callback(response.data)
    })
  }
  const deletePaymentProviderList = (paymentProviderId, callback) => {
    axiosPrivate.delete(`/PaymentProviderList/${paymentProviderId}`).then((response) => {
      callback(response.data)
    })
  }
  return {
    getPaymentProviders,
    getPaymentProvider,
    getPaymentProviderByReservationHotel,
    getPaymentProviderByReservationTour,
    getPaymentProviderByReservationFlight,
    getPaymentProviderByReservationVehicle,
    createPaymentProvider,
    updatePaymentProvider,
    deletePaymentProvider,
    getPaymentProviderList,
    createPaymentProviderList,
    deletePaymentProviderList
  }
}
