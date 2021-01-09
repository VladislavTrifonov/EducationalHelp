import Lesson, {Marks} from './models/Lesson';
import axios from '@/axiosconf';
import LessonParticipant from "@/api/models/LessonParticipant";
import User from "@/api/models/User";

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

    getLessonById(lessonId: string): Promise<Lesson> {
        return new Promise<Lesson>((resolve, reject) => {
          axios.get("api/subjects/lessons/" + lessonId)
              .then(response => {
                  resolve(response.data);
              })
              .catch((error) => {
                  reject(error);
              });
        });
    }

    updateLesson(lessonId: string, lesson: Lesson): Promise<Lesson> {
        return new Promise<Lesson>((resolve, reject) => {
           axios.put("api/subjects/lessons/" + lessonId, lesson)
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

    deleteLesson(lessonId: string): Promise<any> {
        return new Promise((resolve, reject) => {
            axios.delete('api/subjects/lessons/' + lessonId).then(response => {
                resolve(response.data)
            }).catch(error => reject(error));
        });
    }

    getListOfFiles(lessonId: string): Promise<Array<File>> {
        return new Promise((resolve, reject) => {
            axios.get("api/subjects/lessons/" + lessonId + "/files").then(response => {
                resolve(response.data)
            }).catch(error => {
                reject(error)
            })
        })
    }

    uploadLessonFiles(lessonId: string, files: Array<File>): Promise<Array<File>> {
        return new Promise((resolve, reject) => {
           let fd = new FormData();
           for (let i = 0; i < files.length; i++) {
               fd.append("files", files[i]);
           }
           axios.post("api/subjects/lessons/" + lessonId + "/files", fd).then(response => {
               resolve(response.data)
           }).catch(error => {
               reject(error)
           })

        });
    }

    getLessonParticipants(lessonId: string): Promise<Array<LessonParticipant>> {
        return new Promise((resolve, reject) => {
            axios.get("api/subjects/lessons/" + lessonId + "/participants").then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        });
    }

    updateParticipant(lessonId: string, participant: LessonParticipant): Promise<any> {
        return new Promise((resolve, reject) => {
           axios.put("api/subjects/lessons/" + lessonId + "/participants", participant).then(response => {
               resolve(response.data);
           }).catch(error => {
               reject(error);
           });
        });
    }

    removeParticipant(lessonId: string, userId: string): Promise<any> {
        return new Promise((resolve, reject) => {
           axios.delete("api/subjects/lessons/" + lessonId + "/participants/" + userId).then(response => {
               resolve(response.data);
           }).catch(error => {
               reject(error);
           });
        });
    }

    addParticipant(lessonId: string, user: User): Promise<any> {
        return new Promise((resolve, reject) => {
            let participant = {
                userId: user.id,
                isVisited: "false",
                mark: Marks.None
            } as LessonParticipant;

            axios.post("api/subjects/lessons/" + lessonId + "/participants", participant).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        });
    }

    getPossibleParticipants(lessonId: string): Promise<Array<User>> {
        return new Promise((resolve, reject) => {
            axios.get("api/subjects/lessons/" + lessonId + "/participants/possible").then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        });
    }
}
