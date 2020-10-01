import Vue from 'vue';
import axios from '@/axiosconf';
import Vuex from 'vuex';
import store from './store/index'
import App from './App.vue';
import MainPageComponent from './components/MainPageComponent.vue'
import SubjectsPageComponent from './components/SubjectsPageComponent.vue'
import SubjectViewPageComponent from './components/SubjectViewPageComponent.vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
import VueRouter from 'vue-router';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Error404PageComponent from './components/Error404PageComponent.vue';

Vue.config.productionTip = true;

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(VueRouter)
Vue.use(Vuex)


const routes = [
    { name: 'homePage', path: '/', component: MainPageComponent },
    { name: 'error404', path: '*', component: Error404PageComponent },
    { name: 'subjectsList', path: '/subjects', component: SubjectsPageComponent },
    { name: 'subjectView', path: '/subjects/view.:id', component: SubjectViewPageComponent }
];

const router = new VueRouter((
    {
        mode: 'history',
        routes: routes
    }));

new Vue({
    render: h => h(App),
    router: router,
    store: store
}).$mount('#app');
