import {Component, Prop, Watch} from "vue-property-decorator";
import Vue, {WatchOptions} from "vue";
import Lesson from "@/api/models/Lesson";
import { Response } from "@/store/modules/ErrorProcessing";
import LessonAPI from "@/api/LessonAPI";
import CreatedUpdatedInfo from "@/components/CreatedUpdatedInfo/index.vue";
import Grading from "@/pages/LessonSinglePage/components/Grading/index.vue";
import Contents from "@/pages/LessonSinglePage/components/Contents/index.vue";
import LessonHeader from "@/pages/LessonSinglePage/components/LessonHeader/index.vue";
import Homework from "@/pages/LessonSinglePage/components/Homework/index.vue";

@Component({
    components: {
        'created-updated-info': CreatedUpdatedInfo,
        'lesson-header': LessonHeader,
        'grading': Grading,
        'contents': Contents,
        'homework': Homework
    }
})
export default class LessonSinglePage extends Vue {
    public lesson: Lesson;
    public notesEditing: boolean;
    private lessonApi: LessonAPI;
    public isSaveBtnShow: boolean;
    @Prop({
        default: () => false
    })
    public isCreationMode!: boolean;

    constructor() {
        super();
        this.notesEditing = this.isCreationMode;
        this.isSaveBtnShow = false;
        this.lesson = Lesson.Empty;
        this.lessonApi = new LessonAPI();
    }

    mounted() {
        if (this.$route.params.lessonId != null)
            this.fetchLesson(); // загрузка уже существующего Lesson
        else
            this.lesson = new Lesson(); // создание нового Lesson
    }

    @Watch('isCreationMode')
    updateNotesEditing() {
        this.notesEditing = this.isCreationMode
    }

    // Получение данных по API для необходимого занятия
    fetchLesson() {
        this.lesson = Lesson.Empty;
        let response = Response.fromPromise(this.lessonApi.getLessonById(this.$route.params.subjectId, this.$route.params.lessonId), (response) => {
           this.lesson = response;
           console.log(response);
        }).catch((error: Response) => {
           error.process(() => {
               alert("Упс, что-то пошло не так... Информация для разработчиков в консоли.");
               console.log(error);
           });
        });
    }

    get isLessonLoaded(): boolean {
        return this.lesson != Lesson.Empty;
    }

    @Watch('lesson', {
        deep: true // отслеживать изменения полей объекта
    })
    onLessonChanged(value: any, oldValue: any) {
        // Пропустить первый вызов, когда только пришли данные от сервера (не является обновлением данных пользователем)
        if (oldValue == Lesson.Empty || this.isCreationMode)
            return;

        if (!this.isSaveBtnShow) {
            this.isSaveBtnShow = true;
        }
    }

    // Обновление информации о Lesson на сервер
    saveBtnClick() {
        if (this.lesson == null)
            return;

        this.isSaveBtnShow = false;
        let response = Response.fromPromise(this.lessonApi.updateLesson(this.$route.params.subjectId, this.$route.params.lessonId, this.lesson), (response) => {
           this.lesson = response;
           this.$bvToast.toast('Сохранение успешно выполнено', {
               variant: "success",
               autoHideDelay: 5000,
               isStatus: true,
               toaster: 'b-toaster-bottom-left'
           })
        }).catch((error: Response) => {
            error.process(() => {
                this.isSaveBtnShow = true;
                alert("Упс.. что-то пошло не так, смотрите консоль")
                console.log(error)
            })
        });
    }

    createLesson() {
        let response = Response.fromPromise(this.lessonApi.createLesson(this.$route.params.subjectId, this.lesson), (response) => {
            this.$router.push({
                name: 'lessonView',
                params: {
                    subjectId: this.$route.params.subjectId,
                    lessonId: response.id
                }
            })
        }).catch((error: Response) => {
           error.process(() => {
               alert('Что-то пошло не так')
               console.log(error)
           })
        });
    }

    deleteLesson() {
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
                    let response = Response.fromPromise(this.lessonApi.deleteLesson(this.$route.params.subjectId, this.$route.params.lessonId), (response) => {
                        this.$store.dispatch("subjects/fetchSubjects");
                        console.log('subjid: ', this.$route.params.subjectId)
                        this.$router.push({
                            name: 'subjectView',
                            params: {
                                id: this.$route.params.subjectId
                            }
                        });
                    }).catch((error: Response) => {
                        error.process(() => {
                            alert("Что-то пошлое не так")
                            console.log(error)
                        })
                    })
                }
            });

    }
}