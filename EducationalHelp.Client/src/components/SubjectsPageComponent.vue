<template>
    <div class="col-12">
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

<script lang="ts">
    import 'bootstrap-vue'
    import Vue from 'vue';
    import { Component } from 'vue-property-decorator';
    import Subject from '@/api/models/Subject';
    import TableGrid from './system/TableGrid.vue';
    import { Response, IErrorDetails, IValidationDetails, IValidationError } from '@/store/modules/ErrorProcessing';
    import AddSubjectComponent from './AddSubjectComponent.vue';

    @Component({
        components: {
            'table-grid': TableGrid,
            'add-subject': AddSubjectComponent
        }

    })
    export default class SubjectsPageComponent extends Vue {
        public addModel: Subject;
        private validationState: Array<IValidationError>;

        constructor() {
            super();
            this.addModel = new Subject();
            this.validationState = new Array<IValidationError>();
        }

        public LinkToSubject(subject: Subject): String {
            return this.$router.resolve({ name: "subjectView", params: { "id": subject.id } }).href;
        }

        get validationErrors(): Array<IValidationError> {
            return this.validationState;
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

        get newSubjectModel(): Subject {
            return this.addModel;
        }

        created() {
            this.fetchSubjects();   
        }

        fetchSubjects() {
            this.$store.dispatch("subjects/fetchSubjects").then((error: Response) => {
            });
        }

        onTableUpdate(): void {
            this.fetchSubjects();
        }

        onTableAdd(): void {
            this.$bvModal.show('addSubjectModal');
        }

        onAddModalOk(bvEvtModal: any) {
            bvEvtModal.preventDefault();
            this.validationState = new Array<IValidationError>();
            this.$store.dispatch("subjects/addSubject", this.addModel).then((r: Response) => {
                if (r.isOk)
                    this.$nextTick(() => {
                        this.$bvModal.hide('addSubjectModal');
                    });
                r.process(undefined, (validation: IErrorDetails<IValidationDetails>) => {
                    for (let i = 0; i < validation.details.errorCount; i++) {
                        this.validationState.push(validation.details.listOfErrors[i]);
                    }
                });
            });
        }

        onAddModalCancel() {
            this.addModel = new Subject();
            this.validationState = new Array<IValidationError>();
        }

    }

</script>

<style scoped>
  
</style>