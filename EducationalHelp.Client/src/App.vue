<template>
    <div class="container-fluid">
        <div class="container-fluid">
            <Navigation />
        </div>
        <div class="container p-3">
            <NetworkError v-bind:show="isNetworkError"></NetworkError>
            <div class="row">
                <router-view></router-view>
            </div>
        </div>
        <b-toast toaster="b-toaster-bottom-left" no-auto-hide no-close-button v-bind:visible="isBusy">
            <LoadProgress></LoadProgress>
        </b-toast>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { processing } from './axiosconf';
    import { Component } from 'vue-property-decorator';
    import Navigation from './components/Navigation/index.vue';
    import NetworkError from './components/system/NetworkError.vue';
    import LoadProgress from './components/system/LoadProgress.vue'

    @Component({
        components: {
            Navigation,
            NetworkError,
            LoadProgress
        }
    })
    export default class App extends Vue {

        private isNetworkError: boolean = false;
        private isBusy: boolean = false;

        data() {
            return {
                isBusy: this.isBusy,
                isNetworkError: this.isNetworkError
            }
        }
        mounted() {

        }
        created() {
            processing.onRequestStart = (request) => {
                this.isNetworkError = false;
                this.isBusy = true;
                
            }

            processing.onNetworkError = (error) => {
                this.isNetworkError = true;
                this.isBusy = false;
            }

            processing.onResponseResolved = (response) => {
                this.isBusy = false;
            }
        }
    }
</script> 

<style>
</style>
