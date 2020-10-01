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

    getSubject(guid: String): Promise<Subject> {
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

    addSubject(model: Subject): Promise<Subject> {
        return new Promise<Subject>((resolve, reject) => {
            axios.post("Subjects/CreateSubject", model, {
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

    updateSubject(model: Subject, id: string): Promise<Subject> {
        return new Promise<Subject>((resolve, reject) => {
            axios.put("Subjects/UpdateSubject", model, {
                params: {
                    'id': id
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

    deleteSubject(id: string): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            axios.delete("Subjects/DeleteSubject", {
                params: {
                    'id': id
                }
            })
            .then(response => {
                resolve();
            })
            .catch(error => {
                reject(error);
            });
        });
    }
}