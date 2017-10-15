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

    ngOnInit() {

    }

    loginUser(e) {
        e.preventDefault();
        var username = e.target.elements[0].value;
        var password = e.target.elements[1].value;

        if (username == 'admin' && password == 'admin') {
            this.router.navigate(["comments"]);
        }
        this.authService.login(username, password);
    }

    signInWithFB() {
        this.authService.logInWithFB();
    }
}
