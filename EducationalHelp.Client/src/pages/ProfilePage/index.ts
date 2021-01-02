import Vue from 'vue';

import {Component} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import User from "@/api/models/User";
import DateTime from '@/components/system/DateTime.vue'

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

    constructor() {
        super();
        this.isEditPseudonym = false;
        this.newPseudonym = "";
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
