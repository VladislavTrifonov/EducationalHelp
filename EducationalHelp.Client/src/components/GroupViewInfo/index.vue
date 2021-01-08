<template>
  <div>
    <b-alert show variant="info">
      Сейчас Вы просматриваете часть контента, связанную с группой <b>"{{currentGroup.title}}"</b>. <br>
      <b-form-group id="input-group-1" label="Сменить группу просмотра" label-for="select-1">
        <b-form-select v-model="selectedGroup" :options="groupOptions" id="select-1"></b-form-select>
      </b-form-group>
    </b-alert>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">
</style>
