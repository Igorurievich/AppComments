import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { LoginComponent } from "./components/login/login.component";
import { AuthServiceConfig } from 'angular4-social-login';
import { AuthenticationService } from './services/auth/auth.service';
import { SocialLoginModule } from "angular4-social-login";
import { FacebookLoginProvider } from "angular4-social-login";
import { SignupComponent } from "./components/signup/signup.component";
import { CommentsComponent } from "./components/comments/comments.component";
import { AuthguardGuard } from "./authguard.guard";

const config = new AuthServiceConfig([
    {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider('473399536359340')
    }
]);

export function provideConfig() {
    return config;
}

const appRoutes: Routes = [
    {
        path: 'comments',
        canActivate: [AuthguardGuard],
        component: CommentsComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'signup',
        component: SignupComponent
    }
];

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        LoginComponent,
        SignupComponent,
        CommentsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        SocialLoginModule,
        RouterModule.forRoot(appRoutes)
    ],
    providers: [
        AuthenticationService,
        {
            provide: AuthServiceConfig,
            useFactory: provideConfig
        },
        AuthguardGuard
    ]
})
export class AppModuleShared {
}
