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

    logInWithFB(): void {

        console.log("logged with FB");
        this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(res => {
            this.user = res;
            console.log(this.user.firstName, this.user.email, this.user.provider, this.user.id);

            let urlSearchParams = new URLSearchParams();
            urlSearchParams.append('username', this.user.firstName);
            urlSearchParams.append('email', this.user.email);
            this.httpService.get('/api/account/LogInUserWithFacebook', { search: urlSearchParams }).subscribe(res => {

                this.putTokenToLocalStorage(res.text(), this.user.firstName);
            });
        });

        
    }

    constructor(private httpService: Http, private authService: AuthService) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
    }

    register(username: string, password: string, email: string) {
        console.log("registerMethod");

        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        urlSearchParams.append('email', email);
        console.log(urlSearchParams);
        this.httpService.post('/api/account/Register', urlSearchParams).subscribe(res => {
            console.log(res.text());
            return res;
        });
    }


    login(username: string, password: string){
        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);
        this.httpService.get('/api/account/LogInUser', { search: urlSearchParams }).subscribe(res => {
            this.putTokenToLocalStorage(res.text(), username);
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
    }



}
