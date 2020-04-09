export default class Subject {
    id: string;
    name: string;
    description: string;
    teacher: string;

    constructor() {
        this.id = this.name = this.description = this.teacher = "";
    }
}