import { ChartLineOptions } from '../../chart.options/ChartLineOptions';
import { Component, ViewChild, OnInit } from '@angular/core';
import { ChartComponent }  from "ng-apexcharts";
import { ChartService } from '../../services/api/chart.service';
@Component({
  selector: 'app-line-chart',
  templateUrl: './line.chart.component.html',
  styleUrls: ['./line.chart.component.css']
})

export class LineChartComponent implements OnInit{
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: ChartLineOptions | any;

  constructor(public chartService:ChartService) {}

  async ngOnInit(): Promise<void> {
    const smaData = await this.chartService.getSMA();
    const ema9Data = await this.chartService.getEMA(9);
    const ema12Data = await this.chartService.getEMA(12);
    const ema26Data = await this.chartService.getEMA(26);
    this.chartOptions = {
      series: [
        {
          name: "SMA",
          color: "#000",
          data: this.formatData(smaData)
        },
        {
          name: "EMA 9",
          data: this.formatData(smaData)
        },
        {
          name: "EMA 12",
          data: this.formatData(smaData)
        },
        {
          name: "EMA 26",
          data: this.formatData(smaData)
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
        labels: {
          show: false,
        },
      },
    };
  }

  private formatData(data: number[]): number[] {
    return data.map(value => parseFloat(value.toFixed(2)));
  }
}