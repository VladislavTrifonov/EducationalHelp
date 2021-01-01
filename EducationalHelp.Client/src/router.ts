import VueRouter from 'vue-router';
import Vue from "vue";
import MainPageComponent from "@/pages/MainPage/index.vue";
import Error404PageComponent from "@/pages/Error404Page/index.vue";
import SubjectsPageComponent from "@/pages/SubjectsPage/index.vue";
import SubjectViewPageComponent from "@/pages/SubjectsViewPage/index.vue";
import LessonSinglePage from "@/pages/LessonSinglePage/index.vue";
import CalendarPage from "@/pages/CalendarPage/index.vue";
import LoginPage from "@/pages/LoginPage/index.vue";

Vue.use(VueRouter)

const routes = [
    { name: 'homePage', path: '/', component: MainPageComponent },
    { name: 'error404', path: '*', component: Error404PageComponent },
    { name: 'subjectsList', path: '/subjects', component: SubjectsPageComponent },
    { name: 'subjectView', path: '/subjects/v-:id', component: SubjectViewPageComponent },
    { name: 'lessonView', path: '/subjects/lessons/:lessonId', component: LessonSinglePage, props: {
        isCreationMode: false
        } },
    { name: 'lessonCreate', path: '/subjects/:subjectId/createLesson', component: LessonSinglePage, props: {
            isCreationMode: true
    }},
    { name: 'calendarView', path: '/calendar', component: CalendarPage },
    { name: 'loginView', path: '/login', component: LoginPage },
    { name: 'profileView', path: '/profile' }

];

export default new VueRouter((
    {
        mode: 'history',
        routes: routes
    }));

