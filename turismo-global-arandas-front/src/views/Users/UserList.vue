<template>
  <user-add-new />
  <el-card header="Usuarios">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-input
          v-model="searchValue"
          size="large"
          placeholder="Buscar usuario..."
        />
      </el-col>
      <el-col :xs="10" :sm="12" :md="6" :xl="3" :lg="4">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddedUser = !isAddedUser"
        >
          <i> Agregar usuario </i>
        </el-button>
      </el-col>
    </el-row>
    <el-row class="mt-3">
      <el-col :span="24">
        <EasyDataTable
          rows-per-page-message="registro por pagina"
          empty-message="No se en cuentran registros"
          table-class-name="customize-table"
          buttons-pagination
          border-cell
          :loading="isloading"
          :headers="fields"
          :items="users"
          :rows-per-page="5"
          :search-field="searchField"
          :search-value="searchValue"
          theme-color="#6366F1"
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
                          name: 'Edit-User',
                          params: { UserId: items.id }
                        })
                      }
                    "
                    >Editar</el-dropdown-item
                  >
                  <el-dropdown-item @click="onDeleteUser(items.userName)"
                    >Eliminar</el-dropdown-item
                  >
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </template>
        </EasyDataTable>
      </el-col>
    </el-row>
  </el-card>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import UserServices from '@/Services/User.Services'
import UserAddNew from './UserAddNew.vue'

export default {
  components: { UserAddNew },
  setup () {
    const { getUsers, deleteUser } = UserServices()
    const users = ref([])
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('name')
    const isAddedUser = ref(false)
    provide('AddUser', isAddedUser)
    const fields = ref([
      { value: 'employee.name', text: 'Empleado' },
      { value: 'employee.workStation', text: 'Puesto de trabajo' },
      { value: 'userName', text: 'nombre de usuario' },
      { value: 'employee.phoneNumber', text: 'Telefono' },
      { value: 'actions', text: 'Acciones' }
    ])
    getUsers(data => {
      users.value = data
      isloading.value = false
    })
    const refreshTable = () => {
      isloading.value = true
      getUsers(data => {
        users.value = data
        isloading.value = false
      })
    }
    watch(isAddedUser, newValue => {
      console.log(newValue)
      if (!newValue) {
        refreshTable()
      }
    })
    const onDeleteUser = employeeId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un usuario, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar usuario',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteUser(employeeId, data => {
              refreshTable()
              swal.fire({
                title: 'Usuario eliminado!',
                text: 'El usuario ha sido eliminado satisfactoriamente .',
                icon: 'success'
              })
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
      users,
      isAddedUser,
      refreshTable,
      onDeleteUser
    }
  }
}
</script>

<style></style>
