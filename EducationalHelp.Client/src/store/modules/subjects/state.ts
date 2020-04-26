import Subject from "@/api/models/Subject";
import SubjectsAPI from '@/api/SubjectsAPI';

export default class SubjectsState {
    public api: SubjectsAPI;

    public all: Array<Subject> = new Array<Subject>();

    constructor() {
        this.api = new SubjectsAPI();
    }

}