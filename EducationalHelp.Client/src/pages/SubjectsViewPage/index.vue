<template>
    <div class="col-12">
        <div class="row">
            <h3>
                {{Model.name}}
            </h3>
            <b-button-group>
                <b-button size="sm" variant="light" @click="onEditClick()"><b-icon-pencil></b-icon-pencil> Редактировать</b-button>
                <b-button size="sm" variant="light" @click="onDeleteClick()"><b-icon-trash></b-icon-trash> Удалить</b-button>
            </b-button-group>
        </div>
        <created-updated-info :entity="Model"></created-updated-info>
        <b-tabs content-class="mt-3">
            <b-tab title="Информация" active>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            Преподаватель
                        </div>
                        <div class="col">
                            {{Model.teacher}}
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            Описание
                        </div>
                        <div class="col">
                            {{Model.description}}
                        </div>
                    </div>
                </div>
            </b-tab>
            <b-tab title="Учебная литература">

            </b-tab>
            <b-tab title="Занятия">
              <lessons-list :p_lessons="Model.lessons">
              </lessons-list>
            </b-tab>

        </b-tabs>
        <b-modal id="editSubjectModal" @ok="onEditModalOk">
            <template v-slot:modal-header="{ close }">
                <b-button size="sm" variant="outline-primary" @click="close()">
                    Закрыть
                </b-button>
                <h5>Редактирование</h5>
            </template>
            <template v-slot:default>
                <add-subject :model="Model" :validationStates="[]"></add-subject>
            </template>
            <template v-slot:modal-footer="{ ok, cancel, hide }">
                <b-button variant="primary" @click="ok()">Изменить</b-button>
                <b-button variant="outline-primary" @click="cancel()">Отмена</b-button>
            </template>
        </b-modal>
    </div>
</template>

<script lang="ts" src="./index.ts">

</script>

<style scoped src="./index.css">

</style>