<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <el-row :gutter="16" justify="center">
    <el-col :span="16">
      <el-card>
        <el-row justify="center">
          <el-col :span="6" :push="2">
            <strong>Informes de reservas</strong>
          </el-col>
          <el-col :span="24" :push="9">
            <span>Resumen semanal de reservaciones</span>
          </el-col>
        </el-row>
        <el-row justify="center">
          <el-col :span="8" style="justify: center">
            <h1>9999</h1>
            <label> reservaciones totales de esta semana</label>
          </el-col>
          <el-col :span="12">
            <Bar id="my-chart-id" :options="chartOptions" :data="chartData" />
          </el-col>
        </el-row>
      </el-card>
    </el-col>
    <el-col :span="8">
      <el-card>
        <el-row>
          <el-col :span="24">
            <strong>Informes de empleado</strong>
          </el-col>
          <el-col :span="24">
            <span>Empleado con mayores reservas:</span>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <h1>9999</h1>
            <label> reservaciones</label>
          </el-col>
          <el-col :span="12">
            <strong> Miguel López Ortega</strong>
          </el-col>
        </el-row>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'
import HomeServices from '@/Services/Home.Services'
import { ref } from 'vue'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
  components: {
    Bar
  },
  setup () {
    const cantidad = []
    const { getReservationsByMonth } = HomeServices()
    getReservationsByMonth(data => {
    })
    const chartData = ref({
      labels: [
        'Ene',
        'Feb',
        'Mar',
        'Abr',
        'May',
        'Jun',
        'Jul',
        'Agos',
        'Sep',
        'Oct',
        'Nov',
        'Dic'
      ],
      datasets: [
        {
          label: 'Reservasiones',
          backgroundColor: '#887EF2',
          data: cantidad.value
        }
      ]
    })

    const chartOptions = ref({
      responsive: true
    })
    return {
      chartData,
      chartOptions
    }
  }
}
</script>

<style scoped>
.card-header {
  justify-content: center;
}
.box-card {
  justify-content: center;
  display: flex !important;
  flex-wrap: wrap;
}
strong {
  color: #656174;
}
span {
  color: #b7b5be;
}
h1 {
  color: #5d596c;
}
label {
  color: #39393a;
}
</style>
