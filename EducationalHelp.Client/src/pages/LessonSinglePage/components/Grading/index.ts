import Vue from 'vue';
import {Component} from "vue-property-decorator";
import {Marks} from "@/api/models/Lesson";

@Component({
    props: ['lesson']
})
export default class Grading extends Vue {
    public markEditing: boolean = false;
    public marks: any;

    constructor() {
        super();
        this.marks = [
            {value: Marks.None, text: "Нет"},
            {value: Marks.Excellent, text: "Отлично (5)"},
            {value: Marks.Good, text: "Хорошо (4)"},
            {value: Marks.Satisfactory, text: "Удовл. (3)"},
            {value: Marks.Unsatisfactory, text: "Неудовл. (2)"},
            {value: Marks.Poor, text: "Плохо (1)"},
        ]
    }

    get markName(): string {
        return this.marks.filter((m: any) => m.value == this.$props.lesson.selfMark)[0].text;
    }

    setMark() {
        this.markEditing = !this.markEditing;
    }
}