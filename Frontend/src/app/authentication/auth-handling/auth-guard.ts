import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from "@angular/router";
import { AuthenticationService } from "../../api/services/authentication.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(
        private readonly _authService: AuthenticationService,
        private readonly _router: Router
    ) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
        if (this._authService.isAuthenticated) {
            return true;
        } else {
            this._router.navigate(['/login']);
            return false;
        }
    }

}