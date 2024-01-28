import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { ChartService } from '../../shared/services';
import { LineChartModule, CandleChartModule } from '../../shared/components/charts';
import { MacdChartModule } from 'src/app/shared/components/charts/macd-chart/macd.chart.module';
@NgModule({
  declarations: [HomeComponent],
  imports: [ BrowserModule,CommonModule, LineChartModule, CandleChartModule, MacdChartModule,  HttpClientModule],
  providers: [ChartService],
  exports: [HomeComponent]
})

export class HomeModule { }
