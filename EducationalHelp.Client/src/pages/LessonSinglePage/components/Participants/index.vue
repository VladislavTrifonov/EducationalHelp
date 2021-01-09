<template>
  <div>
    <b-table-simple>
      <b-thead>
        <b-th>№ (п/п)</b-th>
        <b-th>Псевдоним</b-th>
        <b-th>Действия</b-th>
      </b-thead>
      <b-tbody>
        <b-tr v-for="(participant, i) in participants" :key="i*777">
          <b-td>{{i + 1}}</b-td>
          <b-td>{{participant.pseudonym}}</b-td>
          <b-td><b-button variant="danger">Удалить</b-button></b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">
</style>
