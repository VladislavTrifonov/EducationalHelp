import Vue from 'vue';
import {Component, Prop, Watch} from 'vue-property-decorator';
import {mapGetters} from "vuex";
import Group from "@/api/models/Group";
import GroupSelectForm from "@/components/GroupSelectForm/index.vue";


@Component({
    components: {
      'group-select': GroupSelectForm
    },
    computed: {
        ...mapGetters("user", {
            currentGroup: "getCurrentGroup",
        })
    }
})
export default class GroupViewInfoComponent extends Vue {

    private currentGroup!: Group;


    constructor() {
        super();
    }


}
