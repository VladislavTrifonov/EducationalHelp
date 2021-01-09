<template>
  <div>
    <b-table-simple>
      <b-thead>
        <b-th>№ (п/п)</b-th>
        <b-th>Пользователь</b-th>
        <b-th>Отметка о посещении</b-th>
        <b-th>Оценка</b-th>
      </b-thead>
      <b-tbody>
        <b-tr v-for="(participant, i) in gradingParticipants" :key="i*666">
          <b-td>{{i + 1}}</b-td>
          <b-td>{{participants[i].pseudonym}}</b-td>
          <b-td>
            <b-form-checkbox v-model="participant.isVisited" switch>
            <span v-if="participant.isVisited">Был</span>
            <span v-else>Не был</span>
            </b-form-checkbox>
          </b-td>
          <b-td>
            <b-col md="6" class="p-0">
              <b-form-select :options="marks" v-model="participant.mark"></b-form-select>
            </b-col>
          </b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
    <div class="row justify-content-center">
      <div class="col col-md-auto">
        <b-button variant="success" @click="saveButton">Сохранить</b-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">
</style>
