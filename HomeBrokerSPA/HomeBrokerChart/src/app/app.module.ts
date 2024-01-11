import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app.routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeModule } from './pages/home/home.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PeriodFilterComponent } from './shared/components';
import { Acoesodule } from './pages/acoes/acoes.module';
@NgModule({
    declarations: [AppComponent, NavMenuComponent],
    providers: [],
    bootstrap: [AppComponent],
    imports: [BrowserModule, HttpClientModule, FormsModule, HomeModule, Acoesodule,
        AppRoutingModule, BrowserAnimationsModule, PeriodFilterComponent]
})
export class AppModule { }
