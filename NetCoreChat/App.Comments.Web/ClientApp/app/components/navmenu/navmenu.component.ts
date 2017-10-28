import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { AuthenticationService } from "../../services/auth/auth.service";
import { isPlatformServer, isPlatformBrowser } from '@angular/common';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {

    loggedUserName = '';
    isLogged = false;

    subscriptionStatus: any;
    subscriptionName: any;

    constructor(private authService: AuthenticationService, @Inject(PLATFORM_ID) private platformId: Object) {
        if (isPlatformBrowser(this.platformId)) {
            this.checkUser();
        }
    }

    private changeName(name: string): void {
        this.loggedUserName = name;
    }

    private changeStatus(status: boolean): void {
        this.isLogged = status;
    }

    ngOnInit() {
        if (isPlatformBrowser(this.platformId)) {
            this.subscriptionName = this.authService.getLoggedInName.subscribe((item: string) => this.changeName(item));
            this.subscriptionStatus = this.authService.getLoggedInStatus.subscribe((item: boolean) => this.changeStatus(item));

            this.checkUser();
        }
    }

    private checkUser() {
        this.authService.checkUserName().toPromise().then((data: any) => {
            if (data.text() === 'false') {
                this.authService.logout();
            } else {
                this.loggedUserName = this.authService.getLoggedUserName();
                this.isLogged = true;
                console.log(this.loggedUserName);
            }
        });
    }

    logOut() {
        this.authService.logout();
    }
}
