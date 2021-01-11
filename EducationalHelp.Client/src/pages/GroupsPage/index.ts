import Vue from 'vue';
import {Component} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import Group from "@/api/models/Group";

@Component({
    computed: {
        ...mapGetters("user", {
            userGroups: "getListOfGroups"
        })
    }
})
export default class GroupsPage extends Vue {
    private userGroups!: Array<Group>;

    constructor() {
        super();
    }

    leave(idx: number) {
        this.$store.dispatch("user/leaveFromGroup", this.userGroups[idx].id).catch(error => {
           this.$bvToast.toast("Нельзя выйти из всех групп!");
        });
    }

    mounted()
    {
        this.$store.dispatch("user/getGroupInformation");
    }

}
