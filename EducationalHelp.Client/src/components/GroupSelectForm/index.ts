import Vue from 'vue';
import {Component, Watch} from 'vue-property-decorator';
import Group from "@/api/models/Group";
import {mapGetters} from "vuex";

@Component({
    computed: {
        ...mapGetters("user", {
            listOfGroups: "getListOfGroups"
        })
    }
})
export default class GroupSelectForm extends Vue {

    private listOfGroups!: Array<Group>;
    private selectedGroup: Group;

    constructor() {
        super();
        this.selectedGroup = new Group();
    }

    get groupOptions(): any[] {
        return this.listOfGroups.map<any>(((val, index, array) => {
            return {
                value: val,
                text: val.title
            }
        }));
    }

    @Watch("selectedGroup")
    onUpdateSelectedGroup(newValue: Group, oldValue: Group)
    {
        if (newValue != null)
        {
            this.$store.commit("user/setCurrentGroup", newValue);
        }
    }
}
