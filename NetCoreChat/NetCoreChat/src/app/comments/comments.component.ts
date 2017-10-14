import { Component, OnInit } from '@angular/core';
import { AuthService } from "angular4-social-login";
import { SocialUser } from "angular4-social-login";
import { AuthenticationService } from "../services/auth/auth.service";
import { Http } from '@angular/http';
import {UserComment} from './UserComment';

@Component({
    selector: 'app-comments',
    templateUrl: './comments.component.html',
    styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

    Comments: Array<UserComment>;
    constructor(private authService: AuthenticationService, 
                private httpService: Http) {
                    
    }

    ngOnInit() {
        this.httpService.get('/api/comments/GetAllComments').subscribe(values => {
            this.Comments = values.json();

            console.log(this.Comments);
         });
    }
}
