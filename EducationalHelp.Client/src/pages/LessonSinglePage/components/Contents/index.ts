import Vue from 'vue';
import {Component} from "vue-property-decorator";
// @ts-ignore
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
// @ts-ignore
import CKEditor from '@ckeditor/ckeditor5-vue';


@Component({
    components: {
        'ckeditor': CKEditor.component
    },
    props: ['lesson']
})
export default class Contents extends Vue {
    public editMark: boolean = false;
    public editor: any = ClassicEditor;

    constructor() {
        super();
    }

    setHomework() {
        this.editMark = !this.editMark;
    }
}