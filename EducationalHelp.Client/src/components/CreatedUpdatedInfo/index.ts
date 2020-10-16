import Vue from 'vue';
import { Component } from "vue-property-decorator";

@Component({
    props: ["entity"]
})
export default class CreatedUpdatedInfo extends Vue {
    constructor() {
        super();
    }


}