import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Subject from '../../api/models/Subject';
import AddSubject from '@/pages/AddSubjectPage/index.vue';
import { Response } from '../../store/modules/ErrorProcessing';
import LessonAPI from "@/api/LessonAPI";
import Lesson from "@/api/models/Lesson";

@Component({
    components: {
        'add-subject': AddSubject
    }
})
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
