var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { Subject } from "rxjs/Subject";
import { SignalRConnectionStatus } from '../interfaces';
var FeedService = (function () {
    function FeedService(http) {
        this.http = http;
        this.currentState = SignalRConnectionStatus.Disconnected;
        this.connectionStateSubject = new Subject();
        this.setConnectionIdSubject = new Subject();
        this.updateMatchSubject = new Subject();
        this.addFeedSubject = new Subject();
        this.addChatMessageSubject = new Subject();
        this.connectionState = this.connectionStateSubject.asObservable();
        this.setConnectionId = this.setConnectionIdSubject.asObservable();
        this.updateMatch = this.updateMatchSubject.asObservable();
        this.addFeed = this.addFeedSubject.asObservable();
        this.addChatMessage = this.addChatMessageSubject.asObservable();
    }
    FeedService.prototype.start = function (debug) {
        var _this = this;
        $.connection.hub.logging = debug;
        var connection = $.connection;
        // reference signalR hub named 'broadcaster'
        var feedHub = connection.broadcaster;
        this.server = feedHub.server;
        // setConnectionId method called by server
        feedHub.client.setConnectionId = function (id) { return _this.onSetConnectionId(id); };
        // updateMatch method called by server
        feedHub.client.updateMatch = function (match) { return _this.onUpdateMatch(match); };
        // addFeed method called by server
        feedHub.client.addFeed = function (feed) { return _this.onAddFeed(feed); };
        feedHub.client.addChatMessage = function (chatMessage) { return _this.onAddChatMessage(chatMessage); };
        // start the connection
        $.connection.hub.start()
            .done(function (response) { return _this.setConnectionState(SignalRConnectionStatus.Connected); })
            .fail(function (error) { return _this.connectionStateSubject.error(error); });
        return this.connectionState;
    };
    FeedService.prototype.setConnectionState = function (connectionState) {
        console.log('connection state changed to: ' + connectionState);
        this.currentState = connectionState;
        this.connectionStateSubject.next(connectionState);
    };
    // Client side methods
    FeedService.prototype.onSetConnectionId = function (id) {
        this.setConnectionIdSubject.next(id);
    };
    FeedService.prototype.onUpdateMatch = function (match) {
        this.updateMatchSubject.next(match);
    };
    FeedService.prototype.onAddFeed = function (feed) {
        console.log(feed);
        this.addFeedSubject.next(feed);
    };
    FeedService.prototype.onAddChatMessage = function (chatMessage) {
        this.addChatMessageSubject.next(chatMessage);
    };
    // Server side methods
    FeedService.prototype.subscribeToFeed = function (matchId) {
        this.server.subscribe(matchId);
    };
    FeedService.prototype.unsubscribeFromFeed = function (matchId) {
        this.server.unsubscribe(matchId);
    };
    return FeedService;
}());
FeedService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [Http])
], FeedService);
export { FeedService };
//# sourceMappingURL=feed.service.js.map