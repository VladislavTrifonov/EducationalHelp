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
    },

    getAvatarBlob: state => {
        return state.userDownloadedAvatar;
    }
};

const mutations: MutationTree<UserState> = {
    login: (state, token: AccessToken) => {
        state.accessToken = token;
        state.isAuthenticated = true;
    },

    logout: (state) => {
      state.accessToken = new AccessToken();
      state.isAuthenticated = false;
      state.user = new User();
    },

    setProfileInformation: (state, user: User) => {
        state.user = user;
    },

    setPseudonym: (state, pseudonym: string) => {
        state.user.pseudonym = pseudonym;
    },

    updateDownloadedAvatar: (state, data) => {
        state.userDownloadedAvatar = new Blob([data], { type: 'image/png' });
    }
};

// @ts-ignore
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
            state.dispatch("downloadUserAvatar", user);
        });
    },

    register: (state, credentials: UserRegisterInfo) => {
        return Response.fromPromise(state.state.api.register(credentials), token => {
            state.commit("login", token);
            state.dispatch("loadProfileInformation");

        });
    },
    updateProfile: (state: any, data: { user: User, avatar: File }) => {
        return Response.fromPromise(state.state.api.updateProfile(data.user, data.avatar), user => {
            state.commit("setProfileInformation", user);
            if (data.avatar != null) {
                state.commit("updateDownloadedAvatar", data.avatar);
                console.log(data.avatar)
            }
        });
    },

    downloadUserAvatar: (state, user: User) => {
        return Response.fromPromise(state.state.api.downloadAvatar(user.avatarLink), data => {
           state.commit("updateDownloadedAvatar", data)
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
