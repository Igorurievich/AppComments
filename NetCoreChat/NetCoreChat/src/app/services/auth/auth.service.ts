import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FacebookLoginProvider } from "angular4-social-login";
import { AuthService } from "angular4-social-login";
import { SocialUser } from "angular4-social-login";

@Injectable()
export class AuthenticationService {

  private user: SocialUser;
  private loggedIn: boolean;

  constructor(private authService: AuthService) {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
    });

  }

   signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
    this.authService.signOut();
  }

  // store the URL so we can redirect after logging in
  redirectUrl: string;

  login(): boolean {
    // to do
    return true;
  }

  logout(): void {

  }
}
