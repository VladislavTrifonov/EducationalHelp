import Subject from "@/api/models/Subject";
import SubjectsAPI from '@/api/SubjectsAPI';

export default class SubjectsState {
    private api: SubjectsAPI;
    all: Array<Subject>;

    constructor() {
        this.api = new SubjectsAPI();
        this.all = this.api.getAllSubjects();
    }

}