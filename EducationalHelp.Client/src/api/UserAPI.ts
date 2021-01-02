import AccessToken from './models/AccessToken';
import axios from '@/axiosconf'
import UserCredentials from "@/api/models/UserCredentials";
import User from "@/api/models/User";
import UserRegisterInfo from "@/api/models/UserRegisterInfo";

export default class UserAPI {
    constructor() {
    }

    getAccessToken(credentials: UserCredentials): Promise<AccessToken> {
        return new Promise<AccessToken>((resolve, reject) => {
           axios.post("api/auth/token", credentials).then(response => {
               resolve(response.data);
           }).catch(error => {
               reject(error);
           });
        });
    }

    getProfileInformation(): Promise<User> {
        return new Promise<User>((resolve, reject) => {
           axios.get("api/profile/me").then(response => {
               resolve(response.data);
           }).catch(error => {
               reject(error);
           });
        });
    }

    register(credentials: UserRegisterInfo): Promise<AccessToken> {
        return new Promise((resolve, reject) => {
            axios.post("api/auth/register", credentials).then(response => {
                resolve(response.data)
            }).catch(error => {
                reject(error)
            })
        });
    }

    updateProfile(user: User): Promise<User> {
        return new Promise((resolve, reject) => {
            axios.put("api/profile/me", user).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        })
    }
}
