import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { LineChartComponent } from "./line.chart.component";
import { CommonModule } from "@angular/common";
import { NgApexchartsModule } from "ng-apexcharts";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [LineChartComponent ],
  imports: [BrowserModule, CommonModule, NgApexchartsModule, FormsModule, ReactiveFormsModule],
  exports: [LineChartComponent]
})
export class LineChartModule {}
