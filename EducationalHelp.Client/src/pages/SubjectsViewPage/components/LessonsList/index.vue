<template>
  <div class="container">
    <div class="row justify-content-end mb-3">
      <div class="col col-md-auto">
        <router-link :to="{name: 'lessonCreate', params: {subjectId: lessons[0].subjectId} }">
          <b-button variant="success"><b-icon-plus-circle></b-icon-plus-circle> Добавить</b-button>
        </router-link>
      </div>
    </div>
    <ul class="lessons">
      <li class="lesson" v-for="lesson in lessons" :key="lesson.id">
        <div class="lesson__block">
          <div class="lesson__left">
            <router-link  :to="{name: 'lessonView', params: { subjectId: lesson.subjectId, lessonId: lesson.id }}" class="lesson__header">
              <h4 class="lesson__header-h4">
                {{lesson.title}}
              </h4>
            </router-link>
            <p class="lesson__description" v-html="lesson.description"></p>
          </div>
          <div class="lesson__right">
              <span class="lesson__green" v-if="new Date(lesson.dateStart).getTime() > Date.now()">
                Начнется через {{getTimeOfWaiting(lesson.dateEnd)}}
              </span>
              <span class="lesson__orange" v-else-if="new Date(lesson.dateEnd).getTime() > Date.now()">
                Началось <b>{{new Date(lesson.dateStart).toTimeString()}}</b>,<br> закончится через <b>{{getTimeOfWaiting(lesson.dateEnd)}}</b>
              </span>
              <span class="lesson__gray" v-else>
                Завершено {{lesson.dateEnd}}
              </span>
          </div>
        </div>
        <hr>
      </li>
    </ul>
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>