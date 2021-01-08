import Vue from 'vue';
import {Component, Prop, Watch} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import Group from "@/api/models/Group";


@Component({
    computed: {
        ...mapGetters("user", {
            currentGroup: "getCurrentGroup",
            listOfGroups: "getListOfGroups"
        })
    }
})
export default class GroupViewInfoComponent extends Vue {

    private currentGroup!: Group;
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
