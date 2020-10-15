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
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>