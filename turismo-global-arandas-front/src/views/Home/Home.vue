<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <h1></h1>
</template>

<script>
import { inject } from 'vue'
export default {
  setup () {
    const swal = inject('$swal')
    swal.fire({
      title: '¡Nueva Tarifa registrado!',
      text: 'La tarifa se ha registrado correctamente',
      icon: 'success'
    })
    return {}
  }
}
</script>

<style></style>
