import { CandleChartComponent } from './candle.chart.component';
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgApexchartsModule } from "ng-apexcharts";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [CandleChartComponent],
  imports: [BrowserModule, CommonModule, NgApexchartsModule, FormsModule, ReactiveFormsModule],
  exports: [CandleChartComponent]
})

export class CandleChartModule {}
