<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <el-row :gutter="10" justify="center">
    <el-col :span="12">
      <el-card>
        <el-row justify="center" align="middle">
          <el-col :span="6" :push="2">
            <strong>Informes de reservas</strong>
          </el-col>
        </el-row>
        <el-row justify="center" align="middle">
          <el-col :span="12">
            <apexchart
              type="bar"
              :options="chartOptions"
              :series="chartSeries"
            />
          </el-col>
        </el-row>
      </el-card>
    </el-col>
    <el-col :span="6">
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
            <h1>{{ employeeMostReservations.totalReservations }}</h1>
            <label> reservaciones</label>
          </el-col>
          <el-col :span="12">
            <strong>
              {{ employeeMostReservations.employeeName }}
              {{ employeeMostReservations.employeeLastName }}</strong
            >
          </el-col>
        </el-row>
      </el-card>
    </el-col>
    <el-col :span="6">
      <el-card>
        <el-row>
          <el-col :span="24">
            <strong>Informes de reservaciones hoteleria</strong>
          </el-col>
          <el-col :span="24">
            <span>Destino mas reservado del año:</span>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <h1>
              {{ mostPopularDestination.reservationsCount }}
            </h1>
            <label> reservaciones</label>
          </el-col>
          <el-col :span="12">
            <h3>
              <strong>{{ mostPopularDestination.destination }}</strong>
            </h3>
          </el-col>
        </el-row>
      </el-card>
    </el-col>
  </el-row>
  <el-row class="mt-2">
    <el-col :span="24">
      <el-card>
        <el-row justify="center"  align="middle">
          <el-col :span="24">
            <strong>Informes de reservas</strong>
          </el-col>
          <el-col :span="24">
            <span>Resumen semanal de reservaciones</span>
          </el-col>
        </el-row>
        <el-row justify="center" align="middle">
          <el-col :span="8" style="justify: center">
            <h1>{{ reservationCount }}</h1>
            <h5>reservaciones totales de este año</h5>
          </el-col>
          <el-col :span="10">
            <apexchart
              ref="chartInstance"
              type="pie"
              :options="pieChartOptions"
              :series="pieChartSeries"
            />
          </el-col>
        </el-row>
      </el-card>
    </el-col>
    <el-col :span="24" class="mt-2">
      <el-card>
        <el-row justify="center">
          <el-col :span="24">
            <strong>Informes de reservas</strong>
          </el-col>
          <el-col :span="24">
            <span>Resumen semanal de reservaciones</span>
          </el-col>
        </el-row>
        <el-row justify="center">
          <el-col :span="8" style="justify: center">
            <h1>{{ reservationCount }}</h1>
            <h5>reservaciones totales de este año</h5>
          </el-col>
          <el-col :span="12">
            <apexchart
              ref="reservationschartInstance"
              type="pie"
              :options="reservationCountChartOptions"
              :series="reservationCountChartSeries"
            />
          </el-col>
        </el-row>
      </el-card>
    </el-col>
  </el-row>
</template>
<script>
import { ref, onMounted, nextTick } from 'vue'
import HomeServices from '@/Services/Home.Services'

export default {
  setup () {
    const reservations = ref([])
    const reservationCount = ref(0)
    const employeeMostReservations = ref([])
    const mostPopularDestination = ref([])
    const reservationsByEmployee = ref([])
    const reservationsCount = ref([])
    const chartInstance = ref(null)
    const reservationschartInstance = ref(null)
    const {
      getReservationsByMonth,
      EmployeeWithMostReservations,
      MostPopularDestination,
      getReservationsByEmployee,
      getReservationsCount
    } = HomeServices()

    // Definir variables antes de su uso
    const chartOptions = ref({
      chart: {
        type: 'bar'
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

    const chartSeries = ref([])
    const pieChartOptions = ref({
      chart: {
        type: 'pie'
      },
      labels: [] // Sustituye con tus etiquetas
    })

    const pieChartSeries = ref([])
    const reservationCountChartOptions = ref({
      chart: {
        type: 'pie'
      },
      labels: [] // Sustituye con tus etiquetas
    })

    const reservationCountChartSeries = ref([])

    onMounted(() => {
      getReservationsByMonth(data => {
        reservations.value = data
        reservations.value.forEach(item => {
          reservationCount.value += item
        })
        chartSeries.value = [
          {
            name: 'Reservaciones',
            data: reservations.value
          }
        ]
      })
      EmployeeWithMostReservations(data => {
        employeeMostReservations.value = data
      })
      MostPopularDestination(data => {
        mostPopularDestination.value = data
      })

      // Mover estas asignaciones dentro del bloque onMounted
      getReservationsByEmployee(data => {
        reservationsByEmployee.value = data
        const pieChartData = data.map(employee => ({
          name: employee.employeeName,
          data: employee.totalReservationsCount
        }))
        getReservationsCount(resp => {
          reservationsCount.value = resp
          const reservationCountChartData = resp.map(reservation => ({
            name: reservation.tipo,
            data: reservation.cantidad
          }))

          // Asignar datos al gráfico de pie
          pieChartOptions.value.labels = pieChartData.map(item => item.name)
          pieChartSeries.value = pieChartData.map(item => item.data)
          reservationCountChartOptions.value.labels = reservationCountChartData.map(
            item => item.name
          )
          reservationCountChartSeries.value = reservationCountChartData.map(
            item => item.data
          )
          nextTick(() => {
            // Tu instancia de ApexCharts debería estar en una variable, por ejemplo chartInstance
            // Asegúrate de tener una referencia a la instancia de ApexCharts
            // chartInstance.updateOptions es un método de ApexCharts para actualizar las opciones
            chartInstance.value.updateOptions({
              labels: pieChartOptions.value.labels
            })
            reservationschartInstance.value.updateOptions({
              labels: reservationCountChartOptions.value.labels
            })
          })
        })
      })
    })

    return {
      reservationCount,
      chartOptions,
      chartSeries,
      employeeMostReservations,
      mostPopularDestination,
      pieChartOptions,
      pieChartSeries,
      chartInstance,
      reservationschartInstance,
      reservationCountChartSeries,
      reservationCountChartOptions
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
