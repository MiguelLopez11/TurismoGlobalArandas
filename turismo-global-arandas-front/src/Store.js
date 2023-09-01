import { createStore } from 'vuex'

const store = createStore({
  state: {
    reservationHotelId: null, // Aquí se almacenará el ID
    hotelId: null // Aquí se almacenará el ID
  },
  mutations: {
    setReservationHotelId (state, id) {
      state.reservationHotelId = id
    },
    setHotelId (state, id) {
      state.hotelId = id
    }
  },
  getters: {
    getReservationHotelId: state => state.reservationHotelId,
    getHotelId: state => state.hotelId
  }
})

export default store
