import { Component, Input } from '@angular/core';
import { IArticle } from '../../api/services/models/contracts/article.interface';

@Component({
  selector: 'app-article-list-item',
  standalone: true,
  imports: [],
  templateUrl: './article-list-item.component.html',
  styleUrl: './article-list-item.component.scss'
})
export class ArticleListItemComponent {
  @Input()
  public article!: IArticle;

}
