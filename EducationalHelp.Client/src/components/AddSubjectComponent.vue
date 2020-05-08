<template>
    <b-container>
        <b-form-group label="Название">
            <b-form-input v-model="model.name" type="text"></b-form-input>
        </b-form-group>
        <b-form-group label="Преподаватель">
            <b-form-input v-model="model.teacher" type="text"></b-form-input>
        </b-form-group>
        <b-form-group label="Описание">
            <b-form-textarea v-model="model.description"></b-form-textarea>
        </b-form-group>
    </b-container>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { Component } from 'vue-property-decorator';

    @Component({
        props: ['model']
    })
    export default class AddSubjectComponent extends Vue {
    }
</script>

<style scoped>
</style>