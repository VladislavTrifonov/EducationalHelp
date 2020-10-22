import Vue from 'vue';
import {Component, Prop, Watch} from "vue-property-decorator";
import Lesson, {Marks} from "@/api/models/Lesson";

@Component({})
export default class Grading extends Vue {
    public markEditing!: boolean;
    public marks: any;

    @Prop({type: Object as () => Lesson})
    public lesson!: Lesson;

    @Prop({
        default: () => false
    })
    public editing!: boolean;

    constructor() {
        super();
        this.markEditing = this.editing;
        this.marks = [
            {value: Marks.None, text: "Нет"},
            {value: Marks.Excellent, text: "Отлично (5)"},
            {value: Marks.Good, text: "Хорошо (4)"},
            {value: Marks.Satisfactory, text: "Удовл. (3)"},
            {value: Marks.Unsatisfactory, text: "Неудовл. (2)"},
            {value: Marks.Poor, text: "Плохо (1)"},
        ]
    }

    @Watch('editing')
    updateMarkEditing() {
        this.markEditing = this.editing
    }

    get markName(): string {
        return this.marks.filter((m: any) => m.value == this.lesson.selfMark)[0].text;
    }

    setMark() {
        this.markEditing = !this.markEditing;
    }
}