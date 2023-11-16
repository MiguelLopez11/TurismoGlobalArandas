import { createStore } from 'vuex'

const store = createStore({
  state: {
    reservationHotelId: null,
    hotelId: null,
    hotels: null,
    PaymentReservationId: null,
    paymentAmountTotal: null,
    refreshPaymentRelation: false
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
    setRefreshPaymentRelation (state, isRefreshing) {
      state.refreshPaymentRelation = isRefreshing
    }
  },
  getters: {
    getReservationHotelId: state => state.reservationHotelId,
    getHotelId: state => state.hotelId,
    getHotels: state => state.hotels,
    getPaymentReservationId: state => state.paymentReservationId,
    getPaymentAmountTotal: state => state.paymentAmountTotal,
    getRefreshPaymentRelation: state => state.refreshPaymentRelation
  }
})

export default store
