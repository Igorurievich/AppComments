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

    constructor(private authService: AuthenticationService, @Inject(PLATFORM_ID) private platformId: Object) {

    }

    private changeName(name: string): void {
        this.loggedUserName = name;
    }

    private changeStatus(status: boolean): void {
        this.isLogged = status;
    }

    ngOnInit() {
        if (isPlatformBrowser(this.platformId)) {
            this.checkUser();
            this.authService.getLoggedInName.toPromise().then(data => this.changeName(name));
            this.authService.getLoggedInStatus.toPromise().then(status => this.changeStatus(status));
        }
    }

    private checkUser() {
        this.authService.checkUserName().toPromise().then((data: any) => {
            if (data.text() === 'false') {
                this.authService.logout();
            } else {
                this.loggedUserName = this.authService.getLoggedUserName();
            }
        });
    }
}
