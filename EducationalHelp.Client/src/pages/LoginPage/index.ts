import Vue from 'vue'
import {Component} from 'vue-property-decorator'
import UserCredentials from "@/api/models/UserCredentials";

@Component({
    name: 'LoginPage'
})
export default class LoginPage extends Vue {
    private loginForm!: UserCredentials;

    constructor() {
        super();
        this.loginForm = new UserCredentials();
    }

    onSubmit(e: any) {
        e.preventDefault()
        this.$store.dispatch("user/authorize", this.loginForm)
            .then(response => {
            })
            .catch((error:Response) => {
           console.log(error);
        });
    }

}
