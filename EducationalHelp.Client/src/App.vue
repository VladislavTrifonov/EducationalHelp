<template>
    <div class="container-fluid">
        <div class="container-fluid">
            <Navigation />
        </div>
        <div class="container p-3">
            <div class="row">
                <div class="alert alert-danger">
                    <b-icon-exclamation-octagon-fill></b-icon-exclamation-octagon-fill> Возникли проблемы с интернет-подключением к серверу.
                    <b-link>Обновить страницу...</b-link>
                </div>
            </div>
            <div class="row">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { Component } from 'vue-property-decorator';
    import Navigation from './components/Navigation.vue';

    @Component({
        components: {
            Navigation
        }
    })
    export default class App extends Vue { }
</script> 

<style>
</style>
