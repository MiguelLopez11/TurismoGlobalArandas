/* eslint-disable vue/multi-word-component-names */
import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import ElementPlus from 'element-plus'
import VueSweetalert2 from 'vue-sweetalert2'
import { defineRule, Field, ErrorMessage, Form } from 'vee-validate'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faUserSecret, faUser } from '@fortawesome/free-solid-svg-icons'
import 'element-plus/dist/index.css'
import 'sweetalert2/dist/sweetalert2.min.css'
const app = createApp(App)
const options = {
  confirmButtonColor: '#6366F1',
  cancelButtonColor: '#ff7674'
}
defineRule('required', value => {
  if (!value || !value.length) {
    return 'Este campo es requerido'
  }
  return true
})

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component)
}
library.add(faUserSecret, faUser)

app.component('font-awesome-icon', FontAwesomeIcon)
app.component('Field', Field)
app.component('ErrorMessage', ErrorMessage)
app.component('Form', Form)

app.use(VueSweetalert2, options)
app.use(router)
app.use(ElementPlus)

app.mount('#app')
