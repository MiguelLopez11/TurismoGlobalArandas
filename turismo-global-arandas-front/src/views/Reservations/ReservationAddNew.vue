<template>
  <el-card>
    <horizontal-stepper :steps="steps"> </horizontal-stepper>
  </el-card>
</template>

<script>
import { shallowRef } from 'vue'
import HorizontalStepper from 'vue-stepper'
import StepOne from './StepsWizzard/StepOne.vue'
export default {
  components: {
    HorizontalStepper
  },
  setup () {
    const steps = shallowRef([
      {
        icon: 'mail',
        name: 'first',
        title: 'Sample title 1',
        subtitle: 'Subtitle sample',
        component: StepOne,
        completed: false
      }
    ])
    return {
      steps
    }
  }
}
</script>

<style scoped>
#Icon-Step {
  font-size: 150%;
  justify-content: center;
}
</style>
