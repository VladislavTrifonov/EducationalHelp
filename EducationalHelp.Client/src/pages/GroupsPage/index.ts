import Vue from 'vue';
import {Component} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import Group from "@/api/models/Group";
import GroupsAPI from "@/api/GroupsAPI";

@Component({
    computed: {
        ...mapGetters("user", {
            userGroups: "getListOfGroups"
        })
    }
})
export default class GroupsPage extends Vue {
    private userGroups!: Array<Group>;
    private allGroups: Array<Group>;

    constructor() {
        super();
        this.allGroups = new Array<Group>();
    }

    leave(idx: number) {
        this.$store.dispatch("user/leaveFromGroup", this.userGroups[idx].id).catch(error => {
           this.$bvToast.toast("Нельзя выйти из всех групп!");
        });
    }

    enter(idx: number) {

    }

    mounted()
    {
        this.$store.dispatch("user/getGroupInformation");
        GroupsAPI.getAllGroups().then(groups => {
           this.allGroups = groups;
        });
    }

}
