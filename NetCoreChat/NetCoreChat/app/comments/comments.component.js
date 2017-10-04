var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input } from '@angular/core';
import { FeedService } from '../services/feed.service';
var ChatComponent = (function () {
    function ChatComponent(feedService) {
        this.feedService = feedService;
    }
    ChatComponent.prototype.ngOnInit = function () {
        var self = this;
        self.feedService.addChatMessage.subscribe(function (message) {
            console.log('received..');
            console.log(message);
            if (!self.messages)
                self.messages = new Array();
            self.messages.unshift(message);
        });
    };
    return ChatComponent;
}());
__decorate([
    Input(),
    __metadata("design:type", Array)
], ChatComponent.prototype, "matches", void 0);
__decorate([
    Input(),
    __metadata("design:type", String)
], ChatComponent.prototype, "connection", void 0);
ChatComponent = __decorate([
    Component({
        selector: 'chat',
        templateUrl: 'app/shared/components/chat.component.html'
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof FeedService !== "undefined" && FeedService) === "function" && _a || Object])
], ChatComponent);
export { ChatComponent };
var _a;
//# sourceMappingURL=comments.component.js.map