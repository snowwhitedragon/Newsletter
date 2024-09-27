import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseViewService } from "./base-view.service";
import { IResponse } from "./contracts/response.interface";

@Injectable({
    providedIn: 'root'
})
export abstract class BaseActionService<T,S> extends BaseViewService<T,S> {
    public create(newEntry: T): Observable<IResponse<T>> {
        return this.post<T, IResponse<T>>(`${this.route}/Create`, newEntry);
    }

    public update(patchEntry: T): Observable<IResponse<T>> {
        return this.post<T, IResponse<T>>(`${this.route}/Update`, patchEntry);
    }

    public remove(id: string): Observable<IResponse<boolean>> {
        return this.delete<IResponse<boolean>>(`${this.route}/Delete/${id}`);
    }    
}