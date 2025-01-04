import { Component } from "@angular/core";
import { ContactService } from "../../service/contact-service";
import { PhoneModel } from "../../model/phone-model";
import { ActivatedRoute } from "@angular/router";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { NgFor } from "@angular/common";
import { PhoneTypeModel } from "../../model/phone-type-model";

@Component({
    imports: [ReactiveFormsModule, NgFor],

    selector: 'phone-page',
    templateUrl: 'phone-page.component.html',
    styleUrl: 'phone-page.component.css'
})
export class PhonePageComponent {
    constructor(private contactService: ContactService, private route: ActivatedRoute) { }

    loading = true;

    contactId = 0;
    phoneTypes: PhoneTypeModel[] = [];
    phones: PhoneModel[] = [];

    ngOnInit() {
        this.contactId = parseInt(this.route.snapshot.paramMap.get('contactid') ?? '0');

        this.contactService.getPhoneTypes().subscribe(response => {
            this.phoneTypes = response.data;
        });

        this.contactService.getPhones(this.contactId).subscribe(response => {
            this.phones = response.data;

            this.loading = false;
        });
    }

    url = "";

    addPhoneForm = new FormGroup({
        phoneTypeId: new FormControl(''),
        number: new FormControl('')
    });

    addPhone() {
        let request: PhoneModel = {
            id: 0,
            contactId: this.contactId,
            phoneTypeId: parseInt(this.addPhoneForm.value.phoneTypeId ?? ''),
            number: this.addPhoneForm.value.number as string
        };

        this.contactService.addPhone(request).subscribe((response) => {
            this.ngOnInit();
        });
    }
}
