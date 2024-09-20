import { Injectable } from "@angular/core";
import { AuthenticationService } from "../../api/services/authentication.service";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = null;

        if (token) {
            const request = req.clone({
                headers: req.headers.set('Authentication', `Bearer ${token}`)
            });

            return next.handle(request);
        }

        return next.handle(req);
    }
}