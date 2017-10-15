import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../services/auth/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

    constructor(private authService: AuthenticationService, private router: Router) {
        
    }

    ngOnInit() {
        
    }

    register(e) {
        e.preventDefault();
        var username = e.target.elements[0].value;
        var password = e.target.elements[1].value;
        var email = e.target.elements[2].value;

        this.authService.register(username, password, email).subscribe(data => {

            if (data.text().length != 0) {
                this.router.navigate(["comments"]);
            }
        });
    }
}
