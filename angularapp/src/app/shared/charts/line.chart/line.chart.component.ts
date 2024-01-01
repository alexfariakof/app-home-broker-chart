import { ChartLineOptions } from '../../chart.options/ChartLineOptions';
import { Component, ViewChild, OnInit } from '@angular/core';
import { ChartComponent }  from "ng-apexcharts";

@Component({
  selector: 'app-line-chart',
  templateUrl: './line.chart.component.html',
  styleUrls: ['./line.chart.component.css']
})

export class LineChartComponent implements OnInit{
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: ChartLineOptions | any;

  constructor() {

  }
  ngOnInit(): void {
    this.chartOptions = {
      series: [
        {
          name: "SMA",
          color: "#000",
          data: [10, 41, 35, 51, 49, 62, 69, 91, 148, 150, 100, 120]
        },
        {
          name: "EMA 9",
          data: [10, 30, 20, 80, 50, 74, 65, 88, 159, 150, 142, 190]
        },
        {
          name: "EMA 12",
          data: [9, 20, 10, 50, 70, 48, 60, 70, 100, 120, 160, 190]
        },
        {
          name: "EMA 26",
          data: [10, 30, 50, 58, 62, 74, 78, 60, 130, 200, 151, 190]
        },
      ],
      chart: {
        height: 350,
        type: "line",
        zoom: {
          enabled: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "straight"
      },
      title: {
        text: "MAGAZINE LUIZA S.A.",
        align: "left"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
          opacity: 0.5
        }
      },
      xaxis: {
        categories: [
          "Jan",
          "Feb",
          "Mar",
          "Apr",
          "May",
          "Jun",
          "Jul",
          "Aug",
          "Sep",
          "Out",
          "Nov",
          "Dez"
        ]
      }
    };
  }
}
