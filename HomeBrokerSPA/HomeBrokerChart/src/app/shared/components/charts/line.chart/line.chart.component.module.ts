import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgApexchartsModule } from "ng-apexcharts";
import { LineChartComponent } from "./line.chart.component";
import { ChartService } from "../../../services";
@NgModule({
  declarations: [LineChartComponent ],
  imports: [BrowserModule, CommonModule, NgApexchartsModule, FormsModule, ReactiveFormsModule],
  providers: [ChartService],
  exports: [LineChartComponent]
})
export class LineChartModule {}
