<template>
  <el-card class="scrollable-card">
    <el-row :gutter="25" justify="end">
      <el-col :xs="13" :sm="12" :md="6" :xl="6" :lg="8">
        <el-button
          class="w-100"
          size="large"
          color="#7367F0"
          @click="isAddFile = !isAddFile"
        >
          <i> cargar nuevo documento </i>
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
            :items="files"
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
                    <el-dropdown-item @click="onDownloadFile(items)"
                      >Descargar</el-dropdown-item
                    >
                    <el-dropdown-item
                      @click="onDeletePaymentMethod(items.fileId)"
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
  <el-dialog v-model="isAddFile">
    <el-upload
      v-model:file-list="uploadFiles"
      drag
      action=""
      multiple
      :show-file-list="true"
      :auto-upload="false"
    >
      <template #trigger>
        <h1 class="bi bi-cloud-arrow-up" />
        <div class="m-2">Arrastra tu archivo aqui o <em>Click aqui</em></div>
        <el-button type="primary">Seleccionar archivo</el-button>
      </template>

      <template #tip>
        <div class="el-upload__tip">solamente archivos pdf/word/excel</div>
      </template>
    </el-upload>
    <el-row :gutter="25" justify="end">
      <el-col :span="24">
        <el-button size="large" type="success" @click="handleSuccess"
          >cargar archivos</el-button
        >
      </el-col>
    </el-row>
  </el-dialog>
</template>

<script>
import { ref, inject } from 'vue'
import FileServices from '@/Services/Files.Service'
import { useStore } from 'vuex'
export default {
  setup () {
    const { getFiles, deleteFile, downloadFile, uploadFile } = FileServices()
    const store = useStore()
    const uploadFiles = ref([])
    const files = ref([])
    const isAddFile = ref(false)
    const swal = inject('$swal')
    const filter = ref(null)
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('invoice')
    const paymentProviderId = ref()
    setTimeout(() => {
      paymentProviderId.value = parseInt(store.getters.getPaymentProviderId)
      getFiles(paymentProviderId.value, data => {
        files.value = data
        isloading.value = false
      })
    }, 1000)
    const fields = ref([
      { value: 'name', text: 'Nombre' },
      { value: 'type', text: 'Tipo de archivo' },
      { value: 'uploadDate', text: 'Fecha de carga' },
      { value: 'actions', text: 'Acciones' }
    ])
    const refreshTable = () => {
      isloading.value = true
      getFiles(paymentProviderId.value, data => {
        files.value = data
        isloading.value = false
      })
    }
    const onDownloadFile = file => {
      downloadFile(file.fileId, data => {
        const blob = new Blob([data])

        const url = window.URL.createObjectURL(blob)
        const link = document.createElement('a')
        link.href = url
        link.setAttribute('download', `${file.name}`)

        document.body.appendChild(link)
        link.click()

        document.body.removeChild(link)
        window.URL.revokeObjectURL(url)
      })
    }
    const onDeletePaymentMethod = paymentProviderId => {
      swal
        .fire({
          title: 'Estás a punto de eliminar un archivo, ¿Estas seguro?',
          text: '¡No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Si, eliminar archivo',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteFile(paymentProviderId, data => {
              swal.fire({
                title: 'Archivo eliminado!',
                text: 'El archivo ha sido eliminado satisfactoriamente .',
                icon: 'success'
              })
              refreshTable()
            })
          } else {
            isloading.value = false
          }
        })
    }
    const handleSuccess = () => {
      const formData = new FormData()
      uploadFiles.value.forEach(file => {
        formData.append('files', file.raw)
      })
      uploadFile(paymentProviderId.value, formData, data => {
        isAddFile.value = false
        refreshTable()
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
      uploadFiles,
      files,
      paymentProviderId,
      isAddFile,
      refreshTable,
      onDownloadFile,
      onDeletePaymentMethod,
      handleSuccess
    }
  }
}
</script>

<style></style>
