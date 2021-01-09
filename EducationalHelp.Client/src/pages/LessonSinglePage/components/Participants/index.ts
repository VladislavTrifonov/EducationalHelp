import Vue from 'vue';
import {Component, Prop} from 'vue-property-decorator';
import User from "@/api/models/User";

@Component({})
export default class Participants extends Vue {

    @Prop()
    private participants!: Array<User>;

    constructor() {
        super();
    }
}
