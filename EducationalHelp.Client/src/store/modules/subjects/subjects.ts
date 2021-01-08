import { GetterTree } from 'vuex';
import { MutationTree } from 'vuex';
import { ActionTree } from 'vuex';
import { Module } from 'vuex';
import { Response } from '../ErrorProcessing';
import SubjectsState from './state';
import RootState from '../../rootstate';
import Subject from '../../../api/models/Subject';
import SubjectsAPI from "@/api/SubjectsAPI";


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
    }, 

    deleteSubject: (state, subject: Subject) => {
        let idx = state.all.findIndex(i => i.id == subject.id);
        state.all.splice(idx, 1);
    }
};

const actions: ActionTree<SubjectsState, RootState> = {
    fetchSubjects: (state, rootstate) => {
        return Response.fromPromise(SubjectsAPI.getAllSubjects(state.rootGetters['user/getCurrentGroup'].id), result => {
            state.commit("addSubjects", result);
        });
    },
    
    fetchSubject: (state, id) => {
        return Response.fromPromise(SubjectsAPI.getSubject(id),
            response => {
                state.commit("addSubject", response);
        });
    },

    addSubject: (state, subject) => {
        return Response.fromPromise(SubjectsAPI.addSubject(subject), response => {
            state.commit("addSubject", response);
        });
    },

    updateSubject: (state, subject: Subject) => {
        return Response.fromPromise(SubjectsAPI.updateSubject(subject, subject.id), response => {
            state.commit("deleteSubject", subject);
            state.commit("addSubject", response);
        });
    },

    deleteSubject: (state, subject: Subject) => {
        return Response.fromPromise(SubjectsAPI.deleteSubject(subject.id), response => {
            state.commit("deleteSubject", subject);
        });
    }
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
