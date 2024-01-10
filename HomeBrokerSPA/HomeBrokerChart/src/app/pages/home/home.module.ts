import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HomeComponent } from './home.component';
import { CommonModule } from '@angular/common';
import { ChartService } from '../../shared/services';
import { LineChartModule, CandleChartModule } from 'src/app/shared/components/charts';

@NgModule({
  declarations: [HomeComponent],
  imports: [ BrowserModule,CommonModule, LineChartModule, CandleChartModule,  HttpClientModule],
  providers: [ChartService],
  exports: [HomeComponent]
})

export class HomeModule { }
