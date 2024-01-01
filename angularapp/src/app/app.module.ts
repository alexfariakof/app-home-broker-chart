import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LineChartModule } from './shared/charts/line.chart/line.chart.component.module';
import { NgApexchartsModule } from 'ng-apexcharts';
import { CandleChartModule } from './shared/charts/candle.chart/candle.chart.component.module';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, LineChartModule, CandleChartModule, NgApexchartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
