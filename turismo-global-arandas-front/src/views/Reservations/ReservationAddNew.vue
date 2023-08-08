<template>
  <el-card>
    <el-steps :space="200" :active="1" simple>
      <el-step title="Datos generales" >
       <h1>
        holi
       </h1>
        <template #icon>
            <i id="Icon-Step" class="bi bi-file-earmark-person"></i>
        </template>
      </el-step>
      <el-step title="Step 2"/>
      <el-step title="Step 3"/>
    </el-steps>
  </el-card>
</template>

<script>
export default {}
</script>

<style scoped>
#Icon-Step{
  font-size: 150%;
  justify-content: center;
}
</style>
