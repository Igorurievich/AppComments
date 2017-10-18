import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FacebookLoginProvider } from "angular4-social-login";
import { AuthService } from "angular4-social-login";
import { SocialUser, AuthServiceConfig } from "angular4-social-login";
import { Http, Headers, Response, URLSearchParams } from '@angular/http';
import { EventEmitter, Output } from '@angular/core';
import { Router } from "@angular/router";

@Injectable()
export class AuthenticationService {

    private user: SocialUser;
    public token: string;

    @Output() getLoggedInName: EventEmitter<string> = new EventEmitter();
    @Output() getLoggedInStatus: EventEmitter<boolean> = new EventEmitter();

    logInWithFB(): any {
        Observable.fromPromise(this.authService.signIn(FacebookLoginProvider.PROVIDER_ID))
            .switchMap(res => {
                let urlSearchParams = new URLSearchParams();
                urlSearchParams.append('username', res.firstName);
                urlSearchParams.append('email', res.email);
                return this.httpService.get('/api/account/LogInUserWithFacebook', { search: urlSearchParams })
                    .map(res => {
                        this.putTokenToLocalStorage(res.text(), this.user.firstName);
                        return res.text();
                    });
            }); //TODO: Fix this shit
    }

    public getLoggedUserName(): string {
        let curUsr = JSON.parse(localStorage.getItem('currentUser'));
        if (curUsr == null) {
            return "";
        }
        return curUsr.username;
    }

    public checkUserName(): any {
        let userName = this.getLoggedUserName();

        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', userName);
        return this.httpService.get('/api/account/CheckUserName', { search: urlSearchParams })
            .map(res => {
                return res;
            });
    }

    constructor(private httpService: Http, private authService: AuthService, private router:Router) {
        // set token if saved in local storage
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
    }

    register(username: string, password: string, email: string): any {
        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        urlSearchParams.append('email', email);

        return this.httpService.post('/api/account/Register', urlSearchParams)
            .map(res => {
            return res;
        });
    }

    login(username: string, password: string) : any {
        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        return this.httpService.get('/api/account/LogInUser', { search: urlSearchParams })
            .map(res => {
                if (res.text().length > 0) {
                    this.putTokenToLocalStorage(res.text(), username);
                    return res;
                } else {
                    return "";
                }
            });
    }

    private putTokenToLocalStorage(token: string, username:string) {
        if (token) {
            // set token property
            this.token = token;
            // store username and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));
            this.getLoggedInName.emit(username);
            this.getLoggedInStatus.emit(true);
        }
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
        this.getLoggedInStatus.emit(false);
        this.router.navigate(["login"]);
    }
}
