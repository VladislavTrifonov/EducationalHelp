import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import {mapGetters} from "vuex";

@Component({
    components: {

    },
    computed: {
        ...mapGetters('user', {
            isAuthenticated: 'isAuthenticated',
            user: 'getProfileInformation'
        })
    }
})
export default class Navigation extends Vue {
    isAuthenticated!: any;
    user!: any;

    logOut(e: any) {
        e.preventDefault();
        this.$store.commit("user/logout");
        this.$router.push({
           name: 'homePage'
        });
    }
}
