export default class Lesson {
    id!: string;
    title!: string;
    label!: string;
    description!: string;
    dateStart?: string;
    dateEnd?: string;
    isVisited!: boolean;
    selfMark!: number;
    homework!: string;
    notes!: string;
    subjectId!: string;

    createdAt!: Date;
    updatedAt!: Date;
    deletedAt!: Date;
    static Empty: Lesson = new Lesson();
    
    constructor() {

    }
}

export enum Marks {
    None,
    Excellent,
    Good,
    Satisfactory,
    Unsatisfactory,
    Poor
}