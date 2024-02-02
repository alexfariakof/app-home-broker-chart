import { Component, ViewChild } from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';
import { IMagazineLuizaHistoryPrice } from 'src/app/shared/interfaces';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
import { ChartService } from 'src/app/shared/services';
import { CustomValidators } from 'src/app/shared/validators';
import { ChartOptions, ChartCommonOptions } from '../chart.options';

@Component({
  selector: 'app-macd-chart',
  templateUrl: './macd.chart.component.html',
  styleUrls: ['./macd.chart.component.css']
})

export class MacdChartComponent {
  @ViewChild("chart") chart!: ChartComponent;
  public chartMacdOptions: ChartOptions | any;
  public magazineLuizaHistoryPrices: IMagazineLuizaHistoryPrice[] = [];

  constructor(public chartService: ChartService, public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) { }

  async ngOnInit(): Promise<void> {
    if (CustomValidators.IsValidPeriod(this.obsStartDate.startDate.toString(), this.obsEndDate.endDate.toString())) {
      this.initializeComponent();
    } else {
      this.chartMacdOptions = ChartCommonOptions.DEFAULT_CHART_OPTIONS;
    }
  }

  public initializeComponent = async (): Promise<void> => {
    this.magazineLuizaHistoryPrices = await this.chartService.get(this.obsStartDate.startDate, this.obsEndDate.endDate);
    const macd = await this.chartService.getMACD(this.obsStartDate.startDate, this.obsEndDate.endDate);
    const data = [
      { name: "Histograma", type: "bar", data: macd.histogram },
      { name: "MACD Line", type: "line", color: "#000", data: macd.macdLine },
      { name: "Signal", type: "line", color: "#FFA500", data: macd.signal },
    ];

    const labelXAxis: any[] = [];
    macd.macdLine.forEach((value: any, index: number) => {
      labelXAxis.push(this.magazineLuizaHistoryPrices[index].date);
    });
    this.chartMacdOptions = {
      chart: {
        height: (document.body.clientHeight / 2) - 16,
        type: "line",
        selection: {
          enabled: true,
          xaxis: {
            min: Math.min(...labelXAxis),
            max: Math.max(...labelXAxis)
          },
          fill: {
            color: "#ccc",
            opacity: 0.4
          },
          stroke: {
            width: [4, 2, 4],

          }
        }
      },
      dataLabels: {
        enabled: false,
        enabledOnSeries: [0, 0, 0]
      },
      plotOptions: {
        bar: {
          //distributed: true,
          columnWidth: "80%",
          borderRadius: 3,
          colors: {
            ranges: [
              {
                from: -Number.MAX_VALUE,
                to: 0,
                color: '#FF0000',
              },
              {
                from: 0,
                to: Number.MAX_VALUE,
                color: '#229A00',
              },
            ],
          },
        },
      },
      stroke: {
        width: [12, 2, 2],
        curve: "straight",
        colors: [
          {
            ranges: [
              {
                from: -Number.MAX_VALUE,
                to: 0,
                color: '#FF0000',
              },
              {
                from: 0,
                to: Number.MAX_VALUE,
                color: '#229A00',
              },
            ],
          },
          '#000',
          '#FFA500'
        ],
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
          axisTicks: {
            show: true
          },
          labels: {
            show: false
          },
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
    ChartCommonOptions.initializeChartData(this.chartMacdOptions, data, labelXAxis);
  }
}
