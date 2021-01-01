import Vue from 'vue'
import {Component, Prop} from 'vue-property-decorator'
import UserCredentials from "@/api/models/UserCredentials";
import {RawLocation} from "vue-router";

@Component({
    name: 'LoginPage'
})
export default class LoginPage extends Vue {
    private loginForm!: UserCredentials;

    @Prop()
    private redirected!: boolean;

    @Prop()
    private redirectRoute!: Location;

    constructor() {
        super();
        this.loginForm = new UserCredentials();
    }

    onSubmit(e: any) {
        e.preventDefault()
        this.$store.dispatch("user/authorize", this.loginForm)
            .then(response => {
                // @ts-ignore
                this.$router.push(this.redirectRoute);
            })
            .catch((error:Response) => {
           console.log(error);
        });
    }

}
