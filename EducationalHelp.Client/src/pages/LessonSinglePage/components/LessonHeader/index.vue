<template>
  <div class="row">
    <div class="col">
      <div class="card header-card">
        <div class="card-body">
          <div class="row justify-content-between">
            <div class="col col-md-auto">
              <div class="mb-3" v-if="!headEditing">
                <h5 class="card-title">
                  {{lesson.title}}
                  <span class="badge badge-secondary">
                    {{lesson.label}}
                  </span>
                </h5>
                <p class="card-text" v-html="lesson.description"></p>
              </div>
              <div class="mb-3" v-else>
                <b-input-group>
                  <b-input-group-prepend>
                    <b-input-group-text>
                        Название и метка
                    </b-input-group-text>
                  </b-input-group-prepend>
                  <b-form-input v-model="lesson.title"></b-form-input>
                  <b-form-input v-model="lesson.label"></b-form-input>
                </b-input-group>
                <div class="pt-2">
                  <ckeditor :editor="editor" class="ckeditor-size" v-model="lesson.description" :config="{language: 'ru'}"></ckeditor>
                </div>
              </div>
              <created-updated-info :entity="lesson"></created-updated-info>
            </div>
            <div class="col col-md-auto">
              <div v-if="!headEditing">
                <p class="mb-0">Начало занятия: {{lesson.dateStart}}</p>
                <p>Конец занятия: {{lesson.dateEnd}}</p>
              </div>
              <div class="col col-md-auto" v-else>
                <div class="mb-4">
                  <b-input-group-text class="mb-1">
                     Начало занятия
                  </b-input-group-text>
                  <b-input-group prepend="Дата" class="mb-1">
                    <b-form-datepicker v-model="startDate"></b-form-datepicker>
                  </b-input-group>
                  <b-input-group prepend="Время">
                    <b-form-timepicker v-model="startTime"></b-form-timepicker>
                  </b-input-group>
                </div>
                <div>
                  <b-input-group-text class="mb-1">
                    Конец занятия
                  </b-input-group-text>
                  <b-input-group prepend="Дата" class="mb-1">
                    <b-form-datepicker v-model="endDate"></b-form-datepicker>
                  </b-input-group>
                  <b-input-group prepend="Время">
                    <b-form-timepicker v-model="endTime"></b-form-timepicker>
                  </b-input-group>
                </div>
              </div>
              <div class="row justify-content-end">
                <b-button v-if="!headEditing" variant="outline-primary" @click="headEditing = !headEditing"><b-icon-pencil-square></b-icon-pencil-square> Редактировать шапку</b-button>
                <b-button v-else variant="outline-success btn-save" @click="headEditing = !headEditing"><b-icon-check></b-icon-check> Сохранить</b-button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" src="./index.ts">

</script>

<style scoped src="./index.css">

</style>