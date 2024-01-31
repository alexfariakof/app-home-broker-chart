import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeModule } from './pages/home/home.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Acoesodule } from './pages/acoes/acoes.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomInterceptor } from './shared/interceptors/http.interceptor.service';
import { PeriodFilterComponent } from './shared/components';

@NgModule({
    declarations: [AppComponent, NavMenuComponent ],
    providers: [ { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, } ],
    bootstrap: [AppComponent],
    imports: [BrowserModule, HttpClientModule, FormsModule, HomeModule, Acoesodule, AppRoutingModule, BrowserAnimationsModule, PeriodFilterComponent, NgbModule ],
})
export class AppModule { }
