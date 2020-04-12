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
                <b-tr v-for="(subject, i) in Subjects" :key="subject.id">
                    <b-td>{{i}}</b-td>
                    <b-td>
                        <router-link :to="LinkToSubject(subject)">{{subject.name}}</router-link>
                    </b-td>
                    <b-td>{{subject.teacher}}</b-td>
                </b-tr>
            </b-tbody>
        </b-table-simple>
    </div>
</template>

<script lang="ts">
    import 'bootstrap-vue'
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import Subject from '@/api/models/Subject';
    import Store from '@/store/index';
    import { mapState, mapActions } from 'vuex';
    import SubjectsState from '@/store/modules/subjects/state';
    import { Dictionary } from 'vue-router/types/router';

    @Component({

    })
    export default class SubjectsPageComponent extends Vue {

        public subjects!: Array<Subject>;
        public fields: Array<any>; // in render only

        get Subjects() {
            return this.$store.state.subjects.all;
        }

        public LinkToSubject(subject: Subject): String {
            return this.$route.path + '/' + subject.name;
        }

        constructor() {
            super();
            this.fields = [
                {
                    key: 'name',
                    sortable: true,
                    label: "Название предмета"
                },
                {
                    key: 'teacher',
                    sortable: true,
                    label: "Преподаватель"
                }
            ];
        }

        
    }

</script>

<style scoped>
  
</style>