import { Injectable, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformServer, isPlatformBrowser } from '@angular/common';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from "./services/auth/auth.service";

@Injectable()
export class AuthguardGuard implements CanActivate {

    constructor(private router: Router, @Inject(PLATFORM_ID) private platformId: Object) {

    }
    canActivate() {
        if (isPlatformBrowser(this.platformId)) {
            let token = localStorage.getItem('currentUser');
            if (token) {
                // logged in so return true
                return true;
            }
        }

        // not logged in so redirect to login page
        this.router.navigate(['/login']);
        return false;
    }
}
