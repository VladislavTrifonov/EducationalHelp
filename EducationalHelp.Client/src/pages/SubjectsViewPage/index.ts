import Vue from 'vue';
import {Component, Watch} from 'vue-property-decorator';
import Subject from '../../api/models/Subject';
import AddSubject from '@/pages/AddSubjectPage/index.vue';
import { Response } from '../../store/modules/ErrorProcessing';
import LessonsListComponent from "@/pages/SubjectsViewPage/components/LessonsList/index.vue";
import CreatedUpdatedInfo from "@/components/CreatedUpdatedInfo/index.vue";
import {IBreadcrumb} from "@/components/Breadcrumbs/index.ts";
import BreadcrumbsComponent from "@/components/Breadcrumbs/index.vue";
import {bc_subjectView} from "@/breadcrumbs";

@Component({
    components: {
        'add-subject': AddSubject,
        'lessons-list': LessonsListComponent,
        'created-updated-info': CreatedUpdatedInfo,
        'breadcrumbs': BreadcrumbsComponent
    }
})
export default class SubjectViewPageComponent extends Vue {

    get Model(): Subject {
        return this.$store.getters['subjects/getSubject'](this.$route.params.id);
    }

    constructor() {
        super();
    }

    mounted() {

    }

    created() {
        this.fetchSubject();
    }

    beforeRouteUpdate(to: any, from: any, next: any) {
        this.fetchSubject();
        next();
    }

    get breadcrumbs(): Array<IBreadcrumb> {
        let bcrumbs = bc_subjectView({id: this.Model.id, name: this.Model.name});
        bcrumbs[bcrumbs.length - 1].link = false;
        return bcrumbs;
    }

    fetchSubject() {
        this.$store.dispatch("subjects/fetchSubject", this.$route.params.id).
        then((error: Response) => {
            error.process((details) => {
                this.$router.push({ name: "error404", params: { id: this.$route.params.id } });
            });
        });
    }

    onEditClick() {
        this.$bvModal.show('editSubjectModal');
    }

    onEditModalOk() {
        this.$store.dispatch("subjects/updateSubject", this.Model).then((response: Response) => {

        });
    }

    onDeleteClick() {
        this.$bvModal.msgBoxConfirm('Вы действительно хотите удалить этот элемент?', {
            title: 'Необходимо подтверждение',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'danger',
            okTitle: 'Да',
            cancelTitle: 'Нет',
            footerClass: 'p-2',
            hideHeaderClose: false,
            centered: true
        })
            .then(value => {
                if (value) {
                    this.$store.dispatch("subjects/deleteSubject", this.Model).then((response: Response) => {
                        if (response.isOk) {
                            this.$router.push({ name: "subjectsList" });
                        }
                    });
                }
            });
    }
}
