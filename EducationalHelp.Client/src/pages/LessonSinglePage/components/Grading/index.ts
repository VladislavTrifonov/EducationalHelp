import Vue from 'vue';
import {Component, Prop, Watch} from "vue-property-decorator";
import Lesson, {Marks} from "@/api/models/Lesson";
import LessonParticipant from "@/api/models/LessonParticipant";
import User from "@/api/models/User";
import LessonAPI from "@/api/LessonAPI";
import {Response} from "@/store/modules/ErrorProcessing";

@Component({})
export default class Grading extends Vue {
    public marks: any;

    @Prop({type: Object as () => Lesson})
    public lesson!: Lesson;

    @Prop()
    private gradingParticipants!: Array<LessonParticipant>;

    @Prop()
    private participants!: Array<User>;
    private lessonApi: LessonAPI;

    constructor() {
        super();
        this.marks = [
            {value: Marks.None, text: "Нет"},
            {value: Marks.Excellent, text: "Отлично (5)"},
            {value: Marks.Good, text: "Хорошо (4)"},
            {value: Marks.Satisfactory, text: "Удовл. (3)"},
            {value: Marks.Unsatisfactory, text: "Неудовл. (2)"},
            {value: Marks.Poor, text: "Плохо (1)"},
        ];
        this.lessonApi = new LessonAPI();
    }

    get markName(): string {
        return this.marks.filter((m: any) => m.value == this.lesson.selfMark)[0].text;
    }

    saveButton() {
        this.gradingParticipants.forEach(value => {
           Response.fromPromise(this.lessonApi.updateParticipant(this.lesson.id, value), response => {
               console.log(response);
           });
        });
    }
}
