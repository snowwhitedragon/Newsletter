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
  /*private _testValues: IArticle[] = [
    {
      id: '1',
      title: 'Erster Artikel',
      summary: 'Dies ist eine Zusammenfassung des ersten Artikels.',
      description: 'Vollständige Beschreibung des ersten Artikels.',
      previewPicture: '/assets/images/example.png',
      newsletterId: 'n1',
      updatedAt: new Date(),
      updatedByName: 'Max Mustermann',
      published: false
    },
    {
      id: '2',
      title: 'Zweiter Artikel',
      summary: 'Dies ist eine Zusammenfassung des zweiten Artikels.',
      description: 'Vollständige Beschreibung des zweiten Artikels.',
      previewPicture: '/assets/images/Bild1.png',
      newsletterId: 'n1',
      updatedAt: new Date(),
      updatedByName: 'Max Mustermann',
      published: true,
      publishedAt: new Date(),
      publishedByName: 'Anna Beispiel' 
    },
    {
      id: '3',
      title: 'Dritter Artikel',
      summary: 'Dies ist eine Zusammenfassung des dritten Artikels.',
      description: 'Vollständige Beschreibung des dritten Artikels.',
      previewPicture: '/assets/images/controls.png',
      newsletterId: 'n1',
      updatedAt: new Date(),
      updatedByName: 'Max Mustermann',
      published: true,
      publishedAt: new Date(),
      publishedByName: 'Anna Beispiel'
    },
    {
      id: '4',
      title: 'Vierter Artikel',
      summary: 'Dies ist eine Zusammenfassung des vierten Artikels.',
      description: 'Vollständige Beschreibung des vierten Artikels.',
      previewPicture: '/assets/images/human.png',
      newsletterId: 'n1',
      updatedAt: new Date(),
      updatedByName: 'Max Mustermann',
      published: true,
      publishedAt: new Date(),
      publishedByName: 'Anna Beispiel' 
    },
    {
      id: '5',
      title: '!! Neues Spiel !!',
      summary: 'NEU: Im Herbst erscheint ein neues Fantasy RPG.',
      description: 'Hier könnte natürlich sehr ausführlich etwas über dieses coole Spiel stehen.',
      previewPicture: '/assets/images/game-logo.png',
      newsletterId: 'n1',
      updatedAt: new Date(),
      updatedByName: 'Max Mustermann',
      published: true,
      publishedAt: new Date(),
      publishedByName: 'Anna Beispiel'
    },
  ];

  public override getById(id: string): Observable<IResponse<IArticle>> {
    return of({ result: this._testValues.find(t => t.id == id), errors: [], isSuccess: true });
  }

  public override search(search: ISearchRequestBase): Observable<IResponse<IArticle[]>> {
    return of({ result: this._testValues.filter(t => t.title.includes(search.searchTerm ?? '')), errors: [], isSuccess: true });
  }*/

  public publish(id: string): Observable<IResponse<IArticle>> {
    return this.post<any, IResponse<IArticle>>('Publish/'+id, null);
  }
}
