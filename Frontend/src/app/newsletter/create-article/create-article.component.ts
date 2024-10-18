import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { IArticle } from '../../api/services/models/contracts/article.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ArticleService } from '../../api/services/article.service';
import { CommonService } from '../../api/services/common.service';
import { catchError, of, Subject, take, takeUntil, tap } from 'rxjs';
import { AlertService } from '../../api/services/alert.service';
import { NewsletterPickerComponent } from '../../base-components/picker/newsletter-picker/newsletter-picker.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-create-article',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    NewsletterPickerComponent
  ],
  templateUrl: './create-article.component.html',
  styleUrl: './create-article.component.scss'
})
export class CreateArticleComponent implements OnInit, OnDestroy {
  private readonly _destroy$: Subject<void> = new Subject<void>();
  private _id: string | null = null;

  public previewPicture: string | null = null;
  public article: IArticle = {
    id: '',
    title: '',
    summary: '',
    description: '',
    newPicture: undefined,
    newsletterId: '',
    published: false,
  };

  constructor(
    private readonly _router: Router,
    private readonly _route: ActivatedRoute,
    private readonly _articleService: ArticleService,
    private readonly _common: CommonService,
    private readonly _alert: AlertService
  ) {}

  public get isUpdate(): boolean {
    return this._id != null;
  }

  ngOnInit(): void {
    this._id = this._route.snapshot.paramMap.get('id');
    if (this._id) {
      this._articleService.getById(this._id).pipe(
        takeUntil(this._destroy$),
        tap(res => {
          if (res.isSuccess) {
            this.article = res.result!;
          } else {
            res.errors.forEach(error => this._alert.showError(error));
          }
        }),
        catchError(errorResponse => {
          console.log(errorResponse);
          return of(undefined);
        })
      ).subscribe();
    }
  }

  ngOnDestroy(): void {
    this._destroy$.next();
    this._destroy$.complete();
  }

  public onSubmit(): void {
    const service = this.isUpdate ? this._articleService.update(this.article) : this._articleService.create(this.article);
    service.pipe(
      take(1),
      takeUntil(this._destroy$),
      tap(res => {
        if (res.isSuccess) {
          this._alert.showSuccess('Beitrag erfolgreich ' + (this.isUpdate ? 'angepasst' : 'erstellt'));
          this._router.navigate(['news/details/' + res.result!.id]);
        } else {
          res.errors.forEach(error => this._alert.showError(error));
        }
      }),
      catchError(err => {
        console.log(err);
        return of(undefined);
      })
    ).subscribe();
  }

  public fileChanged(event: any): void {
    const file: File = event.target.files[0];

    if (file) {
      this._common.fileToByteArray(file)
        .then((byteArray) => {
          this.article.newPicture = Array.from(byteArray);
          this._common.convertByteArrayToBase64(byteArray)
            .then(base => this.previewPicture = base)
            .catch((error) => {
              console.log(error);
              this._alert.showError('Vorschau nicht möglich.');
            });
        })
        .catch((error) => {
          console.log(error);
          this._alert.showError('Datei konnte nicht geladen werden.');
        });
    } else {
      this.previewPicture = null;
      this.article.newPicture = undefined;
    }
  }
}
