import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { ChartService } from '../../shared/services';
import { LineChartModule, MacdChartModule } from 'src/app/shared/components';
@NgModule({
  declarations: [HomeComponent],
  imports: [ BrowserModule, CommonModule, LineChartModule, MacdChartModule,  HttpClientModule],
  providers: [ChartService],
  exports: [HomeComponent]
})

export class HomeModule { }
