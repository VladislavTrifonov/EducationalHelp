import Vue from 'vue';
import {Component, Prop, Watch} from 'vue-property-decorator';
import CreatedUpdatedInfo from "@/components/CreatedUpdatedInfo/index.vue";

// @ts-ignore
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
// @ts-ignore
import CKEditor from '@ckeditor/ckeditor5-vue';
import Lesson from "@/api/models/Lesson";
import DateTime from "@/components/system/DateTime.vue";


@Component({
    components: {
      'created-updated-info': CreatedUpdatedInfo,
      'ckeditor': CKEditor.component,
      'date-time': DateTime
    }
})
export default class LessonHeader extends Vue {
    public headEditing: boolean = false;
    public editor: any = ClassicEditor;

    @Prop({type: Object as () => Lesson})
    public lesson!: Lesson;

    public startDate: string = '';
    public startTime: string = '';
    public endDate: string = '';
    public endTime: string = '';

    constructor() {
        super();
    }

    // Необходимо использовать watch за входящ. параметром lesson, т.к. он приходит асинхронно (не в момент загрузки компонента)
    @Watch("lesson")
    initalizeDateAndTime() {
        // @ts-ignore
        let splittedDateStart = this.lesson.dateStart.split("T");
        this.startDate = splittedDateStart[0];
        this.startTime = splittedDateStart[1];
        // @ts-ignore
        let splittedDateEnd = this.lesson.dateEnd.split("T");
        this.endDate = splittedDateEnd[0];
        this.endTime = splittedDateEnd[1];
    }

    @Watch("startDate")
    @Watch("startTime")
    updateDateStart() {
        this.$props.lesson.dateStart = this.startDate + "T" + this.startTime;
    }

    @Watch("endDate")
    @Watch("endTime")
    updateDateEnd() {
        this.$props.lesson.dateEnd = this.endDate + "T" + this.endTime;
    }
}