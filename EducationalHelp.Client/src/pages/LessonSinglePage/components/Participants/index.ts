import Vue from 'vue';
import {Component, Prop} from 'vue-property-decorator';
import User from "@/api/models/User";
import {Response} from "@/store/modules/ErrorProcessing";
import LessonAPI from "@/api/LessonAPI";
import Lesson from "@/api/models/Lesson";

@Component({})
export default class Participants extends Vue {

    @Prop()
    private participants!: Array<User>;

    private possibleParticipants: Array<User>;

    @Prop()
    private lesson!: Lesson;

    private lessonApi: LessonAPI;
    constructor() {
        super();
        this.lessonApi = new LessonAPI();
        this.possibleParticipants = new Array<User>();
    }

    fetchPossibleParticipants()
    {
        Response.fromPromise(this.lessonApi.getPossibleParticipants(this.lesson.id), participants => {
           this.possibleParticipants = participants;
        }).catch(error => {
            console.log(error);
        })
    }

    mounted() {
        this.fetchPossibleParticipants()
    }

    deleteParticipant(idx: number) {
        Response.fromPromise(this.lessonApi.removeParticipant(this.lesson.id, this.participants[idx].id), ok => {
            this.possibleParticipants.push(this.participants[idx]);
            this.participants.splice(idx, 1);
        });

    }

    addParticipant(idx: number) {
        Response.fromPromise(this.lessonApi.addParticipant(this.lesson.id, this.possibleParticipants[idx]), ok => {
            this.participants.push(this.possibleParticipants[idx]);
            this.possibleParticipants.splice(idx, 1);
        });
    }
}
