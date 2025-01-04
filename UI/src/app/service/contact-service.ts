import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { enviroment } from "../environments/environment";

import { Result } from "../model/result";
import { ContactModel } from "../model/contact-model";
import { FavoriteModel } from "../model/favorite-model";
import { PhoneModel } from "../model/phone-model";
import { PhoneTypeModel } from "../model/phone-type-model";

@Injectable({ providedIn: 'root' })
export class ContactService {
    constructor(private http: HttpClient) { }

    addContact(request: ContactModel) {
        let url = `${enviroment.baseUrl}/contact/addcontact`;
        return this.http.post<Result<boolean>>(url, request);
    }

    addPhone(request: PhoneModel) {
        let url = `${enviroment.baseUrl}/contact/addphone`;
        return this.http.post<Result<boolean>>(url, request);
    }

    addFavorite(request: FavoriteModel) {
        let url = `${enviroment.baseUrl}/contact/addfavorite`;
        return this.http.post<Result<boolean>>(url, request);
    }

    getContact(request: Number) {
        let url = `${enviroment.baseUrl}/contact/getcontact?contactId=${request}`;
        return this.http.get<Result<ContactModel>>(url);
    }

    getContacts() {
        let url = `${enviroment.baseUrl}/contact/getcontacts`;
        return this.http.get<Result<ContactModel[]>>(url);
    }

    getPhones(request: Number) {
        let url = `${enviroment.baseUrl}/contact/getphones?request=${request}`;
        return this.http.get<Result<PhoneModel[]>>(url);
    }

    getPhoneTypes() {
        let url = `${enviroment.baseUrl}/contact/getphonetypes`;
        return this.http.get<Result<PhoneTypeModel[]>>(url);
    }

    removeContact(request: Number) {
        let url = `${enviroment.baseUrl}/contact/removecontact?request=${request}`;
        return this.http.delete<Result<boolean>>(url);
    }

    removePhone(request: Number) {
        let url = `${enviroment.baseUrl}/contact/removephone?request=${request}`;
        return this.http.delete<Result<boolean>>(url);
    }

    removeFavorite(request: Number) {
        let url = `${enviroment.baseUrl}/contact/removefavorite?request=${request}`;
        return this.http.delete<Result<boolean>>(url);
    }
}
