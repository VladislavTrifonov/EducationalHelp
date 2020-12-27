import Vue from 'vue';
import {Component, Prop} from 'vue-property-decorator'

@Component({
    name: 'BreadcrumbsComponent'
})

export default class BreadcrumbsComponent extends Vue {
    constructor() {
        super();
    }

    @Prop()
    public breadcrumbs!: Array<IBreadcrumb>;
}

export interface IBreadcrumb {
    to: string | Object;
    title: string;
    link: boolean;
}
