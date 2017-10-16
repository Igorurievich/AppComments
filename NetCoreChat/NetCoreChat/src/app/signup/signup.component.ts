import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../services/auth/auth.service";
import { Router } from "@angular/router";
import { Subscription } from 'rxjs/Rx';

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

    register(e) {

        this.authService.register(this.username, this.password, this.email).subscribe(data => {
            if (data.text().length != 0) {
                this.router.navigate(["comments"]);
            }
        });
    }
}
