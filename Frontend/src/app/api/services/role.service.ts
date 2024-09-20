import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from '../../api/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseService {
  protected override route: string = "role";

  public getAll(): Observable<any> {
    return this.get('getAll');
  }
}
