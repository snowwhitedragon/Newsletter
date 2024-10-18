import { Injectable } from '@angular/core';
import { BaseViewService } from './base/base-view.service';
import { IOrganization } from './models/contracts/organization.interface';
import { OrganizationSearchRequest } from './models/organization-search-request';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService extends BaseViewService<IOrganization, OrganizationSearchRequest> {
  protected override route: string = 'Organizations';
}
