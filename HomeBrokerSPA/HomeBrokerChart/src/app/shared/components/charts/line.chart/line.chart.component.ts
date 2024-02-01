import { CustomValidators } from './../../../validators/custom.validators';
import { Component, ViewChild, OnInit } from '@angular/core';
import { ChartComponent } from "ng-apexcharts";
import { ChartService } from '../../../services';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
import { ChartCommonOptions, ChartOptions } from '../chart.options';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line.chart.component.html',
  styleUrls: ['./line.chart.component.css']
})

export class LineChartComponent implements OnInit {
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: ChartOptions | any;

  constructor(public chartService: ChartService, public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) { }

  ngOnInit(): void {
    if (CustomValidators.IsValidPeriod(this.obsStartDate.startDate.toString(), this.obsEndDate.endDate.toString()))
      this.initializeComponent();
    else
      this.chartOptions = ChartCommonOptions.DEFAULT_CHART_OPTIONS;
  }

  public initializeComponent = async (): Promise<void> => {
    const smaData = await this.chartService.getSMA(this.obsStartDate.startDate, this.obsEndDate.endDate);
    const ema9Data = await this.chartService.getEMA(9, this.obsStartDate.startDate, this.obsEndDate.endDate);
    const ema12Data = await this.chartService.getEMA(12, this.obsStartDate.startDate, this.obsEndDate.endDate);
    const ema26Data = await this.chartService.getEMA(26, this.obsStartDate.startDate, this.obsEndDate.endDate);

    const data = [
      { name: "EMA 9", data: ChartCommonOptions.formatData(ema9Data.values) || [] },
      { name: "EMA 12", data: ChartCommonOptions.formatData(ema12Data.values) || [] },
      { name: "EMA 26", data: ChartCommonOptions.formatData(ema26Data.values) || [] },
      { name: "SMA", color: "#000", data: ChartCommonOptions.formatData(smaData.values) || [] },
    ];

    this.chartOptions = {
      chart: {
        height: (document.body.clientHeight / 3) - 16,
        type: "area",
        zoom: {
          type: "x",
          enabled: true,
          autoScaleYaxis: true
        },
        toolbar: {
          autoSelected: "zoom"
        },
        markers: {
          size: 0
        },
      },
      fill: {
        type: "gradient",
        gradient: {
          shadeIntensity: 1,
          inverseColors: false,
          opacityFrom: 0.5,
          opacityTo: 0,
          stops: [1, 2, 4, 8]
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        width: [2],
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
    ChartCommonOptions.initializeChartData(this.chartOptions, data, []);
  }
}
