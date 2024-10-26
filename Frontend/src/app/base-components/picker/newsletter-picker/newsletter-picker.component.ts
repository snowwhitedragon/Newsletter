import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { IResponse } from '../../../api/services/base/contracts/response.interface';
import { FormsModule } from '@angular/forms';
import { AlertService } from '../../../api/services/alert.service';
import { OrganizationService } from '../../../api/services/organization.service';
import { IOrganization } from '../../../api/services/models/contracts/organization.interface';
import { OrganizationSearchRequest } from '../../../api/services/models/organization-search-request';
import { INewsletter } from '../../../api/services/models/contracts/newsletter.interface';

@Component({
  selector: 'app-newsletter-picker',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './newsletter-picker.component.html',
  styleUrl: './newsletter-picker.component.scss',
})
export class NewsletterPickerComponent {
  @Input()
  public selectedOrganizationId?: string = '';
  @Input()
  public selectedNewsletterId?: string = '';
  @Output()
  public onOrganizationSelect: EventEmitter<string> =
    new EventEmitter<string>();
  @Output()
  public onNewsletterSelect: EventEmitter<string> = new EventEmitter<string>();

  public response$: Observable<IResponse<IOrganization[]> | undefined>;
  public options: IOrganization[] = [];

  constructor(
    private readonly _organizationView: OrganizationService,
    private readonly _alert: AlertService
  ) {
    this.response$ = this._organizationView
      .search(new OrganizationSearchRequest())
      .pipe(
        tap((re) => {
          if (re.isSuccess && re.result) {
            this.options = re.result;
          } else {
            re.errors.forEach((err) => this._alert.showError(err));
          }
        }),
        catchError((error) => {
          console.log(error);
          return of(undefined);
        })
      );
  }

  public get selectedNewsletter(): string | undefined {
    return this.selectedNewsletterId;
  }

  public get selectedOrganization(): string | undefined {
    return this.selectedOrganizationId;
  }

  public get newsletters(): INewsletter[] {
    if (!this.selectedOrganizationId) {
      return [];
    }

    const find = this.options.find((x) => x.id == this.selectedOrganizationId);
    return find ? find.newsletters : [];
  }

  public onOrgChange(event: Event): void {
    this.selectedOrganizationId = (event.target as HTMLSelectElement).value;
    this.onOrganizationSelect.emit(this.selectedOrganizationId);
  }

  public onNewsChange(event: Event): void {
    this.selectedNewsletterId = (event.target as HTMLSelectElement).value;
    this.onNewsletterSelect.emit(this.selectedNewsletterId);
  }
}
