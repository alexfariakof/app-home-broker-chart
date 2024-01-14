import { Component, ViewChild } from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';
import { IMagazineLuizaHistoryPrice, ISeriesDataLinear } from 'src/app/shared/interfaces';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
import { ChartService } from 'src/app/shared/services';
import { ChartCandleOptions } from '../chart.options';

@Component({
  selector: 'app-macd-chart',
  templateUrl: './macd.chart.component.html',
  styleUrls: ['./macd.chart.component.css']
})
export class MacdChartComponent {
  @ViewChild("chart") chart!: ChartComponent;
  public chartMacdOptions: ChartCandleOptions |  any;
    public magazineLuizaHistoryPrices?: IMagazineLuizaHistoryPrice[];

  constructor(public chartService:ChartService, public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) {}

  async ngOnInit(): Promise<void> {
    this.magazineLuizaHistoryPrices = await this.chartService.get(this.obsStartDate.startDate, this.obsEndDate.endDate);
    const labelXAxis: any[] = this.magazineLuizaHistoryPrices?.map(item =>  item.date) || [];
    const macd = await this.chartService.getMACD(this.obsStartDate.startDate, this.obsEndDate.endDate);
    this.chartMacdOptions = {
      series: [
        {
          name: "Histograma",
          type: "bar",
          data:  macd.histogram,
        },
        {
          name: "MACD Line",
          type: "line",
          data: macd.macdLine
        },
        {
          name: "Signal",
          type: "line",
          data: macd.signal
        },
      ],
      chart: {
        height: (document.body.clientHeight/2)-16,
        type: "bar",
        selection: {
          enabled: true,
          xaxis: {
            min: this.obsStartDate.startDate,
            max: this.obsEndDate.endDate
          },
          fill: {
            color: "#ccc",
            opacity: 0.4
          },
          stroke: {
            width: [2, 2, 4]
          }
        }
      },
      dataLabels: {
        enabled: false,
        enabledOnSeries: [0]
      },
      plotOptions: {
        bar: {
          columnWidth: "80%",
          colors: {
            ranges: [
              {
                from: -100000,
                to: 0,
                color: "#F15B46"
              },
              {
                from: 1,
                to: 1000000,
                color: "#FEB019"
              }
            ]
          }
        }
      },
      stroke: {
        width: [4, 2, 2]
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"],
          opacity: 0.5
        }
      },
      xaxis: {
        type: "datetime",
        categories: labelXAxis,

      },
      yaxis: [
        {
          opposite: true,
          seriesName: "Histograma",
          title: {
            text: "Histograma",
            style: {
              color: "#000"
            }
          },
          labels: {
            show: false
          }
        },
        {
          seriesName: "MACD Line",
          axisTicks: {
            show: true
          },
          labels: {
            show: false
          },
        },
        {
          seriesName: "Signal",
          axisTicks: {
            show: true
          },
          labels: {
            show: false
          },
        },
      ],

    };
  }
}