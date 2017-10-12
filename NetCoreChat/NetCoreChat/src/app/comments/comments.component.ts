import { Component, OnInit } from '@angular/core';
import { AuthService } from "angular4-social-login";
import { SocialUser } from "angular4-social-login";

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

    private user: SocialUser;
    private loggedIn: boolean;

    counter: number = 0;
    constructor(private authService: AuthService) {
        this.counter++;
    }

  ngOnInit() {
      this.counter++;

      this.authService.authState.subscribe((user) => {
          this.user = user;
          this.loggedIn = (user != null);
      });
  }
}
