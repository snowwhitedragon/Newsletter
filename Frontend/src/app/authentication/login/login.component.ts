import { Component, OnDestroy } from '@angular/core';
import { catchError, finalize, of, Subject, takeUntil, tap } from 'rxjs';
import { ILoginData } from '../../api/services/models/contracts/login-data.interface';
import { FormsModule } from '@angular/forms';
import { AuthenticationService } from '../../api/services/authentication.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from '../../base-components/spinner/spinner.component';
import { ErrorDisplayComponent } from '../../base-components/error-display/error-display.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ErrorDisplayComponent
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnDestroy {
  private readonly _destroy$: Subject<void> = new Subject<void>();
  public loginData: ILoginData = { userName: '', password: ''};
  public hasError: boolean = false;
  public error: string = '';

  constructor(private readonly _service: AuthenticationService, private readonly _router: Router) {
  }

  ngOnDestroy(): void {
    this._destroy$.next();
    this._destroy$.complete();
  }

  public login(): void {
    this._service.login(this.loginData).pipe(
      takeUntil(this._destroy$),
      tap(res => {
        if(res.isSuccess) {
          this._router.navigate(['/home']);
        } else {
          this.hasError = true;
          this.error = res.errors.join('\n');
        }
      }),
      catchError(error => {
        this.hasError = true;
        this.error = 'Es ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut oder kontaktieren Sie den Admin.';
        return of(undefined);
      }),
    ).subscribe();
  }
}
