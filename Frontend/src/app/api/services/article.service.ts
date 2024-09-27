import { Injectable } from '@angular/core';
import { IArticle } from './models/contracts/article.interface';
import { ISearchRequestBase } from './models/contracts/search-request-base.interface';
import { Observable } from 'rxjs';
import { BaseActionService } from './base/base-action.service';
import { IResponse } from './base/contracts/response.interface';

@Injectable({
  providedIn: 'root'
})
export class ArticleService extends BaseActionService<IArticle, ISearchRequestBase> {
  protected override route: string = 'Articles';

  public publish(id: string): Observable<IResponse<IArticle>> {
    return this.post<any, IResponse<IArticle>>('Publish/'+id, null);
  }

  public arrayBufferToBase64(buffer: Uint8Array): string {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    
    return window.btoa(binary);
  }
}
