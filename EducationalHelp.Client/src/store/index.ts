import Vue from 'vue'
import Vuex from 'vuex'

import RootState from './rootstate'

import { SubjectsModule } from './modules/subjects/subjects'
import {UserModule} from "@/store/modules/user/user";

Vue.use(Vuex)

const debug: boolean = process.env.NODE_ENV !== 'production'

export default new Vuex.Store<RootState>({
    modules: {
        subjects: new SubjectsModule(),
        user: new UserModule()
    },
    strict: debug
})
