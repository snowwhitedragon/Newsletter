import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IResponse } from "./contracts/response.interface";
import { BaseService } from "./base-service";

@Injectable({
    providedIn: 'root'
})
export abstract class BaseViewService<T,S> extends BaseService {
    protected abstract route: string;
    
    public getById(id: string): Observable<IResponse<T>> {
        return this.get<IResponse<T>>(`${this.route}/GetById/${id}`);
    }

    public search(search: S): Observable<IResponse<T[]>> {
        return this.post<S, IResponse<T[]>>(`${this.route}/Search`, search);
    }
}