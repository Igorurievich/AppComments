import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/Rx';
import { AuthenticationService } from "../../services/auth/auth.service";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

    private username: string;
    private password: string;
    private email: string;

    constructor(private authService: AuthenticationService, private router: Router) {

    }

    ngOnInit() {

    }

    register() {
        this.authService.register(this.username, this.password, this.email);
    }

    signUpWithFB() {
        this.authService.registerWithFacebook();
    }
}
