import { Component, OnInit } from "@angular/core";
import { UserService } from "../../service/user-service";

@Component({
    selector: 'profile-page',
    templateUrl: './profile-page.component.html',
    styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent implements OnInit {
    constructor(private userService: UserService) { }

    username = '';
    fullname = '';
    avatar = '';

    ngOnInit() {
        this.userService.getProfile().subscribe((response) => {
            this.username = response.data.username;
            this.fullname = response.data.fullname;
            this.avatar = response.data.avatar;
        });
    }
}
