import { GetterTree } from 'vuex';
import { MutationTree } from 'vuex';
import { ActionTree } from 'vuex';
import { Module } from 'vuex';
import SubjectsState from './state';
import RootState from '../../rootstate';
import Subject from '../../../api/models/Subject';


const getters: GetterTree<SubjectsState, RootState> = {
    getSubject: (state) => (id: String) => {
        let subject = state.all.filter(s => s.id == id);
        if (subject == undefined)
            return undefined;
        else
            return subject[0];
        
    }
};

const mutations: MutationTree<SubjectsState> = {
    addSubjects: (state, subjects: Array<Subject>) => {
        state.all = subjects;
    },

    addSubject: (state, subject: Subject) => {
        state.all.push(subject);
    }
};

const actions: ActionTree<SubjectsState, RootState> = {
    fetchSubjects: async (state, rootstate) => {
        state.commit('addSubjects', await state.state.api.getAllSubjects());
    },
    
    fetchSubject: async (state, id) => {
        state.commit("addSubject", await state.state.api.getSubject(id));
    },
};

export class SubjectsModule implements Module<SubjectsState, RootState> {

    namespaced: boolean;
    state: SubjectsState;
    actions: ActionTree<SubjectsState, RootState>;
    mutations: MutationTree<SubjectsState>;
    getters: GetterTree<SubjectsState, RootState>;

    constructor() {
        this.namespaced = true;
        this.state = new SubjectsState();
        this.actions = actions;
        this.mutations = mutations;
        this.getters = getters;
    }
   

}