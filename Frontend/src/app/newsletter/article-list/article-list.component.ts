import { Component, Input, OnInit } from '@angular/core';
import { ArticleService } from '../../api/services/article.service';
import { Observable } from 'rxjs';
import { IArticle } from '../../api/services/models/contracts/article.interface';
import { IResponse } from '../../api/services/base/contracts/response.interface';
import { ArticleSearchRequest } from '../../api/services/models/article-search-request';
import { ArticleListItemComponent } from '../article-list-item/article-list-item.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [
    CommonModule,
    ArticleListItemComponent,
  ],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.scss'
})
export class ArticleListComponent implements OnInit {
  @Input()
  public organizationId?: string;

  public articles$?: Observable<IResponse<IArticle[]>>;
  public request: ArticleSearchRequest = new ArticleSearchRequest();

  constructor(private readonly _articleService: ArticleService) {}

  ngOnInit(): void {
    this.request.organizationId = this.organizationId;
    this.articles$ = this._articleService.search(this.request);
  }
  
}
