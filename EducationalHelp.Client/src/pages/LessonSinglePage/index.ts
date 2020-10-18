import {Component, Watch} from "vue-property-decorator";
import Vue from "vue";
import Lesson from "@/api/models/Lesson";
import { Response } from "@/store/modules/ErrorProcessing";
import LessonAPI from "@/api/LessonAPI";
import CreatedUpdatedInfo from "@/components/CreatedUpdatedInfo/index.vue";
import Grading from "@/pages/LessonSinglePage/components/Grading/index.vue";
import Contents from "@/pages/LessonSinglePage/components/Contents/index.vue";
import LessonHeader from "@/pages/LessonSinglePage/components/LessonHeader/index.vue";

@Component({
    components: {
        'created-updated-info': CreatedUpdatedInfo,
        'lesson-header': LessonHeader,
        'grading': Grading,
        'contents': Contents
    }
})
export default class LessonSinglePage extends Vue {
    public lesson: Lesson;
    public notesEditing: boolean;
    private lessonApi: LessonAPI;

    constructor() {
        super();
        this.notesEditing = false;
        this.lesson = new Lesson();
        this.lessonApi = new LessonAPI();
    }

    mounted() {
        this.fetchLesson();
    }

    // Получение данных по API для необходимого занятия
    fetchLesson() {
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

    @Watch('lesson.selfMark')
    @Watch('lesson.isVisited')
    onLessonChanged(value: Lesson, oldValue: Lesson) {
        console.log("new value -> old value")
        console.log(value)
        console.log(oldValue)
    }

}