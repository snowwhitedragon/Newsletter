import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from '../../api/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  protected override route: string = "user";

  public getAll(): Observable<any> {
    return this.get('getAll');
  }
}
