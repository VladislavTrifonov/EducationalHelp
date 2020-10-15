import {Component} from "vue-property-decorator";
import Vue from "vue";
import Lesson from "@/api/models/Lesson";

@Component({
    components: {

    },
    props: {
        p_lessons: Array
    }
})
export default class LessonsListComponent extends Vue {
    get lessons(): Array<Lesson>
    {
        return this.$props.p_lessons
    }

    constructor() {
        super();
    }
}