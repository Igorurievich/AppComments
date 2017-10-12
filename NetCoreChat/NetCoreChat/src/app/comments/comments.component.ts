import { Component, OnInit } from '@angular/core';
import { AuthService } from "angular4-social-login";
import { SocialUser } from "angular4-social-login";

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

    counter: number = 0;
    constructor() {
        this.counter++;
    }

  ngOnInit() {
      this.counter++;


  }
}
