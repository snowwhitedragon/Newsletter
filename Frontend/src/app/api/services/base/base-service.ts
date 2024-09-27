import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService {
    protected url = 'https://localhost:44383/api/';

    constructor(
        protected _httpClient: HttpClient) {
    }

    protected get<T>(endPoint: string): Observable<T> {
        return this._httpClient.get<T>(`${this.url}${endPoint}`);
    }

    protected post<T, R>(endPoint: string, value: T): Observable<R> {
        return this._httpClient.post<R>(`${this.url}${endPoint}`, value);
    }

    protected put<T, R>(endPoint: string, value: T): Observable<R> {
        return this._httpClient.put<R>(`${this.url}${endPoint}`, value);
    }

    protected delete<T>(endPoint: string): Observable<T> {
        return this._httpClient.delete<T>(`${this.url}${endPoint}`);
    }
}