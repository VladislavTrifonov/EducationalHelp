<template>
  <div>
    <ul class="lessons">
      <li class="lesson" v-for="lesson in lessons" :key="lesson.id">
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
              <span class="lesson__green" v-if="new Date(lesson.dateStart).getTime() > Date.now()">
                Начнется через {{getTimeOfWaiting(lesson.dateEnd)}}
              </span>
              <span class="lesson__orange" v-else-if="new Date(lesson.dateEnd).getTime() > Date.now()">
                Началось <b>{{new Date(lesson.dateStart).toTimeString()}}</b>,<br> закончится через <b>{{getTimeOfWaiting(lesson.dateEnd)}}</b>
              </span>
              <span class="lesson__gray" v-else>
                Завершено
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