import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticateService } from '../services/authenticate.service';


@Injectable({ providedIn: 'root' })
export class Authgard implements CanActivate {
    constructor(
        private router: Router,
        private authenticateService: AuthenticateService
    ) { }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUSer = this.authenticateService.currentUserValue;
        if (currentUSer) {
            return true;
        }
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
