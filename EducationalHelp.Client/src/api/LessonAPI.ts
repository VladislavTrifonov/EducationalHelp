import Lesson from './models/Lesson';
import axios from '@/axiosconf';
import Subject from "@/api/models/Subject";

export default class LessonAPI {
    constructor() {

    }

    async getAllLessons(subjectId:string): Promise<Array<Lesson>> {
        let lessons = new Array<Lesson>();
        await axios.get("api/subjects/"+ subjectId +"/lessons")
            .then(response => {
                lessons = response.data;
            })
            .catch(error => {
            });
        return lessons;
    }

    getLessonById(subjectId: string, lessonId: string): Promise<Lesson> {
        return new Promise<Lesson>((resolve, reject) => {
          axios.get("api/subjects/" + subjectId + "/lessons/" + lessonId)
              .then(response => {
                  resolve(response.data);
              })
              .catch((error) => {
                  reject(error);
              });
        });
    }

    updateLesson(subjectId: string, lessonId: string, lesson: Lesson): Promise<Lesson> {
        return new Promise<Lesson>((resolve, reject) => {
           axios.put("api/subjects/" + subjectId + "/lessons/" + lessonId, lesson)
               .then(response => {
                   resolve(response.data);
               })
               .catch(error => {
                   reject(error);
               });
        });
    }

    createLesson(subjectId: string, lesson: Lesson): Promise<Lesson> {
        return new Promise<Lesson>((resolve, reject) => {
            axios.post("api/subjects/" + subjectId + "/lessons", lesson)
                .then(response => resolve(response.data))
                .catch(error => reject(error));
        })
    }

    deleteLesson(subjectId: string, lessonId: string): Promise<any> {
        return new Promise((resolve, reject) => {
            axios.delete('api/subjects/' + subjectId + '/lessons/' + lessonId).then(response => {
                resolve(response.data)
            }).catch(error => reject(error));
        });
    }
}