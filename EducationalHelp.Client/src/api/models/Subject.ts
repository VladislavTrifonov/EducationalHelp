export default class Subject {
    id: string;
    name: string;
    description: string;
    teacher: string;

    createdAt!: Date;
    deletedAt!: Date;
    updatedAt!: Date;

    constructor() {
        this.id = this.name = this.description = this.teacher = "";
    }
}