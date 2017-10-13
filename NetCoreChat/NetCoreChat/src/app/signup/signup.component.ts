import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../services/auth/auth.service";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

    constructor(private authService: AuthenticationService) {
        
    }

    ngOnInit() {
        
    }
}
