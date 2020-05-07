import Subject from './models/Subject';
import axios from '@/axiosconf';

export default class SubjectsAPI {
    constructor() {
            
    }

    

    async getAllSubjects(): Promise<Array<Subject>> {
        var subjects = new Array<Subject>();
        await axios.get("Subjects/GetAllSubjects")
            .then(response => {
                subjects = response.data;
            })
            .catch(error => {
            });
        return subjects;
    }

    async getSubject(guid: String): Promise<Subject> {
        return new Promise<Subject>((resolve, reject) => {
            axios.get("Subjects/GetSubjectById", {
                params: {
                    id: guid
                }
            }).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        });
    }
}