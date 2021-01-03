import UserAPI from '@/api/UserAPI';
import User from "@/api/models/User";
import AccessToken from "@/api/models/AccessToken";

export default class UserState {
    public api: UserAPI;

    public isAuthenticated: boolean;
    public accessToken: AccessToken;
    public user: User;
    public userDownloadedAvatar: Blob;

    constructor() {
        this.api = new UserAPI();
        this.accessToken = new AccessToken();
        this.isAuthenticated = false;
        this.user = new User();
        this.userDownloadedAvatar = new Blob();
    }

}
