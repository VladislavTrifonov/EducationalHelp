import {ActionTree, GetterTree, Module, MutationTree} from "vuex";
import RootState from "@/store/rootstate";
import UserState from "@/store/modules/user/state";
import { Response } from '../ErrorProcessing';
import AccessToken from "@/api/models/AccessToken";


const getters: GetterTree<UserState, RootState> = {
    getAccessToken: (state) => {
        return state.accessToken;
    }
};

const mutations: MutationTree<UserState> = {
    login: (state, token: AccessToken) => {
        state.accessToken = token;
    }
};

const actions: ActionTree<UserState, RootState> = {
    authorize: (state, credentials) => {
        return Response.fromPromise(state.state.api.getAccessToken(credentials), token => {
            state.commit("login", token);
        });
    }
};

export class UserModule implements Module<UserState, RootState> {
    namespaced: boolean;
    state: UserState;
    actions: ActionTree<UserState, RootState>;
    mutations: MutationTree<UserState>;
    getters: GetterTree<UserState, RootState>;

    constructor() {
        this.namespaced = true;
        this.state = new UserState();
        this.actions = actions;
        this.mutations = mutations;
        this.getters = getters;
    }
}
