import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NewsletterService } from '../../../api/services/newsletter.service';
import { INewsletter } from '../../../api/services/models/contracts/newsletter.interface';
import { catchError, Observable, of, tap } from 'rxjs';
import { SearchRequestBase } from '../../../api/services/models/search-request-base';
import { IResponse } from '../../../api/services/base/contracts/response.interface';
import { FormsModule } from '@angular/forms';
import { AlertService } from '../../../api/services/alert.service';
import { OrganizationService } from '../../../api/services/organization.service';
import { IOrganization } from '../../../api/services/models/contracts/organization.interface';
import { OrganizationSearchRequest } from '../../../api/services/models/organization-search-request';

@Component({
  selector: 'app-newsletter-picker',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './newsletter-picker.component.html',
  styleUrl: './newsletter-picker.component.scss'
})
export class NewsletterPickerComponent {
  @Input()
  public selectedId?: string = '';
  @Output()
  public onSelect: EventEmitter<string> = new EventEmitter<string>();

  public response$: Observable<IResponse<INewsletter[]> | undefined>;
  public options: IOrganization[] = [];

  constructor(private readonly _organizationView: OrganizationService, private readonly _alert: AlertService) {
    this.response$ = this._organizationView.search(new OrganizationSearchRequest()).pipe(
      tap(re => {
        if (re.isSuccess && re.result) {
          this.options = re.result;
        } else {
          re.errors.forEach(err => this._alert.showError(err));
        }
      }),
      catchError(error => {
        console.log(error);
        return of(undefined);
      })
    );
  }

  public get selected(): string | undefined {
    return this.selectedId;
  }

  public onChange(event: Event): void {
    this.selectedId = (event.target as HTMLSelectElement).value;
    this.onSelect.emit(this.selectedId);
  }
}
