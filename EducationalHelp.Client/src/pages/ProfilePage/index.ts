import Vue from 'vue';

import {Component, Watch} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import User from "@/api/models/User";
import DateTime from '@/components/system/DateTime.vue'
import UserAPI from "@/api/UserAPI";
import UserStatistics from "@/api/models/UserStatistics";

@Component({
    components: {
      'date-time': DateTime
    },
    computed: {
        ...mapGetters('user', {
            user: 'getProfileInformation',
            avatar: 'getAvatarBlob',
            userStatistics: 'getUserStatistics'
        })
    }
})
export default class ProfilePage extends Vue {

    private user!: User;

    private isEditPseudonym: boolean;
    private newPseudonym: string;
    private avatar!: Blob;
    private userStatistics!: UserStatistics;

    constructor() {
        super();
        this.isEditPseudonym = false;
        this.newPseudonym = "";
    }

    get urlOfImage(): string {
        return URL.createObjectURL(this.avatar);
    }

    mounted() {
        this.$store.dispatch("user/loadProfileInformation");
    }

    setAvatar(event: any) {
        let file = event.target.files[0];
        if (file == null)
            return;

       this.$store.dispatch("user/updateProfile", {
           user: this.user,
           avatar: file
       }).catch(error => {
            console.log(error);
        });
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
        this.$store.dispatch("user/updateProfile", {
            user: this.user,
            avatar: null
        }).then(response => {
            console.log("Данные обновлены!", response);
        });
    }

}
