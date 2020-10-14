<template>
    <div class="col-12">
        <div class="row">
            <h3>
                {{Model.name}}
            </h3>
            <b-button-group>
                <b-button size="sm" variant="light" @click="onEditClick()"><b-icon-pencil></b-icon-pencil> Редактировать</b-button>
                <b-button size="sm" variant="light" @click="onDeleteClick()"><b-icon-trash></b-icon-trash> Удалить</b-button>
            </b-button-group>
        </div>
        <div class="container">
                <span class="small lead"><i>Создано</i> {{Model.createdAt}}</span>, 
                <span class="small lead"><i>Обновлено</i> {{Model.updatedAt}}</span>
        </div>
        <b-tabs content-class="mt-3">
            <b-tab title="Информация" active>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            Преподаватель
                        </div>
                        <div class="col">
                            {{Model.teacher}}
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            Описание
                        </div>
                        <div class="col">
                            {{Model.description}}
                        </div>
                    </div>
                </div>
            </b-tab>
            <b-tab title="Учебная литература">

            </b-tab>
            <b-tab title="Занятия">
              <ul class="lessons">
                <li class="lesson" v-for="lesson in Model.lessons" :key="lesson.id">
                  <div class="lesson__block">
                    <div class="lesson__left">
                      <a href="#!" class="lesson__header">
                        <h4 class="lesson__header-h4">
                          {{lesson.title}}
                        </h4>
                      </a>
                      <p class="lesson__description">{{lesson.description}}</p>
                    </div>
                    <div class="lesson__right">
                      <span class="lesson__gray">
                        Начало: {{lesson.dateStart}}
                      </span>
                      <br>
                      <span class="lesson__gray">
                        Конец: {{lesson.dateEnd}}
                      </span>
                    </div>
                  </div>
                  <hr>
                </li>
              </ul>
            </b-tab>

        </b-tabs>
        <b-modal id="editSubjectModal" @ok="onEditModalOk">
            <template v-slot:modal-header="{ close }">
                <b-button size="sm" variant="outline-primary" @click="close()">
                    Закрыть
                </b-button>
                <h5>Редактирование</h5>
            </template>
            <template v-slot:default>
                <add-subject :model="Model" :validationStates="[]"></add-subject>
            </template>
            <template v-slot:modal-footer="{ ok, cancel, hide }">
                <b-button variant="primary" @click="ok()">Изменить</b-button>
                <b-button variant="outline-primary" @click="cancel()">Отмена</b-button>
            </template>
        </b-modal>
    </div>
</template>

<script lang="ts" src="./index.ts">

</script>

<style scoped src="./index.css">

</style>