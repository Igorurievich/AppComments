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
    userName: string = "";
    
    constructor(private authService: AuthenticationService, private router: Router) {
        //this.checkUser();
        this.authService.getLoggedInName.subscribe(name => this.changeName(name));
        this.authService.getLoggedInStatus.subscribe(status => this.changeStatus(status));
        this.userName = this.authService.getLoggedUserName();
    }

    private changeName(name: string): void {
        this.userName = name;
    }

    private changeStatus(status: boolean): void {
        console.log()
        this.isLogged = status;
    }

    ngOnInit() {
        
    }

    //checkUser() {
    //    this.authService.checkUserName().subscribe(data => {
    //        console.log(data);
    //        if (data.toString() == "false") {
    //            console.log(data);
    //            this.authService.logout();
    //        }
    //        else {
    //            this.userName = this.authService.getLoggedUserName();
    //        }
    //    });
    //}


    logOut() {
        this.authService.logout();
        this.router.navigate(["login"]);
    }
}
