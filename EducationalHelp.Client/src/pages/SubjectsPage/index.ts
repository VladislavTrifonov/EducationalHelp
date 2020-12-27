import 'bootstrap-vue'
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Subject from '@/api/models/Subject';
import TableGrid from '../../components/system/TableGrid.vue';
import { Response, IErrorDetails, IValidationDetails, IValidationError } from '@/store/modules/ErrorProcessing';
import AddSubjectComponent from '../AddSubjectPage/index.vue';
import BreadcrumbsComponent from "@/components/Breadcrumbs/index.vue";
import { IBreadcrumb } from "@/components/Breadcrumbs/index.ts";
import {bc_subjectsList} from "@/breadcrumbs";

@Component({
    components: {
        'table-grid': TableGrid,
        'add-subject': AddSubjectComponent,
        'breadcrumbs': BreadcrumbsComponent
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

    get breadcrumbs(): Array<IBreadcrumb> {
        let bcrumbs = bc_subjectsList();
        bcrumbs[bcrumbs.length - 1].link = false;
        return bcrumbs;
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

    mounted() {
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
