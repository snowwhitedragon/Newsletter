import { Injectable } from '@angular/core';
import { ISearchRequestBase } from './models/contracts/search-request-base.interface';
import { BaseActionService } from './base/base-action.service';
import { IContact } from './models/contracts/contact.interface';

@Injectable({
  providedIn: 'root'
})
export class ContactService extends BaseActionService<IContact, ISearchRequestBase> {
  protected override route: string = 'Contacts';
}
