<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <div class="card">
          <div class="card-body">
            <div class="row justify-content-between">
              <div class="col col-md-auto">
                <h5 class="card-title interactive-element">
                  {{lesson.title}}
                  <span class="badge badge-secondary">
                  {{lesson.label}}
                </span>
                </h5>
                <p class="card-text interactive-element">{{lesson.description}}</p>
                <created-updated-info :entity="lesson"></created-updated-info>
              </div>
              <div class="col col-md-auto pt-4">
                <p class="mb-0">Начало занятия: <span class="interactive-element">{{lesson.dateStart}}</span></p>
                <p>Конец занятия: <span class="interactive-element">{{lesson.dateEnd}}</span></p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <b-tabs content-class="pt-3" class="pt-3">
      <b-tab title="Содержание занятия" active>

      </b-tab>
      <b-tab title="Выполнение самостоятельных заданий">

      </b-tab>
      <b-tab title="Оценивание (итоги)">
        <grading :lesson="lesson"></grading>
      </b-tab>
    </b-tabs>
    <hr>
    <div class="pt-3">
      <b>Заметки: </b>
      <p class="lead">Тут будут заметки...</p>
    </div>

  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>