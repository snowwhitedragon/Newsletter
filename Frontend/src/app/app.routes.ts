import { Routes } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { RegistrationComponent } from './authentication/registration/registration.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: RegistrationComponent },
    { path: 'register', component: RegistrationComponent },
    { path: '**', redirectTo: '' }
];
