import { Injectable, OnInit, PLATFORM_ID, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { FacebookLoginProvider } from 'angular4-social-login';
import { AuthService } from 'angular4-social-login';
import { SocialUser, AuthServiceConfig } from 'angular4-social-login';
import { Http, Headers, Response, URLSearchParams } from '@angular/http';
import { EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { isPlatformServer, isPlatformBrowser } from '@angular/common';

@Injectable()
export class AuthenticationService {

    private user: SocialUser;
    public token: string;
    private baseUrl: string;

    @Output() getLoggedInName: EventEmitter<string> = new EventEmitter();
    @Output() getLoggedInStatus: EventEmitter<boolean> = new EventEmitter();

    constructor(private httpService: Http, private authService: AuthService, private router: Router, @Inject(PLATFORM_ID) private platformId: Object, @Inject('BASE_URL') baseUrl: string) {
        if (isPlatformBrowser(this.platformId)) {
            this.baseUrl = baseUrl;
            let currentUser: any;
            const token = localStorage.getItem('currentUser');
            if (token) {
                const currentUser = JSON.parse(token);
            }
            this.token = currentUser && currentUser.token;
        }
    }

    private logInWithFB(): Promise<any> {
        return this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(data => {
            if (data) {
                this.user = data;
            }
        });
    }

    private logInuserWithFB(urlSearchParams: URLSearchParams) {
        this.httpService.get(this.baseUrl + 'api/account/LogInUserWithFacebook', { search: urlSearchParams })
            .toPromise()
            .then(res => {
                this.putTokenToLocalStorage(res.text(), this.user.firstName);
            });
    }

    loginWithFBOnServer() {
        this.logInWithFB().then(() => {
            const urlSearchParams = new URLSearchParams();
            urlSearchParams.append('username', this.user.firstName);
            urlSearchParams.append('email', this.user.email);
            this.logInuserWithFB(urlSearchParams);
        });
    }

    registerWithFacebook() {
        this.logInWithFB().then(() => {
            const urlSearchParams = new URLSearchParams();
            urlSearchParams.append('username', this.user.firstName);
            urlSearchParams.append('email', this.user.email);
            this.httpService.post(this.baseUrl + 'api/account/RegisterWithFacebook', urlSearchParams).toPromise().then(res => {
                this.logInuserWithFB(urlSearchParams);
            });
        });
    }

    public getLoggedUserName(): string {
        const token = localStorage.getItem('currentUser');
        if (token) {
            return JSON.parse(token).username;
        }
    }

    public checkUserName(): any {
        const userName = this.getLoggedUserName();

        const urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', userName);
        return this.httpService.get(this.baseUrl + 'api/account/CheckUserName', { search: urlSearchParams })
            .map(res => {
                return res;
            });
    }

    register(username: string, password: string, email: string) {
        const urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        urlSearchParams.append('email', email);
        this.httpService.post(this.baseUrl + 'api/account/Register', urlSearchParams).toPromise().then(res => {
            alert(res.json());
            this.login(username, password);
        }).catch(err => {
            alert(err);
        });
    }

    login(username: string, password: string): any {
        const urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        console.log(this.baseUrl);
        return this.httpService.get(this.baseUrl + 'api/account/LogInUser', { search: urlSearchParams }).toPromise().then(res => {
            if (res.text().length > 0) {
                this.putTokenToLocalStorage(res.text(), username);
                return res;
            } else {
                return '';
            }
        });
    }

    private putTokenToLocalStorage(token: string, username: string) {
        if (token) {
            // set token property
            this.token = token;
            // store username and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));
            this.getLoggedInName.emit(username);
            this.getLoggedInStatus.emit(true);
            this.router.navigate(['comments']);
        }
    }

    logout() {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
        this.getLoggedInStatus.emit(false);
        this.router.navigate(['login']);
    }
}
