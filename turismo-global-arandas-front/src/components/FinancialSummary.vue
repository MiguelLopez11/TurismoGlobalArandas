<template>
  <el-row :gutter="25">
    <el-col :span="12">
      <el-card>
        <el-row>
          <el-col :span="8">
            <strong>Informes de ingresos y egresos</strong>
          </el-col>
          <el-col :span="8">
            <span>Su balance es:</span>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <h1>
              {{ totalRevenue }}
            </h1>
            <label> Pesos MXN</label>
          </el-col>
        </el-row>
      </el-card>
    </el-col>
    <el-col :span="12">
      <el-card>
        <el-row justify="center" align="middle">
          <el-col :span="6" :push="2">
            <strong>Informes de reservas</strong>
          </el-col>
        </el-row>
        <el-row justify="center" align="middle">
          <el-col :span="24">
            <apexchart
              type="line"
              height="200"
              :options="chartOptions"
              :series="chartSeries"
            ></apexchart>
          </el-col>
        </el-row>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import HomeServices from '@/Services/Home.Services'
import { ref, onMounted } from 'vue'
export default {
  setup () {
    const { getTotalRevenue, getReservationsByMonth } = HomeServices()
    const totalRevenue = ref()
    const reservationsByMonth = ref([])

    getTotalRevenue(data => {
      totalRevenue.value = data
    })
    const chartSeries = ref([])

    const chartOptions = ref({
      chart: {
        height: 300,
        type: 'line',
        zoom: {
          enabled: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: 'straight'
      },
      title: {
        text: 'Reservaciones por mes',
        align: 'left'
      },
      grid: {
        row: {
          colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
          opacity: 0.5
        }
      },
      xaxis: {
        categories: [
          'Ene',
          'Feb',
          'Mar',
          'Abr',
          'May',
          'Jun',
          'Jul',
          'Ago',
          'Sep',
          'Oct',
          'Nov',
          'Dic'
        ]
      }
    })
    onMounted(() => {
      getReservationsByMonth(data => {
        reservationsByMonth.value = data
        chartSeries.value = [
          {
            name: 'Reservaciones',
            data: reservationsByMonth.value
          }
        ]
      })
    })
    return {
      totalRevenue,
      chartSeries,
      chartOptions
    }
  }
}
</script>

<style></style>
