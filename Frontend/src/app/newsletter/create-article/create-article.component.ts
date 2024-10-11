import { Component, Input } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { IArticle } from '../../api/services/models/contracts/article.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-article',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './create-article.component.html',
  styleUrl: './create-article.component.scss'
})
export class CreateArticleComponent {
  @Input()
  public article: IArticle = {
    id: '',
    title: '',
    summary: '',
    description: '',
    link: '',
    picture: '',
    newsletterId: '',
    newsletter: { id: '', title: '' },
    published: false,
  };
  public image: any;

  constructor() {}

  public onSubmit(): void {
    console.log('Artikel erstellt:', this.article);
  }
}
