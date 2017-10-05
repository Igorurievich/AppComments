import { Component, OnInit } from '@angular/core';

import { FeedService } from './services/feed.service';
import { SignalRConnectionStatus } from './interfaces';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    providers: [FeedService]
})
export class AppComponent implements OnInit {

    constructor(private service: FeedService) { }

    ngOnInit() {
        this.service.start(true).subscribe();
    }
}