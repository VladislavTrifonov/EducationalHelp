<template>
  <div>
    <b-table-simple hover small responsive>
      <b-thead head-variant="light">
        <b-tr>
          <b-th>
            Параметры
          </b-th>
          <b-th>

          </b-th>
        </b-tr>
      </b-thead>

      <b-tbody>
        <b-tr>
          <b-th>
            Отметка о посещении
          </b-th>
          <b-td>
            <b-form-checkbox v-model="lesson.isVisited" switch>
              <span v-if="lesson.isVisited">Да</span>
              <span v-else>Нет</span>
            </b-form-checkbox>
          </b-td>
        </b-tr>
        <b-tr>
          <b-th>
            Отметка о успеваемости (самостоят.)
          </b-th>
          <b-td>
            <b-form-row>
              <span class="interactive-element" @click="setMark" v-if="!markEditing">{{ markName }}</span>
              <b-col md="6" v-else>
                <b-input-group>
                  <b-form-select :options="marks" v-model="lesson.selfMark"></b-form-select>
                  <b-input-group-append>
                    <b-button variant="success" @click="setMark">Ок</b-button>
                  </b-input-group-append>
                </b-input-group>
              </b-col>
            </b-form-row>
          </b-td>
        </b-tr>
      </b-tbody>

    </b-table-simple>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">
</style>