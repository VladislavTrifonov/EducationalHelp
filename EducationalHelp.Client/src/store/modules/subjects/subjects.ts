import { GetterTree } from 'vuex';
import { MutationTree } from 'vuex';
import { ActionTree } from 'vuex';
import { Module } from 'vuex';
import SubjectsState from './state';
import RootState from '../../rootstate';


const getters: GetterTree<SubjectsState, RootState> = {

};

const mutations: MutationTree<SubjectsState> = {

};

const actions: ActionTree<SubjectsState, RootState> = {

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