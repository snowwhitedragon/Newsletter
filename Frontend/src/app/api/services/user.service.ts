import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse } from './base/contracts/response.interface';
import { BaseService } from './base/base-service';
import { IMyUserData } from './models/contracts/my-user-data.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  private readonly route: string = "Users";

  public getMyProfile(): Observable<IResponse<IMyUserData>> {
    return this.get<IResponse<IMyUserData>>(this.route + '/GetMe');
  }
}
