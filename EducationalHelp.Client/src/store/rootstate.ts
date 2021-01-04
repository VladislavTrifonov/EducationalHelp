import UserState from "@/store/modules/user/state";
import SubjectsState from "@/store/modules/subjects/state";

export default class State {

    user: UserState;
    subjects: SubjectsState;

    constructor() {
        this.user = new UserState();
        this.subjects = new SubjectsState();
    }

}
