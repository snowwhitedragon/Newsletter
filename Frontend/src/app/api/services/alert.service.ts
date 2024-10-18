import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  private _horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  private _verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(private readonly _snackBar: MatSnackBar) {}

  public show(message: string, action: string = '', duration: number = 3000, className?: string): void {
    this._snackBar.open(message, action, {
      duration: duration,
      panelClass: [className ?? ''],      
      horizontalPosition: this._horizontalPosition,
      verticalPosition: this._verticalPosition,
    });
  }

  public showSuccess(message: string): void {
    this._snackBar.open(message, '', {
      duration: 3000,
      panelClass: ['snack-success']
    });
  }

  public showError(message: string): void {
    this._snackBar.open(message, '', {
      duration: 3000,
      panelClass: ['snack-error']
    });
  }
}
