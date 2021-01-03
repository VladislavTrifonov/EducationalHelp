import Vue from 'vue';

import {Component, Watch} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import User from "@/api/models/User";
import DateTime from '@/components/system/DateTime.vue'
import UserAPI from "@/api/UserAPI";

@Component({
    components: {
      'date-time': DateTime
    },
    computed: {
        ...mapGetters('user', {
            user: 'getProfileInformation'
        })
    }
})
export default class ProfilePage extends Vue {

    private user!: User;

    private isEditPseudonym: boolean;
    private newPseudonym: string;
    private imageBlob: Blob;
    private userApi: UserAPI;

    constructor() {
        super();
        this.isEditPseudonym = false;
        this.newPseudonym = "";
        this.imageBlob = new Blob();
        this.userApi = new UserAPI();
    }

    get urlOfImage(): string {
        return URL.createObjectURL(this.imageBlob);
    }

    @Watch("user.avatarLink")
    whenAvatarFilled(newValue: any, oldValue: any)
    {
        if (newValue != null) {
            this.userApi.downloadAvatar(newValue).then(data => {
                this.imageBlob = new Blob([data], { type: 'image/png' });
            })
        }
    }

    onClickSavePseudonym(e: any) {
        this.isEditPseudonym = false;
        if (this.newPseudonym == this.user.pseudonym)
            return;
        this.$store.commit("user/setPseudonym", this.newPseudonym);

        this.updateAccountData()
    }

    editPseudonym() {
        this.isEditPseudonym = true;
        this.newPseudonym = this.user.pseudonym;
    }

    updateAccountData() {
        this.$store.dispatch("user/updateProfile", this.user).then(response => {
            console.log("Данные обновлены!", response);
        });
    }

}
