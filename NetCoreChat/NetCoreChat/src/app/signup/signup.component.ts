import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../services/auth/auth.service";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {


    result: boolean;

    constructor(private authService: AuthenticationService) {
        
    }

    ngOnInit() {
        
    }

    register(e) {
        e.preventDefault();
        var username = e.target.elements[0].value;
        var password = e.target.elements[1].value;
        var email = e.target.elements[2].value;

        console.log(username, password, email);
        console.log(e);

        this.authService.register(username, password, email);
    }
}
