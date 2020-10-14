import Lesson from './models/Lesson';
import axios from '@/axiosconf';
import Subject from "@/api/models/Subject";

export default class LessonAPI {
    constructor() {

    }

    async getAllLessons(subjectId:string): Promise<Array<Lesson>> {
        var lessons = new Array<Lesson>();
        await axios.get("api/subjects/"+ subjectId +"/lessons")
            .then(response => {
                lessons = response.data;
            })
            .catch(error => {
            });
        return lessons;
    }
}