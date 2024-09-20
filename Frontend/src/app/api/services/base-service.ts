import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { finalize, Observable } from "rxjs";
import { SpinnerService } from "../../base-components/spinner/spinner.service";

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService {
    private readonly _url = 'https://localhost:44383/api/';
    protected abstract route: string;

    constructor(
        private _httpClient: HttpClient,
        private readonly _spinner: SpinnerService) {
    }

    private get url(): string {
        return this._url + this.route;
    }

    protected get<T>(endPoint: string): Observable<T> {
        this._spinner.show();
        return this._httpClient.get<T>(`${this.url}/${endPoint}`).pipe(finalize(() => this._spinner.hide()));
    }

    protected post<T, R>(endPoint: string, value: T): Observable<R> {
        this._spinner.show();
        return this._httpClient.post<R>(`${this.url}/${endPoint}`, value).pipe(finalize(() => this._spinner.hide()));
    }

    protected put<T, R>(endPoint: string, value: T): Observable<R> {
        this._spinner.show();
        return this._httpClient.put<R>(`${this.url}/${endPoint}`, value).pipe(finalize(() => this._spinner.hide()));
    }

    protected delete<T>(endPoint: string): Observable<T> {
        this._spinner.show();
        return this._httpClient.delete<T>(`${this.url}/${endPoint}`).pipe(finalize(() => this._spinner.hide()));
    }
}