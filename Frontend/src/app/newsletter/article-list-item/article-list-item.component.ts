import { Component, Input } from '@angular/core';
import { IArticle } from '../../api/services/models/contracts/article.interface';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-article-list-item',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './article-list-item.component.html',
  styleUrl: './article-list-item.component.scss'
})
export class ArticleListItemComponent {
  @Input()
  public article!: IArticle;

}
