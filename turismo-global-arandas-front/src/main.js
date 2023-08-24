/* eslint-disable vue/multi-word-component-names */
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import ElementPlus from 'element-plus'
import VueSweetalert2 from 'vue-sweetalert2'
import { defineRule, Field, ErrorMessage, Form } from 'vee-validate'
import Vue3EasyDataTable from 'vue3-easy-data-table'
import { required } from '@vee-validate/rules'
import vSelect from 'vue-select'
import Vue3FormWizard from 'vue3-form-wizard'
import store from './Store'

import 'vue3-form-wizard/dist/style.css'
import 'vue3-easy-data-table/dist/style.css'
import './registerServiceWorker'
import 'element-plus/dist/index.css'
import 'sweetalert2/dist/sweetalert2.min.css'
const app = createApp(App)
const options = {
  confirmButtonColor: '#6366F1',
  cancelButtonColor: '#ff7674'
}

// Definir regla global 'required'
defineRule('required', required)
defineRule('userPassword', value => {
  if (!value || !value.length) {
    return 'Este campo es requerido '
  }
  if (
    !/^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/.test(
      value
    )
  ) {
    return 'La contraseña debe de contener minimo 8 Caracteres, minusculas y mayusculas y mínimo un simbolo '
  }
  return true
})

app.component('Field', Field)
app.component('ErrorMessage', ErrorMessage)
app.component('Form', Form)
app.component('EasyDataTable', Vue3EasyDataTable)
app.component('v-select', vSelect)
app.use(VueSweetalert2, options)
app.use(Vue3FormWizard)
app.use(store)
app.use(router)
app.use(ElementPlus)

app.mount('#app')
