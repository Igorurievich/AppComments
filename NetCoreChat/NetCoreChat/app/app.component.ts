import { Component, OnInit } from '@angular/core';

import { SignalRConnectionStatus } from './shared/interfaces';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    providers: [FeedService]
})
export class AppComponent implements OnInit {

    constructor() { }

    ngOnInit() {
        this.service.start(true).subscribe(
            null,
            error => console.log('Error on init: ' + error));
    }
}