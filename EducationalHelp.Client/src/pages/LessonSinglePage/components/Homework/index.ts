import Vue from 'vue';
import {Component} from 'vue-property-decorator';

@Component({})
export default class Homework extends Vue {
    public files: Array<File> = new Array<File>();

    constructor() {
        super();
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

    }

}