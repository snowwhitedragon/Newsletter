import { Injectable } from '@angular/core';
import { BaseService } from './base-service';

@Injectable({
  providedIn: 'root'
})
export class ArticleServiceService extends BaseService {
  protected override route: string = 'articles';
}
