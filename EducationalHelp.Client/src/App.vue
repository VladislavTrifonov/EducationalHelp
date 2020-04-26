<template>
    <div class="container-fluid">
        <div class="container-fluid">
            <Navigation />
        </div>
        <div class="container p-3">
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
