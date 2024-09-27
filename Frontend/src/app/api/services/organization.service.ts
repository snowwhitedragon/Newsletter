import { Injectable } from '@angular/core';
import { ISearchRequestBase } from './models/contracts/search-request-base.interface';
import { BaseViewService } from './base/base-view.service';
import { IOrganization } from './models/contracts/organization.interface';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService extends BaseViewService<IOrganization, ISearchRequestBase> {
  protected override route: string = 'Organizations';
}
