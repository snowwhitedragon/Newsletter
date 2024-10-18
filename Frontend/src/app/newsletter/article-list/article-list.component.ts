import { Component, Input, OnInit } from '@angular/core';
import { ArticleService } from '../../api/services/article.service';
import { map, Observable, of } from 'rxjs';
import { IArticle } from '../../api/services/models/contracts/article.interface';
import { ArticleSearchRequest } from '../../api/services/models/article-search-request';
import { ArticleListItemComponent } from '../article-list-item/article-list-item.component';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [
    CommonModule,
    ArticleListItemComponent,    
    RouterLink,
    FormsModule
  ],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.scss'
})
export class ArticleListComponent implements OnInit {
  @Input()
  public organizationId?: string;

  public articles$?: Observable<IArticle[]>;
  public request: ArticleSearchRequest = new ArticleSearchRequest();

  constructor(private readonly _articleService: ArticleService) {}

  ngOnInit(): void {
    this.request.organizationId = this.organizationId;
    this.search();
  }

  public search(): void {
    this.articles$ = this._articleService.search(this.request).pipe(map(res => res?.result ?? []));
  }

  public clear(): void {
    if (!this.request.searchTerm) {
      this.search();
    }
  }
}
