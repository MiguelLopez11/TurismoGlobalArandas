<template>
<hotels-add-new />
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar Hotel..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddHotel = !isAddHotel"
        >
          <i> Nuevo Hotel </i>
        </el-button>
      </el-col>
    </el-row>
    <el-row class="mt-3">
      <el-col :span="24">
        <div class="table-scroll">
          <EasyDataTable
            rows-per-page-message="registro por pagina"
            empty-message="No se en cuentran registros"
            table-class-name="customize-table"
            buttons-pagination
            header-text-direction="center"
            body-text-direction="center"
            border-cell
            theme-color="#7367F0"
            :rows-per-page="10"
            :loading="isloading"
            :headers="fields"
            :items="hotels"
            :search-field="searchField"
            :search-value="searchValue"
          >
            <template #header-actions="header">
              {{ header.text }}
            </template>
            <template #item-actions="items">
              <el-dropdown>
                <span class="bi bi-three-dots-vertical"> </span>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item
                      @click="
                        () => {
                          $router.push({
                            name: 'Edit-Hotel',
                            params: { HotelId: items.hotelId }
                          })
                        }
                      "
                      >Editar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeleteHotel(items.hotelId)"
                      >Eliminar</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import HotelsServices from '@/Services/Hotels.Services'
import HotelsAddNew from './HotelsAddNew.vue'

export default {
  components: { HotelsAddNew },
  setup () {
    const { getHotels, deleteHotel } = HotelsServices()
    const hotels = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddHotel = ref(false)
    provide('addHotel', isAddHotel)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'destination.name', text: 'Destino' },
      { value: 'description', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getHotels(data => {
      hotels.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getHotels(data => {
        hotels.value = data
        isloading.value = false
      })
    }
    watch(isAddHotel, newValue => {
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteHotel = hotelId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un proveedor, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar hotel',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteHotel(hotelId, data => {
              swal.fire({
                title: 'hotel eliminado!',
                text: 'El hotel ha sido eliminado satisfactoriamente .',
                icon: 'success'
              })
              refreshTable()
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      filter,
      perPage,
      currentPage,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,
      hotels,
      isAddHotel,
      refreshTable,
      onDeleteHotel
    }
  }
}
</script>

<style></style>
