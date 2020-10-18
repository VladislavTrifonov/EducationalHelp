import Vue from 'vue';
import { Component } from "vue-property-decorator";
import DateTime from '@/components/system/DateTime.vue'

@Component({
    components: {
      'date-time': DateTime
    },
    props: ["entity"]
})
export default class CreatedUpdatedInfo extends Vue {
    constructor() {
        super();
    }


}