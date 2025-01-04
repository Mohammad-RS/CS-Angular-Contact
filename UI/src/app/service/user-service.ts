import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Result } from "../model/result";
import { UserAddModel } from "../model/user-add-model";
import { UserLoginModel } from "../model/user-login-model";
import { UserProfileMode } from "../model/user-profile-model";

import { enviroment } from "../environments/environment";

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getProfile() {
        let url = `${enviroment.baseUrl}/user/profile`;
        return this.http.get<Result<UserProfileMode>>(url);
    }

    postLogin(request: UserLoginModel) {
        let url = `${enviroment.baseUrl}/user/login`;
        return this.http.post<Result<string>>(url, request);
    }
    
    postRegister(request: UserAddModel) {
        let url = `${enviroment.baseUrl}/user/register`;
        return this.http.post<Result<Number>>(url, request);
    }
}
