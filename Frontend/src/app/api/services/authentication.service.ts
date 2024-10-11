import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ILoginData } from '../../api/services/models/contracts/login-data.interface';
import { IRegistrationData } from '../../api/services/models/contracts/registration-data.interface';
import { BaseService } from './base/base-service';
import { IResponse } from './base/contracts/response.interface';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  route: string = 'authentication';
  private readonly tokenStorage = 'nentindo-news-token';

  constructor(
    private readonly _router: Router,
    private readonly _http: HttpClient) {
        super(_http);
  }

  public get isAuthenticated(): boolean {
    return !!localStorage.getItem(this.tokenStorage);
  }

  public get token(): string | null {
    return localStorage.getItem(this.tokenStorage);
  }

  public logout(): void {
    localStorage.removeItem(this.tokenStorage);
    this._router.navigate(['/login']);
  }

  public login(user: ILoginData): Observable<IResponse<string>> {
    return this.post<ILoginData, IResponse<string>>(this.route + '/login', user).pipe(
      tap(res => {
        if (res.isSuccess && res.result) {
          localStorage.setItem(this.tokenStorage, res.result);
        }
      })
    );
  }

  public register(userRegistration: IRegistrationData): Observable<IResponse<boolean>> {
    return this.post<IRegistrationData, IResponse<boolean>>(this.route + '/register', userRegistration);
  }
}
