import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { HubConnection } from '@aspnet/signalr-client';
import { AuthenticationService } from './services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    isLogged: boolean;
    userName = '';

    private _hubConnection: HubConnection;
    public async: any;
    messages: string[] = [];

    constructor(private authService: AuthenticationService, private router: Router) {
        this.authService.getLoggedInName.subscribe(name => this.changeName(name));
        this.authService.getLoggedInStatus.subscribe(status => this.changeStatus(status));
        this.userName = this.authService.getLoggedUserName();
        if (this.userName.length > 0) {
            this.isLogged = true;
        }
    }

    private changeName(name: string): void {
        this.userName = name;
    }

    private changeStatus(status: boolean): void {
        this.isLogged = status;
    }

    ngOnInit() {
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

    logOut() {
        this.authService.logout();
        this.router.navigate(['login']);
    }
}
