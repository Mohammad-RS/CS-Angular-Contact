import { Routes } from '@angular/router';
import { LoginPageComponent } from './page/login-page/login-page.component';
import { ProfilePageComponent } from './page/profile-page/profile-page.component';
import { RegisterPageComponent } from './page/register-page/register-page.component';
import { ContactPageComponent } from './page/contact-page/contact-page.component';
import { PhonePageComponent } from './page/phone-page/phone-page.component';

export const routes: Routes = [
    { path: 'login', component: LoginPageComponent },
    { path: 'profile', component: ProfilePageComponent },
    { path: 'register', component: RegisterPageComponent },
    { path: 'contact', component: ContactPageComponent },
    { path: 'phone/:contactid', component: PhonePageComponent }
];
