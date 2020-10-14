export default class Lesson {
    id!: string;
    title!: string;
    label!: string;
    description!: string;
    dateStart?: Date;
    dateEnd?: Date;
    isVisited!: boolean;
    selfMark!: number;
    homework!: string;
    notes!: string;
    subjectId!: string;

    createdAt!: Date;
    updatedAt!: Date;
    deletedAt!: Date;


    constructor() {

    }
}