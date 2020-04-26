import Subject from './models/Subject';
import axios from 'axios';

export default class SubjectsAPI {
    constructor() {
            
    }

    

    async getAllSubjects(): Promise<Array<Subject>> {
        var subjects = new Array<Subject>();
        await axios.get("https://localhost:5001/Subjects/GetAllSubjects")
            .then(response => {
                subjects = response.data;
            })
            .catch(error => console.log(error));
        return subjects;
    }

    async getSubject(guid: String): Promise<Subject> {
        let subject = new Subject();
        await axios.get("https://localhost:5001/Subjects/GetSubjectById", {
            params: {
                id: guid
            }
        }).then(response => {
               subject = response.data;
          })
          .catch(error => console.log(error));
        return subject;
    }
}