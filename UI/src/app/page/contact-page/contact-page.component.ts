import { Component, OnInit } from "@angular/core";
import { ContactService } from "../../service/contact-service";
import { ContactModel } from "../../model/contact-model";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { NgFor } from "@angular/common";
import { RouterLink } from "@angular/router";

@Component({
    imports: [ReactiveFormsModule, NgFor, RouterLink],

    selector: 'contact-page',
    templateUrl: 'contact-page.component.html',
    styleUrl: 'contact-page.component.css'
})
export class ContactPageComponent implements OnInit {
    constructor(private contactService: ContactService) { }

    contacts: ContactModel[] = [];

    ngOnInit() {
        this.contactService.getContacts().subscribe(response => {
            this.contacts = response.data;
        });
    }

    url = "";

    addContactForm = new FormGroup({
        fullname: new FormControl(''),
        email: new FormControl(''),
        avatar: new FormControl('')
    });

    show(event: any) {
        let file = event.target.files[0];

        let reader = new FileReader();

        reader.readAsDataURL(file);

        reader.onload = (e) => {
            if (e.target) {
                this.url = e.target.result as string;
            }
        };
    }

    addContact() {
        let request: ContactModel = {
            id: 0,
            fullname: this.addContactForm.value.fullname as string,
            email: this.addContactForm.value.email as string,
            avatar: this.url
        };

        this.contactService.addContact(request).subscribe((response) => {
            this.ngOnInit();
        });
    }
}
