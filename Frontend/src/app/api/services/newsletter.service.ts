import { Injectable } from '@angular/core';
import { ISearchRequestBase } from './models/contracts/search-request-base.interface';
import { BaseViewService } from './base/base-view.service';
import { INewsletter } from './models/contracts/newsletter.interface';

@Injectable({
  providedIn: 'root'
})
export class NewsletterService extends BaseViewService<INewsletter, ISearchRequestBase> {
  protected override route: string = 'Newsletters';
}
