import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class SpinnerService {
    private _loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    public isLoading$: Observable<boolean> = this._loading$.asObservable();

    public show(): void {
        this._loading$.next(true);
    }

    public hide(): void {
        this._loading$.next(false);
    }
}