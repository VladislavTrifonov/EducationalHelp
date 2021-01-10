import Lesson from "@/api/models/Lesson";

export default class Subject {
    id: string;
    name: string;
    description: string;
    teacher: string;
    lessons!: Array<Lesson>;

    createdAt!: Date;
    deletedAt!: Date;
    updatedAt!: Date;

    constructor() {
        this.id = this.name = this.description = this.teacher = "";
    }
}
