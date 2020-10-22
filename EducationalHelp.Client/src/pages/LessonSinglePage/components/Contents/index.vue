<template>
  <div>
    <div v-if="!editMark">
      <div class="mb-5" v-html="lesson.homework">
      </div>
      <div class="row">
        <div class="col">
          <b-button variant="outline-secondary" @click="setHomework"><b-icon-pencil></b-icon-pencil> Редактировать</b-button>
        </div>
      </div>
    </div>
    <div v-else>
      <ckeditor :editor="editor" v-model="lesson.homework" :config="{language: 'ru'}"></ckeditor>
      <b-button variant="success" class="mt-3" @click="setHomework" v-if="!editing">Сохранить</b-button>
    </div>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style src="./index.css">

</style>