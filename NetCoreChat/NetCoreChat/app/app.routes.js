import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
var appRoutes = [
    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent }
];
export var routing = RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routes.js.map