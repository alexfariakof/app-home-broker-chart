import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HomeComponent } from './home.component';
import { CommonModule } from '@angular/common';
import { LineChartModule, CandleChartModule } from '../../shared/charts';
import { ChartService } from '../../shared/services';
@NgModule({
  declarations: [HomeComponent],
  imports: [ BrowserModule,CommonModule, LineChartModule, CandleChartModule,  HttpClientModule],
  providers: [ChartService],
  exports: [HomeComponent]
})

export class HomeModule { }
