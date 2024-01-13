import { Component, ViewChild, OnInit } from '@angular/core';
import { ChartComponent }  from "ng-apexcharts";
import { ChartLineOptions } from '../chart.options/ChartLineOptions';
import { ChartService } from '../../../services';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
@Component({
  selector: 'app-line-chart',
  templateUrl: './line.chart.component.html',
  styleUrls: ['./line.chart.component.css']
})

export class LineChartComponent implements OnInit{
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: ChartLineOptions | any;

  constructor(public chartService:ChartService,public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) {}

  ngOnInit(): void {
    this.initializeComponent();
  }

  public initializeComponent = async ():Promise<void> =>{
    const smaData = await this.chartService.getSMA(this.obsStartDate.startDate, this.obsEndDate.endDate);
    const ema9Data = await this.chartService.getEMA(9,this.obsStartDate.startDate, this.obsEndDate.endDate);
    const ema12Data = await this.chartService.getEMA(12,this.obsStartDate.startDate, this.obsEndDate.endDate);
    const ema26Data = await this.chartService.getEMA(26,this.obsStartDate.startDate, this.obsEndDate.endDate);
    this.chartOptions = {
      series: [
        {
          name: "SMA",
          color: "#000",
          data: this.formatData(smaData)
        },
        {
          name: "EMA 9",
          data: this.formatData(ema9Data)
        },
        {
          name: "EMA 12",
          data: this.formatData(ema12Data)
        },
        {
          name: "EMA 26",
          data: this.formatData(ema26Data)
        },
      ],
      chart: {
        height: (document.body.clientHeight/3)-16,
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
        labels: {
          show: false,
        },
      },
      yaxis: [
        {
          opposite: true,

        },
      ]
    };
  }

  formatData(data: number[]): number[] {
    return data.map(value => parseFloat(value.toFixed(2)));
  }
}
