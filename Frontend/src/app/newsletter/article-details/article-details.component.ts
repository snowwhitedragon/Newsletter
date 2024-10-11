import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { map, Observable } from 'rxjs';
import { IArticle } from '../../api/services/models/contracts/article.interface';
import { ArticleService } from '../../api/services/article.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-article-details',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './article-details.component.html',
  styleUrl: './article-details.component.scss'
})
export class ArticleDetailsComponent implements OnInit {
  public article$: Observable<IArticle>;

  constructor(
    private readonly _route: ActivatedRoute,
    private readonly _articleService: ArticleService) {
    const id = this._route.snapshot.paramMap.get('id');
    this.article$ = this._articleService.getById(id!).pipe(map(res => res.result!));
  }

  ngOnInit(): void {
  }
}
