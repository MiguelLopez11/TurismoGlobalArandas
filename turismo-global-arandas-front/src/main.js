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
app.component('Field', Field)
app.component('ErrorMessage', ErrorMessage)
app.component('Form', Form)
app.component('EasyDataTable', Vue3EasyDataTable)
app.component('v-select', vSelect)
app.use(VueSweetalert2, options)
app.use(router)
app.use(ElementPlus)

app.mount('#app')
