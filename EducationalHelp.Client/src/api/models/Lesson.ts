export default class Lesson {
    title: string;
    label: string;
    description: string;
    dateStart?: Date;
    dateEnd?: Date;
    isVisited: boolean;
    selfMark: number;
    homework: string;
    notes: string;
    subjectId: string;

    createdAt!: Date;
    updatedAt!: Date;
    deletedAt!: Date;


    constructor() {
        this.title = this.label = this.description = this.homework = this.notes = this.subjectId = "";
        this.isVisited = false;
        this.selfMark = 0;
    }
}