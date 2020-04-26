import Subject from './models/Subject';
import axios from 'axios';

export default class SubjectsAPI {
    fakeSubjects: Array<Subject> = [
        {
            id: "FE3E3D54-E538-47AF-914B-F7B621F395F5",
            name: "Алгоритмы и алгоритмические языки",
            description: "алгоритмы",
            teacher: "Горюнов Ю.Ю."
        },
        {
            id: "7623BD52-72F5-42D1-BF85-11A62D9B19A7",
            name: "Математический анализ (1 семестр)",
            description: "матан",
            teacher: "Васюнина О.Б."
        },
        {
            id: "30100ECF-741C-4B64-B842-6EAEF3D60B2B",
            name: "Математический анализ (2 семестр)",
            description: "матан",
            teacher: "Хорошева Э.А."
        },
        {
            id: "8E76B156-CA17-48EC-9D9F-FF9ED066F41D",
            name: "Программирование (1 семестр)",
            description: "прогр-е",
            teacher: "Шибанов С.В."
        },
        {
            id: "5EE8DB95-8284-40EE-9D36-8E172FD9B1FD",
            name: "Программирование (2 семестр)",
            description: "прогр-е",
            teacher: "Шибанов С.В."
        }
    ];

    constructor() {
            
    }

    async getFakeSubjects(): Promise<Array<Subject>> {
        let promise = new Promise<Array<Subject>>((resolve, reject) => {
            resolve(this.fakeSubjects);
        });
        return promise;
    }

    async getFakeSubject(id: String): Promise<Subject> {
        let subject = new Subject();
        this.fakeSubjects.forEach(s => {
            if (s.id == id) {
                subject = s;
            }
        });
        return new Promise<Subject>((resolve) => resolve(subject));
    }

    async getAllSubjects(): Promise<Array<Subject>> {
        var subjects = new Array<Subject>();
        /*await axios.get("https://localhost:5001/Subjects/GetAllSubjects")
            .then(response => {
                subjects = response.data;
            })
            .catch(error => console.log(error));*/
        return await this.getFakeSubjects();
    }

    async getSubject(guid: String): Promise<Subject> {
        let subject = new Subject();
        /* await axios.get("https://localhost:5001/Subjects/GetSubjectById", params: {
         *      id: guid
         * }).then(response => {
         *      subject = response.data;
         * })
         * .catch(error => console.log(error));*/
        return await this.getFakeSubject(guid);
    }
}