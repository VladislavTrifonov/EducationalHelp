import UserAPI from '@/api/UserAPI';
import User from "@/api/models/User";
import AccessToken from "@/api/models/AccessToken";
import UserStatistics from "@/api/models/UserStatistics";
import Group from "@/api/models/Group";

export default class UserState {
    public api: UserAPI;

    public isAuthenticated: boolean;
    public accessToken: AccessToken;
    public user: User;
    public userDownloadedAvatar: Blob;
    public userStatistics: UserStatistics;
    public currentGroup: Group;
    public groups: Array<Group>;

    constructor() {
        this.api = new UserAPI();
        this.accessToken = new AccessToken();
        this.isAuthenticated = false;
        this.user = new User();
        this.userDownloadedAvatar = new Blob();
        this.userStatistics = new UserStatistics();
        this.currentGroup = new Group();
        this.groups = new Array<Group>();
    }

}
