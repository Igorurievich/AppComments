import { Component, OnInit } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { AuthenticationService } from '../services/auth/auth.service';
import { Http } from '@angular/http';
import { UserComment } from './UserComment';
import { User } from '../login/User';
import { HubConnection } from "@aspnet/signalr-client/dist/src";

@Component({
    selector: 'app-comments',
    templateUrl: './comments.component.html',
    styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

    private loggedUserName = '';

    private _hubConnection: HubConnection;
    public async: any;
    messages: string[] = [];

    private commentTitle: string;
    private commentText: string;
    private commentAutor: string;

    private Comments: Array<UserComment> = new Array<UserComment>();

    constructor(private authService: AuthenticationService,
        private httpService: Http) {
        this.checkUser();
        this.authService.getLoggedInName.subscribe(name => this.changeName(name));
    }

    private changeName(name: string): void {
        this.loggedUserName = name;
    }

    ngOnInit() {
        this.httpService.get('/api/comments/GetAllComments').subscribe(values => {
            this.Comments = values.json() as Array<UserComment>;
            console.log(this.Comments);
        });

        this._hubConnection = new HubConnection('http://localhost:5000/commentsPublisher');
        this._hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });

        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started');
            })
            .catch(err => {
                console.log('Error while establishing connection', err);
            });
    }

    send() {
        this.checkUser();
        const comment = new UserComment(this.commentTitle, this.commentText, this.loggedUserName, Date.now());
        console.log(comment);
        this.httpService.post('/api/comments/NewComment', comment).subscribe(res => {
            console.log(res.status);
        });

        this._hubConnection.invoke('Send', this.commentText);
        this.messages.push(this.commentText);

        console.log(this.messages);
    }

    private checkUser() {
        this.authService.checkUserName().subscribe(data => {
            console.log(data.text());
            if (data.text() === 'false') {
                this.authService.logout();
            }
            else {
                this.loggedUserName = this.authService.getLoggedUserName();
            }
        });
    }
}
