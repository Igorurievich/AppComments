var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { FeedService } from '../services/feed.service';
import { DataService } from '../services/data.service';
import { SignalRConnectionStatus } from '../interfaces';
var HomeComponent = (function () {
    function HomeComponent(dataService, feedService) {
        this.dataService = dataService;
        this.feedService = feedService;
    }
    HomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        var self = this;
        self.listenForConnection();
        self.feedService.connectionState
            .subscribe(function (connectionState) {
            if (connectionState == SignalRConnectionStatus.Connected) {
                console.log('Connected!');
                self.loadMatches();
            }
            else {
                console.log(connectionState.toString());
            }
        }, function (error) {
            _this.error = error;
            console.log(error);
        });
    };
    HomeComponent.prototype.loadMatches = function () {
        var self = this;
        this.dataService.getMatches()
            .subscribe(function (res) {
            self.matches = res;
            // Listen for match score updates...
            self.feedService.updateMatch.subscribe(function (match) {
                for (var i = 0; i < self.matches.length; i++) {
                    if (self.matches[i].Id === match.Id) {
                        self.matches[i].HostScore = match.HostScore;
                        self.matches[i].GuestScore = match.GuestScore;
                        if (match.HostScore === 0 && match.GuestScore === 0)
                            self.matches[i].Feeds = new Array();
                    }
                }
            });
            // Listen for subscribed feed updates..
            self.feedService.addFeed.subscribe(function (feed) {
                console.log(feed);
                for (var i = 0; i < self.matches.length; i++) {
                    if (self.matches[i].Id === feed.MatchId) {
                        if (!self.matches[i].Feeds) {
                            self.matches[i].Feeds = new Array();
                        }
                        self.matches[i].Feeds.unshift(feed);
                    }
                }
            });
        }, function (error) {
            console.log(error);
        });
    };
    HomeComponent.prototype.listenForConnection = function () {
        var self = this;
        // Listen for connected / disconnected events
        self.feedService.setConnectionId.subscribe(function (id) {
            console.log(id);
            self.connectionId = id;
        });
    };
    HomeComponent.prototype.updateSubscription = function (subscription) {
        if (subscription.subscribe === true)
            this.feedService.subscribeToFeed(subscription.matchId);
        else
            this.feedService.unsubscribeFromFeed(subscription.matchId);
    };
    return HomeComponent;
}());
HomeComponent = __decorate([
    Component({
        selector: 'home',
        templateUrl: 'app/home/home.component.html'
    }),
    __metadata("design:paramtypes", [DataService,
        FeedService])
], HomeComponent);
export { HomeComponent };
//# sourceMappingURL=home.component.js.map