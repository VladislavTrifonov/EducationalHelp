import {Marks} from "@/api/models/Lesson";

export default class LessonParticipant {
    public userId!: string;
    public isVisited!: string;
    public mark!: Marks;
}
