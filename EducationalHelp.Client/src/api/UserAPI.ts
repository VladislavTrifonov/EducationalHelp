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

    updateProfile(user: User, avatar: File): Promise<User> {
        return new Promise((resolve, reject) => {
            let fd = new FormData();
            fd.append("pseudonym", user.pseudonym);
            if (avatar != null)
                fd.append("avatar", avatar);
            axios.put("api/profile/me", fd).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        })
    }

    downloadAvatar(downloadLink: string): Promise<any> {
        return new Promise((resolve, reject) => {
            axios.get(downloadLink, {
                responseType: "arraybuffer"
            }).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        })
    }
}
