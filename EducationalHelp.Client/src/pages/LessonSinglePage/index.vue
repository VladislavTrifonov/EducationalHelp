<template>
  <div class="container" v-if="isLessonLoaded">
    <breadcrumbs :breadcrumbs="breadcrumbs" v-if="!isCreationMode"></breadcrumbs>
    <a @click="$router.go(-1)" href="#!"><b-icon-chevron-left></b-icon-chevron-left> Вернуться назад</a>
    <div class="row justify-content-end mb-2" v-if="!isCreationMode">
      <div class="col col-md-auto">
        <b-button variant="outline-danger" @click="deleteLesson"><b-icon-trash-fill></b-icon-trash-fill> Удалить</b-button>
      </div>
    </div>
    <lesson-header :lesson="lesson" :editing="isCreationMode"></lesson-header>

     <b-tabs content-class="pt-3" class="pt-3">
      <b-tab title="Содержание занятия" active>
        <contents :lesson="lesson" :editing="isCreationMode"></contents>
      </b-tab>
      <b-tab title="Выполнение самостоятельных заданий" v-if="!isCreationMode">
        <homework :lesson="lesson" :lesson_files="files" v-on:reload-files-needed="loadFiles"></homework>
      </b-tab>
      <b-tab title="Оценивание (итоги)">
        <grading :lesson="lesson" :editing="isCreationMode"></grading>
      </b-tab>
    </b-tabs>
    <hr>
    <div class="pt-3">
      <b v-bind:class="{'interactive-element': !isCreationMode}" @click="notesEditing = !notesEditing">Заметки: </b>
      <p class="lead" v-html="lesson.notes" v-if="!notesEditing"></p>
      <div v-else>
        <b-form-textarea placeholder="Введите свои заметки здесь..." rows="3" max-rows="10" v-model="lesson.notes"></b-form-textarea>
        <div class="row justify-content-center pt-3">
          <div class="col col-md-auto">
            <b-button variant="success" @click="notesEditing = !notesEditing" v-if="!isCreationMode">Сохранить</b-button>
            <b-button variant="success" @click="createLesson" v-else>Создать</b-button>
          </div>
        </div>
      </div>
    </div>
    <transition name="fade">
      <b-toast toaster="b-toaster-bottom-center" no-auto-hide no-close-button v-bind:visible="isSaveBtnShow">
        <div class="container">
          <div class="text-sm-center text-secondary">
            У вас есть несохраненные изменения, которые могут быть утеряны.
            <div class="row justify-content-center pt-1">
              <div class="col col-md-auto">
                <b-button variant="success" @click="saveBtnClick">Сохранить</b-button>
              </div>
            </div>
          </div>
        </div>
      </b-toast>
    </transition>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>
