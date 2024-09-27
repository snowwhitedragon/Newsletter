import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseViewService } from './base/base-view.service';
import { SearchRequestBase } from './models/search-request-base';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseViewService<any, SearchRequestBase> {
  protected override route: string = "Roles";

  public assignToRole(userId: string, roleId: string): Observable<any> {
    return this.get(`user/${userId}/role/${roleId}`);
  }
}
