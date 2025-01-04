import { Component } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { UserLoginModel } from "../../model/user-login-model";
import { UserService } from "../../service/user-service";
import { Router } from "@angular/router";

@Component({
    imports: [ReactiveFormsModule],

    selector: 'login-page',
    templateUrl: './login-page.component.html',
    styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
    constructor(private userService: UserService, private router: Router) { }

    loginForm = new FormGroup({
        username: new FormControl(''),
        password: new FormControl('')
    });

    login() {
        let request: UserLoginModel = {
            username: this.loginForm.value.username as string,
            password: this.loginForm.value.password as string
        };

        this.userService.postLogin(request).subscribe((response) => {
            if (response.success) {
                sessionStorage.setItem('jwt', response.data);
                this.router.navigate(['profile']);
            } else {
                alert(response.errorMessage);
            }
        });
    }
}
