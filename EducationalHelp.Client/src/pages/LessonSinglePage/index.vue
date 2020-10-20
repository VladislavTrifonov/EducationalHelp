<template>
  <div class="container">
    <lesson-header :lesson="lesson"></lesson-header>

     <b-tabs content-class="pt-3" class="pt-3">
      <b-tab title="Содержание занятия" active>
        <contents :lesson="lesson"></contents>
      </b-tab>
      <b-tab title="Выполнение самостоятельных заданий">
        <homework></homework>
      </b-tab>
      <b-tab title="Оценивание (итоги)">
        <grading :lesson="lesson"></grading>
      </b-tab>
    </b-tabs>
    <hr>
    <div class="pt-3">
      <b class="interactive-element" @click="notesEditing = !notesEditing">Заметки: </b>
      <p class="lead" v-html="lesson.notes" v-if="!notesEditing"></p>
      <div v-else>
        <b-form-textarea placeholder="Введите свои заметки здесь..." rows="3" max-rows="10" v-model="lesson.notes"></b-form-textarea>
        <div class="row justify-content-center">
          <div class="col col-md-auto">
            <b-button variant="success" @click="notesEditing = !notesEditing">Сохранить</b-button>
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