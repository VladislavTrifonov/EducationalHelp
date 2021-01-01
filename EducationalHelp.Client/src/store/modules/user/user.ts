import {ActionTree, GetterTree, Module, MutationTree} from "vuex";
import RootState from "@/store/rootstate";
import UserState from "@/store/modules/user/state";
import { Response } from '../ErrorProcessing';
import AccessToken from "@/api/models/AccessToken";
import User from "@/api/models/User";
import UserRegisterInfo from "@/api/models/UserRegisterInfo";


const getters: GetterTree<UserState, RootState> = {
    getAccessToken: state => {
        return state.accessToken;
    },
    getProfileInformation: state => {
        return state.user;
    },
    isAuthenticated: state => {
        return state.isAuthenticated;
    }
};

const mutations: MutationTree<UserState> = {
    login: (state, token: AccessToken) => {
        state.accessToken = token;
        state.isAuthenticated = true;
    },

    setProfileInformation: (state, user: User) => {
        state.user = user;
    }
};

const actions: ActionTree<UserState, RootState> = {
    authorize: (state, credentials) => {
        return Response.fromPromise(state.state.api.getAccessToken(credentials), token => {
            state.commit("login", token);
            state.dispatch("loadProfileInformation");
        });
    },

    loadProfileInformation: (state) => {
        return Response.fromPromise(state.state.api.getProfileInformation(), user => {
            state.commit("setProfileInformation", user);
        });
    },

    register: (state, credentials: UserRegisterInfo) => {
        return Response.fromPromise(state.state.api.register(credentials), token => {
            state.commit("login", token);
            state.dispatch("loadProfileInformation");
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
