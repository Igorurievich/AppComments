import { AuthenticationService } from "../services/auth/auth.service";
import { FormGroup, AbstractControl } from '@angular/forms';
import { Router } from "@angular/router";
import {
    Component, Directive, forwardRef,
    Attribute, OnChanges, SimpleChanges, Input, OnInit
} from '@angular/core';
import {
    NG_VALIDATORS, Validator,
    Validators, ValidatorFn
} from '@angular/forms';

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
        
        this.authService.login(username, password).subscribe(data => {
            this.router.navigate(["comments"]);
        });
    }

    signInWithFB() {

        console.log(this.authService.logInWithFB());

        this.authService.logInWithFB().subscribe(data => {
                this.router.navigate(["comments"]);
        });
    }
}
