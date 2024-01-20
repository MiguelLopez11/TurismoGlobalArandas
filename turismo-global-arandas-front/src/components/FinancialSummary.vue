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

    <el-col :span="14" v-for="item in reservationsByEmployee" :key="item" class="mt-2">
      <el-card>
        <el-row>
          <el-col :span="8">
            <strong>Informes de Empleados</strong>
          </el-col>
          <el-col :span="8">
            <span>Empleado: {{item.employeeName}}</span>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <label>Total de reservaciones Hoteleria:  <strong>{{item.hotelReservationsCount}}</strong></label>
            <label>Total de reservaciones Vuelos:  <strong>{{item.flightReservationsCount}}</strong></label>
            <label>Total de reservaciones Vehiculos:  <strong>{{item.vehicleReservationsCount}}</strong></label>
            <label>Total de reservaciones Tours:  <strong>{{item.tourReservationsCount}}</strong></label>
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
    const { getTotalRevenue, getReservationsByMonth, getReservationsByEmployee } = HomeServices()
    const totalRevenue = ref()
    const reservationsByMonth = ref([])
    const reservationsByEmployee = ref([])

    getTotalRevenue(data => {
      totalRevenue.value = data
    })
    getReservationsByEmployee(data => {
      reservationsByEmployee.value = data
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
      reservationsByEmployee,
      chartSeries,
      chartOptions
    }
  }
}
</script>

<style></style>
