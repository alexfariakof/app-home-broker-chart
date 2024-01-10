import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app.routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeModule } from './pages/home/home.module';
import { PeriodFilterModule } from './shared/components/period-filter/period.filter.module';

@NgModule({
  declarations: [ AppComponent, NavMenuComponent  ],
  imports: [ BrowserModule, HttpClientModule, FormsModule, HomeModule, AppRoutingModule, PeriodFilterModule ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
