import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './api/services/authentication.service';
import { SpinnerComponent } from './base-components/spinner/spinner.component';
import { SpinnerService } from './base-components/spinner/spinner.service';
import { Observable } from 'rxjs';
import { MenuComponent } from './base-components/menu/menu.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    SpinnerComponent,
    MenuComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  constructor(
    private readonly _spinnerService: SpinnerService,
    private readonly _auth: AuthenticationService) {
  }

  public get isAuthenticated(): boolean {
    return this._auth.isAuthenticated;
  }

  public get isLoading$(): Observable<boolean> {
    return this._spinnerService.isLoading$;
  }

  public logout(): void {
    this._auth.logout();
  }
}
