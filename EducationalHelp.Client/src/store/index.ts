import Vue from 'vue'
import Vuex from 'vuex'
import VuexPersistence from 'vuex-persist';
import RootState from './rootstate'

import { SubjectsModule } from './modules/subjects/subjects'
import {UserModule} from "@/store/modules/user/user";
import State from "./rootstate";


Vue.use(Vuex)

const vuexLocal = new VuexPersistence<State>({
    storage: window.localStorage,
    reducer: (state) => ({ user: state.user })
})

const debug: boolean = process.env.NODE_ENV !== 'production'

export default new Vuex.Store<RootState>({
    modules: {
        subjects: new SubjectsModule(),
        user: new UserModule()
    },
    plugins: [vuexLocal.plugin]
})
