<template>
    <div class="col-12">
      <breadcrumbs :breadcrumbs="breadcrumbs"></breadcrumbs>
      <group-view></group-view>
        <table-grid :keys="keys" :items="subjects" header="Список предметов" :isAddPossible="true" v-on:add-trigger="onTableAdd" v-on:update-trigger="onTableUpdate">
            <template v-slot:rowContent="props">
                <b-td>{{props.index + 1}}</b-td>
                <b-td>
                    <router-link :to="LinkToSubject(props.item)">
                        {{props.item.name}}
                    </router-link>
                </b-td>
                <b-td>{{props.item.teacher}}</b-td>
            </template>
        </table-grid>
        <b-modal id="addSubjectModal" @ok="onAddModalOk" @cancel="onAddModalCancel">
            <template v-slot:modal-header="{ close }">
                <b-button size="sm" variant="outline-primary" @click="close()">
                    Закрыть
                </b-button>
                <h5>Добавить новый предмет</h5>
            </template>
            <template v-slot:default>
                <add-subject :model="newSubjectModel" :validationStates="validationErrors"></add-subject>
            </template>
            <template v-slot:modal-footer="{ ok, cancel, hide }">
                <b-button variant="primary" @click="ok()">Добавить</b-button>
                <b-button variant="outline-primary" @click="cancel()">Отмена</b-button>
            </template>
        </b-modal>
    </div>
</template>

<script lang="ts" src="./index.ts">

</script>

<style scoped src="./index.css">
  
</style>
