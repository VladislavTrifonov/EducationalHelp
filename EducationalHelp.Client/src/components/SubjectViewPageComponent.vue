<template>
    <div class="col-12">
        <h3>{{Model.name}}</h3>
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
            <b-tab title="Успеваемость">

            </b-tab>
            <b-tab title="Дополнительные материалы">

            </b-tab>
        </b-tabs>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { Component } from 'vue-property-decorator';
    import Subject from '../api/models/Subject';
    import { IErrorDetails, IValidationDetails, Response } from '../store/modules/ErrorProcessing';

    @Component({})
    export default class SubjectViewPageComponent extends Vue {

        get Model(): Subject {
            return this.$store.getters['subjects/getSubject'](this.$route.params.id);
        }

        constructor() {
            super();
        }

        created() {
            this.fetchSubject();
        }

        beforeRouteUpdate(to: any, from: any, next: any) {
            this.fetchSubject();
            next();
        }

        fetchSubject() {
            this.$store.dispatch("subjects/fetchSubject", this.$route.params.id).
                then((error: Response) => {
                    error.process((details) => {
                    });
                });
        }
    }

</script>

<style scoped>
</style>