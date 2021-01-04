export default class UserStatistics {
    public subjectsCount!: number;
    public avgMarkLessonAll!: number;
    public subjectsStatistics!: Array<subjectsStatistics>;
}

export class subjectsStatistics {
    public subjectTitle!: string;
    public lessonsCount!: number;
    public lessonsMissedCount!: number;
    public avgLessonsMark!: number;
}
