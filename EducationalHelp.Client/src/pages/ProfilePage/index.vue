<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-3">
        <b-card>
          <div v-if="user.avatarLink == null">
            <b-card-title class="text-center">
              Аватар
            </b-card-title>
          </div>
          <div v-else>
            <img :src="urlOfImage" class="img-fluid" />
          </div>
          <b-card-sub-title class="text-center pt-2" style="cursor: pointer">
            Загрузить
          </b-card-sub-title>
        </b-card>
      </div>
      <div class="col-md-9">

        <div v-if="!isEditPseudonym">
          <p class="lead text-capitalize interactive-element" @click="editPseudonym()">{{user.pseudonym}}</p>
        </div>
        <div class="row" v-else>
          <div class="col-md-8">
            <b-input-group>
              <b-form-input v-model="newPseudonym"></b-form-input>
            </b-input-group>
          </div>
          <div class="col-md-2">
            <b-button variant="success" @click="onClickSavePseudonym">Сохранить</b-button>
          </div>
        </div>

        <p v-if="user.createdAt != null" class="lead">Присоединился: <date-time :date-time-string="user.createdAt"></date-time> </p>
      </div>
    </div>

    <h2 class="text-capitalize">Статистика</h2>
    <!-- TODO: Вернуть после реализации блока "социальных" функций приложения
    <b-form-checkbox inline>Я не хочу, чтобы мою статистику видел кто-то, кроме меня</b-form-checkbox>
    !-->
    <b-table-simple>
     <b-tbody>
       <b-tr>
         <b-td>
           Предметов
         </b-td>
         <b-td>
           23
         </b-td>
       </b-tr>
       <b-tr>
         <b-td>
           Средняя оценка по занятиям (общее)
         </b-td>
         <b-td>
           3.56
         </b-td>
       </b-tr>
     </b-tbody>
    </b-table-simple>

    <div>
      <b-icon-chevron-right></b-icon-chevron-right>
      <b-button variant="link" v-b-toggle.collapse-subjects><h4>Предметы</h4></b-button>
    </div>
    <b-collapse id="collapse-subjects">
      <b-table-simple>
        <b-thead>
          <b-th>№ (п/п)</b-th>
          <b-th>Предмет</b-th>
          <b-th>Занятий</b-th>
          <b-th>Из них пропущено (%)</b-th>
          <b-th>Средняя оценка (по занятиям)</b-th>
        </b-thead>
        <b-tbody>
          <b-tr>
            <b-td>1</b-td>
            <b-td>Алг. и АЯ</b-td>
            <b-td>1</b-td>
            <b-td>0 (0%)</b-td>
            <b-td>5</b-td>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </b-collapse>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">
</style>
