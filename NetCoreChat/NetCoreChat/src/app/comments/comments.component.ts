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

    loggedUserName: string = "";

    commentTitle: string;
    commentText: string;
    commentAutor: string;

    Comments: Array<UserComment>;
    constructor(private authService: AuthenticationService, 
        private httpService: Http) {
        this.authService.getLoggedInName.subscribe(name => this.changeName(name));
        this.loggedUserName = this.authService.getLoggedUserName();
    }

    private changeName(name: string): void {
        this.loggedUserName = name;
    }

    ngOnInit() {
        this.httpService.get('/api/comments/GetAllComments').subscribe(values => {
            this.Comments = values.json();

            console.log(this.Comments);
         });
    }

    onEnter(value: string) {
        this.commentText = value;
        console.log(this.commentText);
    }

    send() {

        console.log(this.commentTitle);
        console.log(this.commentText);
        
    }
}
