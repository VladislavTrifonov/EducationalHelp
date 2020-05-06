<template>
    <div class="col-12">
        <table-grid :keys="keys" :items="subjects" header="Список предметов" v-on:update-trigger="onTableUpdate">
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
    </div>
</template>

<script lang="ts">
    import 'bootstrap-vue'
    import Vue from 'vue';
    import { Component, Prop } from 'vue-property-decorator';
    import Subject from '@/api/models/Subject';
    import TableGrid from './system/TableGrid.vue';
    import Store from '@/store/index';
    import { mapState, mapActions } from 'vuex';
    import SubjectsState from '@/store/modules/subjects/state';
    import { Dictionary } from 'vue-router/types/router';
    import { Response, ErrorTypes, IErrorDetails, IValidationDetails, IValidationError } from '@/store/modules/ErrorProcessing';

    @Component({
        components: {
            'table-grid': TableGrid
        }

    })
    export default class SubjectsPageComponent extends Vue {


        public LinkToSubject(subject: Subject): String {
            return this.$router.resolve({ name: "subjectView", params: { "id": subject.id } }).href;
        }

        get subjects(): Array<Subject> {
            return this.$store.state.subjects.all;
        }

        get keys(): any {
            return [
                { head: "#" },
                { head: "Предмет" },
                { head: "Преподаватель" },
            ];
        }

        created() {
            this.$store.dispatch("subjects/fetchSubjects").then(error => {
                error.process((details: IErrorDetails<IValidationDetails>) => {
                    console.log(details);
                });
            });
        }

        onTableUpdate(): void {
            this.$store.commit("subjects/addSubject", {
                name: "test", 
                teacher: "Test2"
            });
        }

        constructor() {
            super();
        }


    }

</script>

<style scoped>
  
</style>