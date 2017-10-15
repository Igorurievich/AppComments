import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../services/auth/auth.service";
import { FormGroup, AbstractControl } from '@angular/forms';
import { Router } from "@angular/router";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    constructor(private authService: AuthenticationService, private router: Router) {

    }

    tempResponse: any;

    ngOnInit() {

    }

    loginUser(e) {
        e.preventDefault();
        var username = e.target.elements[0].value;
        var password = e.target.elements[1].value;

        console.log(username, password);

        if (username == 'admin' && password == 'admin') {
            this.router.navigate(["comments"]);
            
        }

        this.tempResponse = this.authService.login(username, password);
        console.log("Temp:", this.tempResponse);
    }

    signInWithFB() {
        this.authService.logInWithFB();
    }
}
