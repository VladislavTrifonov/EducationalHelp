<template>
  <div class="clearfix">
    <span class="small lead"><i>Создано</i> <date-time :date-time-string="entity.createdAt"></date-time></span>,
    <span class="small lead"><i>Обновлено</i> <date-time :date-time-string="entity.updatedAt"></date-time></span>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>