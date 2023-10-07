<template>
  <el-card>
    <Form v-slot="{ errors }" @submit="onupdateTypeReservationGrupal">
      <el-row :gutter="35">
        <el-col :span="8">
          <Field name="name" :rules="validateName" as="text">
            <el-form-item :error="errors.name" required>
              <div>
                <label> Nombre </label>
              </div>
              <el-input
                placeholder="Ingresa el nombre del destino"
                size="large"
                v-model="TypeReservationGroup.name"
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
              placeholder="Ingresa una descripción del destino"
              size="large"
              v-model="TypeReservationGroup.description"
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
                $router.push('/TiposReservaciones')
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
import TypeReservationGrupalservices from '@/Services/TypeReservationGroup.Services'
import { useRoute, useRouter } from 'vue-router'
import { ref, inject } from 'vue'

export default {
  setup () {
    const { getTypeReservationGrupal, updateTypeReservationGrupal } =
      TypeReservationGrupalservices()
    const TypeReservationGroup = ref({})
    const router = useRoute()
    const redirect = useRouter()
    const swal = inject('$swal')
    getTypeReservationGrupal(router.params.TypeReservationGrupalId, data => {
      TypeReservationGroup.value = data
    })
    const onupdateTypeReservationGrupal = () => {
      updateTypeReservationGrupal(TypeReservationGroup.value, data => {
        swal
          .fire({
            title: 'Tipo de reservación grupal modificado correctamente',
            text: 'El Tipo de reservación grupal se ha modificado satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/TiposReservacionesGrupales')
            }
          })
      })
    }
    const validateName = () => {
      if (!TypeReservationGroup.value.name) {
        return 'Este campo es requerido'
      }
      return true
    }
    return {
      TypeReservationGroup,
      onupdateTypeReservationGrupal,
      validateName
    }
  }
}
</script>

<style></style>
