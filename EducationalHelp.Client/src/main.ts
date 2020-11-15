import Vue from 'vue';
import Vuex from 'vuex';
// @ts-ignore
import CKEditor from '@ckeditor/ckeditor5-vue';
import '@ckeditor/ckeditor5-build-classic/build/translations/ru';
import store from './store/index'
import App from './App.vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
import router from '@/router.ts';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
//@ts-ignore
import vuetify from '@/plugins/vuetify'
Vue.config.productionTip = true;

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(CKEditor)
Vue.use(Vuex)


new Vue({
    //@ts-ignore
    vuetify,
    render: h => h(App),
    router: router,
    store: store
}).$mount('#app');
