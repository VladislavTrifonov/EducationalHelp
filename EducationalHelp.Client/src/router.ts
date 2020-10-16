import VueRouter from 'vue-router';
import Vue from "vue";
import MainPageComponent from "@/pages/MainPage/index.vue";
import Error404PageComponent from "@/pages/Error404Page/index.vue";
import SubjectsPageComponent from "@/pages/SubjectsPage/index.vue";
import SubjectViewPageComponent from "@/pages/SubjectsViewPage/index.vue";
import LessonSinglePage from "@/pages/LessonSinglePage/index.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'homePage', path: '/', component: MainPageComponent },
    { name: 'error404', path: '*', component: Error404PageComponent },
    { name: 'subjectsList', path: '/subjects', component: SubjectsPageComponent },
    { name: 'subjectView', path: '/subjects/v-:id', component: SubjectViewPageComponent },
    { name: 'lessonView', path: '/subjects/:subjectId-l-:lessonId', component: LessonSinglePage }

];

export default new VueRouter((
    {
        mode: 'history',
        routes: routes
    }));

