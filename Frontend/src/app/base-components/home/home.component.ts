import { Component, OnInit } from '@angular/core';
import { UserService } from '../../api/services/user.service';
import { Observable } from 'rxjs';
import { IMyUserData } from '../../api/services/models/contracts/my-user-data.interface';
import { IResponse } from '../../api/services/base/contracts/response.interface';
import { CommonModule } from '@angular/common';
import { ArticleListComponent } from '../../newsletter/article-list/article-list.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ArticleListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  public me$: Observable<IResponse<IMyUserData>>;
  constructor(private readonly _userService: UserService) {
    this.me$ = this._userService.getMyProfile();
  }
}
