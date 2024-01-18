import { createStore } from 'vuex'

const store = createStore({
  state: {
    reservationHotelId: null,
    hotelId: null,
    hotels: null,
    PaymentReservationId: null,
    paymentAmountTotal: null,
    paymentProviderAmountMissing: null,
    refreshPaymentRelation: false,
    refreshPaymentProvider: false,
    reservationHotelGroupId: null,
    paymentProviderId: null
  },
  mutations: {
    setReservationHotelId (state, id) {
      state.reservationHotelId = id
    },
    setHotelId (state, id) {
      state.hotelId = id
    },
    setHotels (state, lenght) {
      state.hotels = lenght
    },
    setPaymentReservationId (state, id) {
      state.paymentReservationId = id
    },
    setPaymentAmountTotal (state, amount) {
      state.paymentAmountTotal = amount
    },
    setPaymentProviderAmountMissing (state, amount) {
      state.paymentProviderAmountMissing = amount
    },
    setRefreshPaymentRelation (state, isRefreshing) {
      state.refreshPaymentRelation = isRefreshing
    },
    setRefreshPaymentProvider (state, isRefreshing) {
      state.refreshPaymentProvider = isRefreshing
    },
    setReservationHotelGroupId (state, id) {
      state.reservationHotelGroupId = id
    },
    setPaymentProviderId (state, id) {
      state.paymentProviderId = id
    }
  },
  getters: {
    getReservationHotelId: state => state.reservationHotelId,
    getHotelId: state => state.hotelId,
    getHotels: state => state.hotels,
    getPaymentReservationId: state => state.paymentReservationId,
    getPaymentAmountTotal: state => state.paymentAmountTotal,
    getPaymentProviderAmountMissing: state => state.paymentProviderAmountMissing,
    getRefreshPaymentRelation: state => state.refreshPaymentRelation,
    getRefreshPaymentProvider: state => state.refreshPaymentProvider,
    getReservationHotelGroupId: state => state.reservationHotelGroupId,
    getPaymentProviderId: state => state.paymentProviderId
  }
})

export default store
