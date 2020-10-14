<template>
    <b-container>
        <b-form-group label="Название">
            <b-form-input :state="getValidationState('name')" v-model="model.name" type="text"></b-form-input>
        </b-form-group>
        <b-form-group label="Преподаватель">
            <b-form-input :state="getValidationState('teacher')" v-model="model.teacher" type="text"></b-form-input>
        </b-form-group>
        <b-form-group label="Описание">
            <b-form-textarea :state="getValidationState('description')" v-model="model.description"></b-form-textarea>
        </b-form-group>
    </b-container>
</template>

<script src="./index.js">

</script>

<style scoped src="./index.css">
</style>