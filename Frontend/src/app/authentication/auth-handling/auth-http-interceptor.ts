import { Injectable } from "@angular/core";
import { AuthenticationService } from "../../api/services/authentication.service";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { finalize, Observable } from "rxjs";
import { SpinnerService } from "../../base-components/spinner/spinner.service";

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor {
    constructor(
        private readonly _auth: AuthenticationService,
        private readonly _spinner: SpinnerService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this._auth.token;
        this._spinner.show();

        if (token) {
            const request = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${token}`)
            });

            return next.handle(request).pipe(finalize(() => this._spinner.hide()));
        }

        return next.handle(req);
    }
}