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
}
