import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from "./services/auth/auth.service";
import { Http } from "@angular/http";

@Injectable()
export class AuthguardGuard implements CanActivate {

    state: boolean = false;

    constructor(private router: Router,
        private httpService: Http) {
    }
    canActivate() {
        this.checkUser().subscribe(res => { this.state = res; console.log(res) });
        return this.state;
    }


    private checkUser(): Observable<boolean> {
        if (localStorage.getItem('currentUser')) {
            // logged in so return true

            let curUsr = JSON.parse(localStorage.getItem('currentUser'));

            let urlSearchParams = new URLSearchParams();
            urlSearchParams.append('username', curUsr.username);
            return this.httpService.get('/api/account/CheckUserName', { search: urlSearchParams })
                .map(res => {
                    if (res.text() == "true") {
                        console.log(res.text());
                        return true;
                    }
                });

            //.toPromise()
            //.then(data => {
            //    console.log("data" + data);
            //    if (data == "false") {
            //       
            //        this.router.navigate(['/login']);
            //        console.log("return false");
            //        return false;
            //    }
            //    else {
            //        console.log("return true");
            //        return true;
            //    }
            //});
        }
    }
}
