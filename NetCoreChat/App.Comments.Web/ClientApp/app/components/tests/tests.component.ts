import { Component, OnInit, PLATFORM_ID, Inject } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { Http } from '@angular/http';
import { User } from '../login/User';
import { HubConnection } from '@aspnet/signalr-client/dist/src';
import { AuthenticationService } from "../../services/auth/auth.service";
import { isPlatformServer, isPlatformBrowser } from '@angular/common';
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'app-tests',
    templateUrl: './tests.component.html',
    styleUrls: ['./tests.component.css']
})
export class TestsComponent implements OnInit {
    constructor(private httpService: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    FindStringInTextTestTime: number;
    ParseJsonObjectTime: number;
    ResizeImageTime: number;
    ZipFilesTime: number;

    InsertTime: number;
    SelectTime: number;
    DeleteTime: number;

    BigSizeGausTime: number;
    LittleSizeGausTime: number;

    BigSizeImageResizeTime: number;
    LittleSizeImageResizeTime: number;

    private baseUrl: string;

    ngOnInit(): void {
        
    }

    runCountSQLQueriesGeneratingTime() {
        this.httpService.get(this.baseUrl + 'api/tests/CountSQLQueriesGeneratingTime').toPromise().then(res => {
            const result = res.json();
            this.InsertTime = result[0];
            this.SelectTime = result[1];
            this.DeleteTime = result[2];
        });
    }

    runFindStringInText() {
        this.httpService.get(this.baseUrl + 'api/tests/FindStringInText').toPromise().then(res => {
            this.FindStringInTextTestTime = +res.text();
        });
    }
    runParseJsonObject() {
        this.httpService.get(this.baseUrl + 'api/tests/ParseJsonObject').toPromise().then(res => {
            this.ParseJsonObjectTime = +res.text();
        });
    }
    runResizesTests() {
        this.httpService.get(this.baseUrl + 'api/tests/ResizeImagesTests').toPromise().then(res => {
            const result = res.json();
            this.BigSizeImageResizeTime = result[0];
            this.LittleSizeImageResizeTime = result[1];
        });
    }
    runZipFiles() {
        this.httpService.get(this.baseUrl + 'api/tests/ZipFiles').toPromise().then(res => {
            this.ZipFilesTime = +res.text();
        });
    }
    runGausesTests() {
        this.httpService.get(this.baseUrl + 'api/tests/RunGausTests').toPromise().then(res => {
            const result = res.json();
            this.BigSizeGausTime = result[0];
            this.LittleSizeGausTime = result[1];
        });
    }
}
