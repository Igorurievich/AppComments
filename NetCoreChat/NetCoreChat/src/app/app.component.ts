import { Component } from '@angular/core';
import { Http } from '@angular/http'
import { AuthenticationService } from "./services/auth/auth.service";
import { Router } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    isLogged: boolean;
    
    constructor(private authService: AuthenticationService, private router: Router) {
        this.isLogged = this.authService.isLoggedIn();
    }

    ngOnInit() {
        this.isLogged = this.authService.isLoggedIn();
    }

    logOut() {
        this.authService.logout();
        this.router.navigate(["login"]);
    }
}
