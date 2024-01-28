import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgApexchartsModule } from "ng-apexcharts";
import { CandleChartComponent } from './candle.chart.component';
import { ChartService } from '../../../services';
@NgModule({
  declarations: [CandleChartComponent],
  imports: [BrowserModule, CommonModule, NgApexchartsModule, FormsModule, ReactiveFormsModule],
  providers: [ChartService],
  exports: [CandleChartComponent]
})

export class CandleChartModule {}
