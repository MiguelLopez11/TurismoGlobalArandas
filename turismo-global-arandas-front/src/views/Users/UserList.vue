<template>
  <user-add-new />
  <el-card>
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
            :items="users"
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
                    <el-dropdown-item
                      @click="
                        () => {
                          onOpenDialog(items.userName)
                        }
                      "
                      >Actualizar contraseña</el-dropdown-item
                    >
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
            </template>
            <template #item-name="items">
              <p>{{ items.employee ? items.employee.name : '' }}</p>
            </template>
            <template #item-workStation="items">
              <p>{{ items.employee ? items.employee.workStation : '' }}</p>
            </template>
            <template #item-phoneNumber="items">
              <p>{{ items.employee ? items.employee.phoneNumber : '' }}</p>
            </template>
          </EasyDataTable>
        </div>
      </el-col>
    </el-row>
  </el-card>
  <el-dialog
    v-model="isChangePassword"
    title="Actualizar contraseña"
    width="80%"
    center
  >
    <Form @submit="onUpdatePassword" v-slot="{ errors }">
      <el-row :gutter="35" justify="center">
        <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
          <Field name="passwordOld" as="text">
            <el-form-item :error="errors.passwordOld" required>
              <div>
                <label>Contraseña Actual</label>
              </div>
              <el-input
                placeholder="Ingresa la contraseña"
                size="large"
                v-model="PasswordFields.passwordOld"
                type="password"
                show-password
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
          <Field name="passwordNew" as="text">
            <el-form-item :error="errors.passwordNew" required>
              <div>
                <label>Nueva contraseña</label>
              </div>
              <el-input
                placeholder="Ingresa la nueva contraseña"
                size="large"
                v-model="PasswordFields.passwordNew"
                type="password"
                show-password
              />
            </el-form-item>
          </Field>
        </el-col>
      </el-row>
      <el-row :gutter="25" justify="end">
        <el-col :span="3">
          <el-button
            color="#7367F0"
            class="w-100"
            native-type="submit"
            size="large"
            >Guardar</el-button
          >
        </el-col>
        <el-col :span="3">
          <el-button
            class="w-100"
            color="#F1F1F2"
            size="large"
            @click="isChangePassword = !isChangePassword"
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-dialog>
</template>

<script>
import { ref, watch, provide, inject } from 'vue'
import UserServices from '@/Services/User.Services'
import UserAddNew from './UserAddNew.vue'
import { useRouter } from 'vue-router'
export default {
  components: { UserAddNew },
  setup () {
    const { getUsers, deleteUser, updatePasswordUser } = UserServices()
    const redirect = useRouter()
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
    const isChangePassword = ref(false)
    const userNamePassword = ref()
    const userName = window.sessionStorage.getItem('UserName')
    provide('AddUser', isAddedUser)
    const fields = ref([
      { value: 'name', text: 'Empleado' },
      { value: 'workStation', text: 'Puesto de trabajo' },
      { value: 'userName', text: 'nombre de usuario' },
      { value: 'phoneNumber', text: 'Telefono' },
      { value: 'email', text: 'Correo electronico' },
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
      if (!newValue) {
        refreshTable()
      }
    })
    const PasswordFields = ref({
      passwordOld: null,
      passwordNew: null
    })
    const onOpenDialog = UserName => {
      isChangePassword.value = !isChangePassword.value
      userNamePassword.value = UserName
    }
    const PasswordFieldsBlank = ref(JSON.parse(JSON.stringify(PasswordFields)))
    const onUpdatePassword = () => {
      updatePasswordUser(userNamePassword.value, PasswordFields.value, data => {
        isChangePassword.value = false
        if (data.succeeded) {
          PasswordFields.value = JSON.parse(JSON.stringify(PasswordFieldsBlank))
          swal
            .fire({
              title: 'Contraseña actualizada!',
              text: 'El usuario ha cambiado su contraseña satisfactoriamente.',
              icon: 'success'
            })
            .then(result => {
              if (result.isConfirmed) {
                if (userName === userNamePassword.value) {
                  window.sessionStorage.removeItem('Token')
                  swal
                    .fire({
                      title: 'Has modificado tu propia contraseña!',
                      text: 'Se redireccionará al login para ingresar con tu nueva contraseña.',
                      icon: 'warning'
                    })
                    .then(result => {
                      if (result.isConfirmed) {
                        redirect.go('/Authenticate')
                      }
                    })
                }
              }
            })
        } else {
          PasswordFields.value = JSON.parse(JSON.stringify(PasswordFieldsBlank))
          swal.fire({
            title: 'Contraseña incorrecta!',
            text: 'Ha ingresado una contraseña que no es su actual, o no es válida.',
            icon: 'error'
          })
        }
      })
    }
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
      isChangePassword,
      PasswordFields,
      refreshTable,
      onDeleteUser,
      onOpenDialog,
      onUpdatePassword
    }
  }
}
</script>

<style></style>
