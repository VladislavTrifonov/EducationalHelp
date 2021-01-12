<template>
  <div class="container-fluid">
    <b-card>
      <b-card-title>
        О приложении
      </b-card-title>
      Приложение предназначено для пользователей, которые хотят освоить какую-либо область знаний. Приложение подойдет как для индивидуального использования, так и для группового.
      В первом случае, пользователь сам создает для себя занятия и сам отмечает активность (оценивание) их. Статистика доступна в профиле пользователя.
      <br><br>
      Также пользователи могут объединиться в группы. Только участники группы смогут видеть создаваемый внутри контент. Один пользователь может вступать и создавать неограниченное количество
      групп. Статистика и предметы в таком случае отображаются в зависимости от выбранной группы.
      <b-card-sub-title class="pt-3"><b>Статус проекта и реализованные модули</b></b-card-sub-title>
      <b-list-group class="mt-2">
        <b-list-group-item>
          <div>
            Модуль "Предметы"
            <b-progress class="mt-2" value="100" max="100" variant="success" show-progress animated>
            </b-progress>
            <ul class="p-2">
              <li>Создание, обновление и удаление предметов</li>
            </ul>
          </div>
        </b-list-group-item>
        <b-list-group-item>
          <div>
            Модуль "Занятия"
            <b-progress class="mt-2" value="100" max="100" variant="success" show-progress animated>
            </b-progress>
            <ul class="p-2">
              <li>Создание, обновление и удаление занятий</li>
              <li>Занятия прикреплены к предметам</li>
              <li>Загрузка файлов</li>
              <li>Прикрепление / открепление участников к занятиям</li>
              <li>Оценивание итогов участников</li>
            </ul>
          </div>
        </b-list-group-item>
        <b-list-group-item>
          <div>
            Модуль "Календарь"
            <b-progress class="mt-2" value="100" max="100" variant="success" show-progress animated>
            </b-progress>
            <ul class="p-2">
              <li>Отображение предстоящих событий (занятий)</li>
              <li>Экспорт в .ics формате для вставки во внешний календарь</li>
            </ul>
          </div>
        </b-list-group-item>
        <b-list-group-item>
          <div>
            Модуль "Статистика"
            <b-progress class="mt-2" value="100" max="100" variant="success" show-progress animated>
            </b-progress>
            <ul class="p-2">
              <li>Вывод статистических данных в соответствии с группой в профиле пользователя</li>
            </ul>
          </div>
        </b-list-group-item>
        <b-list-group-item>
          <div>
            Модуль "Группы"
            <b-progress class="mt-2" :value="(3/8) * 100" max="100" variant="warning" show-progress animated>
            </b-progress>
            <ul class="p-2">
              <li>Распределение контента по группам</li>
              <li><b-icon-exclamation-triangle-fill></b-icon-exclamation-triangle-fill> Создание, обновление групп</li>
              <li><b-icon-exclamation-triangle-fill></b-icon-exclamation-triangle-fill> Вступление в группы</li>
              <li>Выход из группы</li>
              <li>Просмотр контента в зависимости от выбранной группы</li>
              <li><b-icon-exclamation-triangle-fill></b-icon-exclamation-triangle-fill> Роли в группах</li>
              <li><b-icon-exclamation-triangle-fill></b-icon-exclamation-triangle-fill> Удаление участников из группы владельцем</li>
              <li><b-icon-exclamation-triangle-fill></b-icon-exclamation-triangle-fill> Обмен сообщениями внутри группы (например, чат)</li>
              <p class="small">* Пункты, помеченные <b-icon-exclamation-triangle-fill></b-icon-exclamation-triangle-fill>, в данный момент не реализованы в проекте.</p>
            </ul>
          </div>
        </b-list-group-item>
      </b-list-group>
    </b-card>
  </div>
</template>

<script lang="ts" src="./index.ts">

</script>

<style scoped src="./index.css">
</style>
