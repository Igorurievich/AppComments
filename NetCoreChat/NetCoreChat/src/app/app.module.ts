import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpModule } from '@angular/http';
import { CommentsComponent } from './comments/comments.component';
import { AdminComponent } from './admin/admin.component';
import { RouterModule, Routes } from '@angular/router';
import { SocialLoginModule } from "angular4-social-login";

import { AuthServiceConfig } from "angular4-social-login";
import { LoginComponent } from './login/login.component';
import { AuthenticationService } from "./services/auth/auth.service";
import { SignupComponent } from './signup/signup.component';
import { AuthguardGuard } from "./authguard.guard";
import { FacebookLoginProvider } from "angular4-social-login";

const appRoutes: Routes = [
    {
        path: 'comments',
        canActivate: [AuthguardGuard],
        component: CommentsComponent
    },
    {
        path: 'admin',
        canActivate: [AuthguardGuard],
        component: AdminComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'signup',
        component: SignupComponent
    }
]

let config = new AuthServiceConfig([
    {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider("473399536359340")
    }
]);

@NgModule({
  declarations: [
    AppComponent,
    CommentsComponent,
    AdminComponent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
      RouterModule.forRoot(appRoutes),
      BrowserModule,
      FormsModule,
      HttpModule,
      SocialLoginModule,
      ReactiveFormsModule,
      FormsModule
  ],
  providers: [
      AuthenticationService,
      AuthguardGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
