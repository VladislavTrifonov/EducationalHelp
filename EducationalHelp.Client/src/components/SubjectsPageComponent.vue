<template>
    <div class="col-12">
        <b-table-simple hover caption-top responsive>
            <caption>Список предметов:</caption>
            <b-thead head-variant="dark">
                <b-tr>
                    <b-th># (п/п)</b-th>
                    <b-th>Предмет</b-th>
                    <b-th>Преподаватель</b-th>
                </b-tr>
            </b-thead>
            <b-tbody>
                <b-tr v-for="(subject, i) in subjects" :key="subject.id">
                    <b-td>{{i}}</b-td>
                    <b-td>
                        <router-link :to="LinkToSubject(subject)">{{subject.name}}</router-link>
                    </b-td>
                    <b-td>{{subject.teacher}}</b-td>
                </b-tr>
            </b-tbody>
        </b-table-simple>
        <table-grid :keys="keys" :items="subjects">
            <template v-slot:rowContent="props">
                <b-td>{{props.index}}</b-td>
                <b-td>{{props.item.name}}</b-td>
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
                { head: "Название" },
                { head: "Преподаватель" },
            ];
        }

        created() {
            this.$store.dispatch("subjects/fetchSubjects");
        }

        constructor() {
            super();
        }


    }

</script>

<style scoped>
  
</style>