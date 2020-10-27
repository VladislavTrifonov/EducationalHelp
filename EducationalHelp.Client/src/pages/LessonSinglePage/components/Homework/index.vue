<template>
  <div>

    <!-- Загруженные файлы !-->
    <div class="mb-4">
      <h4>Раннее прикрепленные файлы</h4>
      <b-list-group>
        <b-list-group-item v-for="(file, idx) in lesson_files" :key="file.id">
          <div class="row justify-content-between">
            <div class="col col-md-auto">
              {{file.originalName}} ({{getFileSize(file.length)}}) / <a :href="file.linkToDownload" target="_blank">Скачать</a> / <a href="#!">Удалить</a>
            </div>
            <div class="col col-md-auto">
              <span class="text-secondary">Загружен  <date-time :date-time-string="file.createdAt"></date-time></span>
            </div>
          </div>

        </b-list-group-item>
      </b-list-group>
    </div>
    <!-- Конец Загруженные файлы !-->

    <!-- Загрузка файлов !-->
    <b-form>
      <div class="mb-1">
        <h3>Загрузка файлов</h3>
        <b-form-file placeholder="Перетащите файлы выполненного Д/З сюда, или выберите их..."
                     drop-placeholder="Отпустите мышку здесь..."
                     multiple
                     size="lg"
                     v-model="files"
                     :file-name-formatter="formatNames"
        ></b-form-file>
      </div>
      <transition name="fade">
        <b-card bg-variant="secondary" text-variant="white" v-if="files.length > 1">
          <p class="lead">Загруженные файлы:</p>
          <b-list-group>
            <transition-group name="fade" tag="p">
              <b-list-group-item v-for="(file, i) in files" variant="secondary" :key="'f' + i">
                <div class="row justify-content-between">
                  <div class="col col-md-auto">
                    {{file.name}} ({{getFileSize(file.size)}})
                  </div>
                  <div class="col col-md-auto">
                    <b-icon-backspace scale="1.5" class="delete-file-icon" @click="deleteFile(i)"></b-icon-backspace>
                  </div>
                </div>
              </b-list-group-item>
            </transition-group>
          </b-list-group>
        </b-card>
      </transition>
      <div class="row justify-content-center" v-if="files.length > 0">
        <div class="col col-md-auto pt-3">
          <b-button variant="success" @click="uploadFiles">Загрузить ({{fileSumSize}})</b-button>
        </div>
      </div>
    </b-form>
    <!-- Конец Загрузка файлов !-->
  </div>
</template>

<script lang="ts" src="./index.ts">
</script>

<style scoped src="./index.css">

</style>