import { Routes } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { RegistrationComponent } from './authentication/registration/registration.component';
import { HomeComponent } from './base-components/home/home.component';
import { AuthGuard } from './authentication/auth-handling/auth-guard';
import { CreateArticleComponent } from './newsletter/create-article/create-article.component';
import { ArticleDetailsComponent } from './newsletter/article-details/article-details.component';
import { ArticleListComponent } from './newsletter/article-list/article-list.component';

export const routes: Routes = [
    {path: '', redirectTo: '/home', pathMatch: 'full'},
    { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'news', component: ArticleListComponent, canActivate: [AuthGuard] },
    { path: 'news/details/:id', component: ArticleDetailsComponent, canActivate: [AuthGuard] },
    { path: 'news/create', component: CreateArticleComponent, canActivate: [AuthGuard] },
    { path: 'news/edit/:id', component: CreateArticleComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegistrationComponent },
    { path: '**', redirectTo: '' }
];
