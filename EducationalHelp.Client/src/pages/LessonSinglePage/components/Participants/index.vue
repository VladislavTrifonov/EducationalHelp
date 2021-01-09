<template>
  <div>
    <b-table-simple>
      <b-thead>
        <b-th>№ (п/п)</b-th>
        <b-th>Псевдоним</b-th>
        <b-th>Действия</b-th>
      </b-thead>
      <b-tbody>
        <b-tr>
          <b-td colspan="2"><b>ТЕКУЩИЕ</b> участники, которые уже прикреплены к занятию</b-td>
        </b-tr>
        <b-tr>
          <br>
        </b-tr>
        <b-tr v-if="participants.length == 0">
          <b-td colspan="3">
            <i>Нет данных для отображения.</i>
          </b-td>
        </b-tr>
        <b-tr v-for="(participant, i) in participants" :key="(i+1)*777" v-else>
          <b-td>{{i + 1}}</b-td>
          <b-td>{{participant.pseudonym}}</b-td>
          <b-td><b-button variant="danger" @click="deleteParticipant(i)"><b-icon-trash></b-icon-trash></b-button></b-td>
        </b-tr>
        <b-tr>
          <b-td colspan="2"><b>ВОЗМОЖНЫЕ</b> участники, состоящие в группе и пока не прикрепленные к занятию</b-td>
        </b-tr>
        <b-tr>
          <br>
        </b-tr>
        <b-tr v-if="possibleParticipants.length == 0">
          <b-td colspan="3">
            <i>Нет данных для отображения.</i>
          </b-td>
        </b-tr>
        <b-tr v-for="(participant, i) in possibleParticipants" :key="(i+1)*778" v-else>
          <b-td>{{i + 1}}</b-td>
          <b-td>{{participant.pseudonym}}</b-td>
          <b-td><b-button variant="success" @click="addParticipant(i)"><b-icon-plus></b-icon-plus></b-button></b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">
</style>
