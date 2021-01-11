<template>
  <div class="container-fluid">
    <b-tabs>
      <b-tab title="Ваши группы">
        <b-table-simple>
          <b-thead>
            <b-th>№ (п/п)</b-th>
            <b-th>Название</b-th>
            <b-th>Описание</b-th>
            <b-th>Участников</b-th>
            <b-th>Действия</b-th>
          </b-thead>
          <b-tbody>
            <b-tr v-for="(group, idx) in userGroups" :key="(idx + 1)*785">
              <b-td>{{idx + 1}}</b-td>
              <b-td>{{group.title}}</b-td>
              <b-td>{{group.description}}</b-td>
              <b-td>{{group.membersCount}}</b-td>
              <b-td><b-button variant="danger" @click="leave(idx)">Выйти</b-button></b-td>
            </b-tr>
          </b-tbody>
        </b-table-simple>
      </b-tab>
      <b-tab title="Общий список групп">

      </b-tab>
    </b-tabs>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>
