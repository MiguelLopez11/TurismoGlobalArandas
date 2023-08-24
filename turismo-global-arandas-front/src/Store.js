import { createStore } from 'vuex'

const store = createStore({
  state: {
    reservationHotelId: null // Aquí se almacenará el ID
  },
  mutations: {
    setReservationHotelId (state, id) {
      state.reservationHotelId = id
    }
  },
  getters: {
    getReservationHotelId: state => state.reservationHotelId
  }
})

export default store
