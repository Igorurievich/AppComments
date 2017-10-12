import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms'
import { HttpModule } from '@angular/http';
import { CommentsComponent } from './comments/comments.component';
import { AdminComponent } from './admin/admin.component';
import { RouterModule, Routes } from '@angular/router';
import { SocialLoginModule } from "angular4-social-login";
import { FacebookLoginProvider } from "angular4-social-login";
import { AuthServiceConfig } from "angular4-social-login";
import { LoginComponent } from './login/login.component';
import { AuthenticationService } from "./services/auth/auth.service";
import { SignupComponent } from './signup/signup.component';

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
    AdminComponent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpModule,
      RouterModule.forRoot([
          { path: 'comments', component: CommentsComponent },
          { path: 'admin', component: AdminComponent },
          { path: 'login', component: LoginComponent },
          { path: 'signup', component: SignupComponent }
      ]),
      SocialLoginModule
  ],
  providers: [
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    },
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
