import { Component, OnInit } from '@angular/core';
import { AuthService } from "angular4-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angular4-social-login";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

    constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  signInWithFB(): void {
      this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
      this.authService.signOut();
  }

}
