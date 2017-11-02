import { Component, OnInit, PLATFORM_ID, Inject } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { Http } from '@angular/http';
import { UserComment } from './UserComment';
import { User } from '../login/User';
import { HubConnection } from '@aspnet/signalr-client/dist/src';
import { AuthenticationService } from "../../services/auth/auth.service";
import { isPlatformServer, isPlatformBrowser } from '@angular/common';
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'app-comments',
    templateUrl: './comments.component.html',
    styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

    loggedUserName = '';

    private _hubConnection: HubConnection;
    public async: any;

    commentTitle: string;
    commentText: string;
    commentAutor: string;
    baseUrl: string;

    subscription: Subscription;

    Comments: Array<UserComment> = new Array<UserComment>();
    control: any;

    subscriptionName : any;

    constructor(private authService: AuthenticationService,
        private httpService: Http, @Inject(PLATFORM_ID) private platformId: Object, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    //private changeName(name: string): void {
    //    this.loggedUserName = name;
    //}

    private getComments() {
        this.httpService.get(this.baseUrl + 'api/comments/GetAllComments').subscribe(values => {
            const jsonComments = values.json();
            for (let i = 0; i < values.json().length; i++) {
                this.Comments.push(
                    new UserComment(jsonComments[i].title, jsonComments[i].commentText, jsonComments[i].autor, jsonComments[i].postTime));
            }
            this.refreshComments();
        });
    }

    private startSignalR() {
        this._hubConnection = new HubConnection(this.baseUrl + 'commentsPublisher');
        this._hubConnection.on('Send', (newComment: any) => {
            this.Comments.push(
                new UserComment(newComment.title, newComment.commentText, newComment.autor, newComment.postTime));
            this.refreshComments();
        });
        this._hubConnection.start();
    }

    ngOnInit() {
        this.checkUser();
        //this.subscriptionName = this.authService.getLoggedInName.subscribe((item: string) => this.changeName(item));

        this.subscriptionName = this.authService.statusNameItem$.subscribe((item: string) => this.loggedUserName = item);

        this.getComments();
        this.startSignalR();

        this.control = document.getElementById('commentsShowArea');
    }

    refreshComments() {
        setTimeout(() => {
            this.control.scrollTop = this.control.scrollHeight;
        }, 1);
    }

    send() {
        this.checkUser();
        const comment = new UserComment(this.commentTitle, this.commentText, this.loggedUserName, Date.now());
        this._hubConnection.invoke('Send', comment);
    }

    private checkUser() {
        this.authService.checkUserName().toPromise().then((data: any) => {
            if (data.text() === 'false') {
                this.authService.logout();
            } else {
                this.loggedUserName = this.authService.getLoggedUserName();
            }
        });
    }
}
