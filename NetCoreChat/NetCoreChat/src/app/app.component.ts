import { Component } from '@angular/core';
import { Http } from '@angular/http';

import { AuthenticationService } from './services/auth/auth.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    isLogged: boolean;
    userName = '';

    constructor(private authService: AuthenticationService, private router: Router) {
        this.authService.getLoggedInName.subscribe(name => this.changeName(name));
        this.authService.getLoggedInStatus.subscribe(status => this.changeStatus(status));
        this.userName = this.authService.getLoggedUserName();
        if (this.userName.length > 0) {
            this.isLogged = true;
        }
        this.router.navigate(['comments']);
    }

    private changeName(name: string): void {
        this.userName = name;
    }

    private changeStatus(status: boolean): void {
        this.isLogged = status;
    }

    ngOnInit() {

    }

    logOut() {
        this.authService.logout();
        this.router.navigate(['login']);
    }
}
