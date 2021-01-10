import {Component} from "vue-property-decorator";
import Vue from "vue";
import Lesson from "@/api/models/Lesson";
import {mapGetters} from "vuex";
import Subject from "@/api/models/Subject";

@Component({

})
export default class LessonsListComponent extends Vue {
    get lessons(): Array<Lesson> {
        return this.$store.getters['subjects/getLessons'](this.$route.params.id);
    }

    constructor() {
        super();
    }

    getTimeOfWaiting(date: string): string {
        let days = (new Date(date).getTime() - new Date().getTime()) / 86400000; // получаем разность в между датами в мс! и делим на 86400 * 10^4
        let hours = Math.floor((days % 1) * 24); // days % 1 - возвращает дробную часть days, умножаем на 24 -> получаем кол-во оставшихся часов
        return Math.floor(days).toString() + " дн. и " + hours.toString() + " час.";
    }
}
