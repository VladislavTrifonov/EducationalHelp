import Vue from 'vue';
import {Component, Prop, Watch} from "vue-property-decorator";
// @ts-ignore
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
// @ts-ignore
import CKEditor from '@ckeditor/ckeditor5-vue';
import Lesson from "@/api/models/Lesson";


@Component({
    components: {
        'ckeditor': CKEditor.component
    },
})
export default class Contents extends Vue {
    public editMark!: boolean;
    public editor: any = ClassicEditor;

    @Prop({type: Object as () => Lesson})
    public lesson!: Lesson;

    @Prop({
        default: () => false
    })
    public editing!: boolean;

    constructor() {
        super();
        this.editMark = this.editing;
    }

    @Watch('editing')
    updateEditMark() {
        this.editMark = this.editing
    }

    setHomework() {
        this.editMark = !this.editMark;
    }
}