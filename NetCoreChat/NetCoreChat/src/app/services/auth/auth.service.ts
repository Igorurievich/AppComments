import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FacebookLoginProvider } from "angular4-social-login";
import { AuthService } from "angular4-social-login";
import { SocialUser, AuthServiceConfig } from "angular4-social-login";
import { Http, Headers, Response } from '@angular/http';

@Injectable()
export class AuthenticationService {

    private user: SocialUser;
    private loggedIn: boolean;

    public token: string;

    logInWithFB(): void {
        this.loggedIn = true;

        console.log("logged with FB", this.loggedIn);
        //this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(res => {
        //    this.user = res;
        //    this.loggedIn = true;
        //    console.log(this.user.firstName, this.user.email, this.loggedIn);
        //});
    }
    

    constructor(private http: Http) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
    }

    login(username: string, password: string) {
        return this.http.post('/api/account/LogIn', JSON.stringify({ username: username, password: password }))
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let token = response.json() && response.json().token;
                console.log(token);
                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });

    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }



}
