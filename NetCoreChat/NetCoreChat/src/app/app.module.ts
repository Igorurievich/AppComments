import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms'
import { HttpModule } from '@angular/http';
import { CommentsComponent } from './comments/comments.component';
import { AdminComponent } from './admin/admin.component';
import { RouterModule, Routes } from '@angular/router';
import { SocialLoginModule, AuthServiceConfig } from "angular4-social-login";
import { GoogleLoginProvider, FacebookLoginProvider } from "angular4-social-login";

let config = new AuthServiceConfig([
    {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider("473399536359340")
    }
]);

export function provideConfig() {
    return config;
}

@NgModule({
  declarations: [
    AppComponent,
    CommentsComponent,
    AdminComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpModule,
      RouterModule.forRoot([
          { path: 'comments', component: CommentsComponent },
          { path: 'admin', component: AdminComponent }
      ]),
      SocialLoginModule.initialize(config)
  ],
  providers: [
      {
          provide: AuthServiceConfig,
          useFactory: provideConfig
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
