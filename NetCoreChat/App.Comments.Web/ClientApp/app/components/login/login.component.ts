import { FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import {
    Component, Directive, forwardRef,
    Attribute, OnChanges, SimpleChanges, Input, OnInit
} from '@angular/core';
import { AuthenticationService } from "../../services/auth/auth.service";
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    userName: string;
    password: string;



    constructor(private authService: AuthenticationService, private router: Router) {
      
    }

    ngOnInit() {
    }

    selectedNavItem(item: string) {
        console.log('selected nav item ' + item);
        this.authService.changeStatusName(item);
    }

    loginUser() {
        //this.selectedNavItem('55');
        this.authService.login(this.userName, this.password);
    }

    signInWithFB() {
        this.authService.loginWithFBOnServer();
    }


}
