<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onUpdateRoute">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="routeName" :rules="validateRouteName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre de la ruta </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del concepto"
                size="large"
                v-model="route.routeName"
              />
            </el-form-item>
          </Field>
        </el-col>
        <el-col :span="8">
            <el-form-item>
              <div>
                <label> Descripción </label>
              </div>
              <el-input
                placeholder="Ingresa una descripción del concepto"
                size="large"
                v-model="route.description"
              />
            </el-form-item>
        </el-col>
      </el-row>
      <el-divider />
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
            @click="
              () => {
                $router.push('/Rutas')
              }
            "
            >Cancelar</el-button
          >
        </el-col>
      </el-row>
    </Form>
  </el-card>
</template>

<script>
import RouteServices from '@/Services/Routes.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getRoute, updateRoute } = RouteServices()
    const route = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getRoute(router.params.RouteId, data => {
      route.value = data
    })
    const onUpdateRoute = () => {
      updateRoute(route.value, data => {
        swal
          .fire({
            title: 'Ruta modificada correctamente',
            text: 'La ruta se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/Rutas')
            }
          })
      })
    }
    const validateRouteName = () => {
      if (!route.value.routeName) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      route,
      onUpdateRoute,
      validateRouteName
    }
  }
}
</script>

<style></style>
