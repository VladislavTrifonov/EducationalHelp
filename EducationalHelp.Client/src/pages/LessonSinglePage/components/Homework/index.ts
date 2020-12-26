import Vue from 'vue';
import {Component, Prop, Watch} from 'vue-property-decorator';
import DateTime from "@/components/system/DateTime.vue";
import Lesson from "@/api/models/Lesson";
import FileModel from "@/api/models/FileModel";
import {Response} from "@/store/modules/ErrorProcessing"
import LessonAPI from "@/api/LessonAPI";
import FileAPI from "@/api/FileAPI";

@Component({
    components: {
        'date-time': DateTime
    }
})
export default class Homework extends Vue {
    public files: Array<File> = new Array<File>();
    public lessonApi: LessonAPI;
    public fileApi: FileAPI;
    public btnUploadDisabled: boolean;

    @Prop({type: Object as () => Lesson})
    public lesson!: Lesson;

    @Prop()
    public lesson_files!: Array<FileModel>;
    constructor() {
        super();

        this.lessonApi = new LessonAPI();
        this.fileApi = new FileAPI();
        this.btnUploadDisabled = false;
    }

    formatNames(files: Array<File>) {
        return files.length === 1 ? files[0].name : `${files.length} файлов было выбрано`
    }

    get fileSumSize(): string {
        let fileSize = 0;
        this.files.forEach(file => {
            fileSize += file.size;
        });

        return this.getFileSize(fileSize);
    }

    getFileSize(size: number): string {
        let kbytes = size / 1024;
        if (kbytes < 1024) {
            return Math.ceil(kbytes).toString() + ' KBytes';
        }

        let mbytes = kbytes / 1024;
        return Math.ceil(mbytes).toString() + ' MBytes';
    }

    deleteFile(index: number) {
        this.files.splice(index, 1);
    }

    uploadFiles() {
        this.btnUploadDisabled = true;
        let promise = Response.fromPromise(this.lessonApi.uploadLessonFiles(this.lesson.id, this.files), (response) => {
            this.$emit('reload-files-needed')
            this.files = new Array<File>();
            this.btnUploadDisabled = false;
        }).catch(error => {
            console.log(error);
            this.btnUploadDisabled = false;
        })
    }

    removeFile(id: string) {
        this.$bvModal.msgBoxConfirm('Вы действительно хотите удалить этот элемент?', {
            title: 'Необходимо подтверждение',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'danger',
            okTitle: 'Да',
            cancelTitle: 'Нет',
            footerClass: 'p-2',
            hideHeaderClose: false,
            centered: true
        })
        .then(value => {
            if (value) {
                Response.fromPromise(this.fileApi.deleteFile(id), (response) => {
                    this.$emit('reload-files-needed')
                    console.log(response)
                }).catch(error => {
                    console.log(error)
                })
            }
        });
    }

}
