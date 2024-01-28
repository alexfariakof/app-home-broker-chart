import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgApexchartsModule } from "ng-apexcharts";
import { ChartService } from '../../../services';
import { MacdChartComponent } from "./macd.chart.component";
@NgModule({
  declarations: [MacdChartComponent],
  imports: [BrowserModule, CommonModule, NgApexchartsModule, FormsModule, ReactiveFormsModule],
  providers: [ChartService],
  exports: [MacdChartComponent]
})

export class MacdChartModule {}
