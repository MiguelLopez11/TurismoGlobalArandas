<template>
  <el-row :gutter="15" align="middle">
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
    <el-col
      :span="12"
      v-for="employee in reservationsByEmployee"
      :key="employee.employeeId"
    >
      <el-card>
        <el-row>
          <el-col :span="8">
            <strong>Informes de Empleados</strong>
          </el-col>
          <el-col :span="8">
            <span>Empleado: {{ employee.employeeName }}</span>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <apexchart
              ref="chartInstance"
              type="pie"
              :options="getPieChartOptions(employee)"
              :series="getPieChartSeries(employee)"
            />
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
    const {
      getTotalRevenue,
      getReservationsByMonth,
      getReservationsByEmployee
    } = HomeServices()

    const totalRevenue = ref()
    const reservationsByMonth = ref([])
    const reservationsByEmployee = ref([])
    const pieChartOptions = ref({
      chart: {
        type: 'pie'
      },
      labels: []
    })

    const pieChartSeries = ref([25, 30, 45])
    const chartSeries = ref([])
    const chartInstance = ref(null)
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
          colors: ['#f3f3f3', 'transparent'],
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

    const getPieChartOptions = employee => {
      return {
        chart: {
          type: 'pie'
        },
        labels: ['Hotel', 'Vuelo', 'Vehículo', 'Tour']
      }
    }

    const getPieChartSeries = employee => {
      return [
        employee.hotelReservationsCount,
        employee.flightReservationsCount,
        employee.vehicleReservationsCount,
        employee.tourReservationsCount
      ]
    }

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
      getTotalRevenue(data => {
        totalRevenue.value = data
      })
      getReservationsByEmployee(data => {
        reservationsByEmployee.value = data
        const pieChartData = data.map(employee => ({
          name: employee.employeeName,
          data: employee.totalReservationsCount
        }))
        pieChartOptions.value.labels = pieChartData.map(item => item.name)
        pieChartSeries.value = pieChartData.map(item => item.data)
      })
    })

    return {
      totalRevenue,
      reservationsByEmployee,
      chartSeries,
      chartOptions,
      pieChartOptions,
      chartInstance,
      pieChartSeries,
      getPieChartOptions,
      getPieChartSeries
    }
  }
}
</script>

<style></style>
