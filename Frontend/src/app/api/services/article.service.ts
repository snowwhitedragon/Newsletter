import { Injectable } from '@angular/core';
import { IArticle } from './models/contracts/article.interface';
import { ISearchRequestBase } from './models/contracts/search-request-base.interface';
import { Observable, of } from 'rxjs';
import { BaseActionService } from './base/base-action.service';
import { IResponse } from './base/contracts/response.interface';

@Injectable({
  providedIn: 'root'
})
export class ArticleService extends BaseActionService<IArticle, ISearchRequestBase> {
  protected override route: string = 'Articles';  
  private _testValues: IArticle[] = [
    {
      id: '1',
      title: 'Erster Artikel',
      summary: 'Dies ist eine Zusammenfassung des ersten Artikels.',
      description: 'Vollständige Beschreibung des ersten Artikels.',
      picture: '/assets/images/example.png',
      newsletterId: 'n1',
      newsletter: { id: 'n1', title: 'Newsletter 1' },
      createdAt: new Date(),
      createdById: 'u1',
      createdBy: { displayName: 'Max Mustermann' },
      published: true,
      publishedAt: new Date(),
      publishedById: 'u2',
      publishedBy: { displayName: 'Anna Beispiel' }
    },
    {
      id: '2',
      title: 'Zweiter Artikel',
      summary: 'Dies ist eine Zusammenfassung des zweiten Artikels.',
      description: 'Vollständige Beschreibung des zweiten Artikels.',
      picture: '/assets/images/Bild1.png',
      newsletterId: 'n1',
      newsletter: { id: 'n1', title: 'Newsletter 1' },
      createdAt: new Date(),
      createdById: 'u1',
      createdBy: { displayName: 'Max Mustermann' },
      published: true,
      publishedAt: new Date(),
      publishedById: 'u2',
      publishedBy: { displayName: 'Anna Beispiel' }
    },
    {
      id: '3',
      title: 'Dritter Artikel',
      summary: 'Dies ist eine Zusammenfassung des dritten Artikels.',
      description: 'Vollständige Beschreibung des dritten Artikels.',
      picture: '/assets/images/controls.png',
      newsletterId: 'n1',
      newsletter: { id: 'n1', title: 'Newsletter 1' },
      createdAt: new Date(),
      createdById: 'u1',
      createdBy: { displayName: 'Max Mustermann' },
      published: true,
      publishedAt: new Date(),
      publishedById: 'u2',
      publishedBy: { displayName: 'Anna Beispiel' }
    },
    {
      id: '4',
      title: 'Vierter Artikel',
      summary: 'Dies ist eine Zusammenfassung des vierten Artikels.',
      description: 'Vollständige Beschreibung des vierten Artikels.',
      picture: '/assets/images/human.png',
      newsletterId: 'n1',
      newsletter: { id: 'n1', title: 'Newsletter 1' },
      createdAt: new Date(),
      createdById: 'u1',
      createdBy: { displayName: 'Max Mustermann' },
      published: true,
      publishedAt: new Date(),
      publishedById: 'u2',
      publishedBy: { displayName: 'Anna Beispiel' }
    },
    {
      id: '5',
      title: '!! Neues Spiel !!',
      summary: 'NEU: Im Herbst erscheint ein neues Fantasy RPG.',
      description: 'Hier könnte natürlich sehr ausführlich etwas über dieses coole Spiel stehen.',
      picture: '/assets/images/game-logo.png',
      newsletterId: 'n1',
      newsletter: { id: 'n1', title: 'Newsletter 1' },
      createdAt: new Date(),
      createdById: 'u1',
      createdBy: { displayName: 'Max Mustermann' },
      published: true,
      publishedAt: new Date(),
      publishedById: 'u2',
      publishedBy: { displayName: 'Anna Beispiel' }
    },
  ];

  public publish(id: string): Observable<IResponse<IArticle>> {
    return this.post<any, IResponse<IArticle>>('Publish/'+id, null);
  }

  public override getById(id: string): Observable<IResponse<IArticle>> {
    return of({ result: this._testValues.find(t => t.id == id), errors: [], isSuccess: true });
  }

  public override search(search: ISearchRequestBase): Observable<IResponse<IArticle[]>> {
    return of({ result: this._testValues, errors: [], isSuccess: true });
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
