import Subject from './models/Subject';
import axios from '@/axiosconf';

export default class SubjectsAPI {
    constructor() {
            
    }

    static async getAllSubjects(): Promise<Array<Subject>> {
        var subjects = new Array<Subject>();
        await axios.get("api/subjects")
            .then(response => {
                subjects = response.data;
            })
            .catch(error => {
            });
        return subjects;
    }

    static getSubject(guid: String): Promise<Subject> {
        return new Promise<Subject>((resolve, reject) => {
            axios.get("api/subjects/" + guid).then(response => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            })
        });
    }

    static addSubject(model: Subject): Promise<Subject> {
        return new Promise<Subject>((resolve, reject) => {
            axios.post("api/subjects", model, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                resolve(response.data);
            })
            .catch(error => {
                reject(error);
            });
                
        });
    }

    static updateSubject(model: Subject, id: string): Promise<Subject> {
        return new Promise<Subject>((resolve, reject) => {
            axios.put("api/subjects/" + id, model)
            .then(response => {
                resolve(response.data);
            })
            .catch(error => {
                reject(error);
            });
        });
    }

    static deleteSubject(id: string): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            axios.delete("api/subjects/" + id)
            .then(response => {
                resolve();
            })
            .catch(error => {
                reject(error);
            });
        });
    }
}
