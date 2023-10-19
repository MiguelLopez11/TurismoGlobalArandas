import { createStore } from 'vuex'

const store = createStore({
  state: {
    reservationHotelId: null, // Aquí se almacenará el ID
    hotelId: null, // Aquí se almacenará el ID
    hotels: null, // Aquí se almacenará el ID
    PaymentReservationId: null // Aquí se almacenará el ID
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
    }
  },
  getters: {
    getReservationHotelId: state => state.reservationHotelId,
    getHotelId: state => state.hotelId,
    getHotels: state => state.hotels,
    getPaymentReservationId: state => state.paymentReservationId
  }
})

export default store
