import Vue from 'vue';
import Vuex from 'vuex';
import store from './store/index'
import App from './App.vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
import router from '@/router.ts';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.config.productionTip = true;

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

Vue.use(Vuex)


new Vue({
    render: h => h(App),
    router: router,
    store: store
}).$mount('#app');
