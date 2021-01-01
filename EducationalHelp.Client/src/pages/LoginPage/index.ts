import Vue from 'vue'
import {Component, Prop} from 'vue-property-decorator'
import UserCredentials from "@/api/models/UserCredentials";
import {RawLocation} from "vue-router";

@Component({
    name: 'LoginPage'
})
export default class LoginPage extends Vue {
    private loginForm: UserCredentials;
    private registerForm: {
        login: string,
        pseudonym: string,
        password: string,
        passwordRepeat: string
    };

    private isLogin: boolean;

    @Prop()
    private redirected!: boolean;

    @Prop()
    private redirectRoute!: Location;

    constructor() {
        super();
        this.isLogin = true;
        this.loginForm = new UserCredentials();
        this.registerForm = {
            login: "",
            pseudonym: "",
            password: "",
            passwordRepeat: ""
        };
    }

    onSubmit(e: any) {
        e.preventDefault();
        this.$store.dispatch("user/authorize", this.loginForm)
            .then(response => {
                if (this.redirected) {
                    // @ts-ignore
                    this.$router.push(this.redirectRoute);
                } else {
                    this.$router.push({
                        name: 'profileView'
                    });
                }

            })
            .catch((error:Response) => {
           console.log(error);
        });
    }

    onRegisterSubmit(e: any) {
        e.preventDefault();

    }

}
