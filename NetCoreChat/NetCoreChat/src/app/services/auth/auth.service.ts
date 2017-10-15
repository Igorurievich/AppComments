import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FacebookLoginProvider } from "angular4-social-login";
import { AuthService } from "angular4-social-login";
import { SocialUser, AuthServiceConfig } from "angular4-social-login";
import { Http, Headers, Response, URLSearchParams } from '@angular/http';

@Injectable()
export class AuthenticationService {

    private user: SocialUser;
    public token: string;

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

    public isLoggedIn(): boolean {

        let result = false;
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
        
        if (currentUser != null) {
            result = true;
        }
        return result;
    }

    constructor(private httpService: Http, private authService: AuthService) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
    }

    register(username: string, password: string, email: string): any {

        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        urlSearchParams.append('email', email);
        console.log(urlSearchParams);
        return this.httpService.post('/api/account/Register', urlSearchParams).subscribe(res => {
            console.log(res.text());
            return res;
        });
    }

    login(username: string, password: string) : any {
        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        return this.httpService.get('/api/account/LogInUser', { search: urlSearchParams })
            .map(res => {
            this.putTokenToLocalStorage(res.text(), username);
            return res;
        });
    }

    private putTokenToLocalStorage(token: string, username:string) {
        if (token) {
            // set token property
            this.token = token;
            // store username and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

            console.log("TOKEN:" + token);
        }
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');

        console.log("logged out");
    }



}
