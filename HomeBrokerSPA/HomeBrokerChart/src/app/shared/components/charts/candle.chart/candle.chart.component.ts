import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';
import { ChartCandleOptions, seriesData } from '../chart.options';
import { IPeriod, ISeriesDataLinear } from 'src/app/shared/interfaces';
import { ChartService } from 'src/app/shared/services';
import * as dayjs from 'dayjs';

@Component({
  selector: 'app-candle-chart',
  templateUrl: './candle.chart.component.html',
  styleUrls: ['./candle.chart.component.css']
})

export class CandleChartComponent implements OnInit {
  @ViewChild("chart") chart!: ChartComponent;
  public chartCandleOptions: ChartCandleOptions | any;
  public chartBarOptions: ChartCandleOptions | any;
  public period:IPeriod = {
    StartDate: dayjs().add(-365,'days').format("YYYY-MM-DD"),
    EndDate: dayjs().format("YYYY-MM-DD")
  }

  randomizeData = (originalData: any[]) => {
    return originalData.map(item => {
      const newItem = { ...item };
      if (Array.isArray(newItem.y)) {
        newItem.y = newItem.y.map((value: any) => Math.random() * (100 - 1) + 1);
      }

      return newItem;
    });
  }

  constructor(public chartService:ChartService) {
    this.period = {
      StartDate: dayjs().add(-365,'days'),
      EndDate: dayjs()
    };
  }

  async ngOnInit(): Promise<void> {

    const magazineLuizaHistoryPrices = await this.chartService.get(this.period.StartDate, this.period.EndDate);
    const seriesDataLinear: ISeriesDataLinear[] = magazineLuizaHistoryPrices?.map(item => ({
      x: item.date,
      y: item.close >= item.open ? item.open : -item.close
    })) || [];

      this.chartCandleOptions = {
      series: [
        {
          name: "candle",
          data: seriesData
        }
      ],
      chart: {
        type: "candlestick",
        height: 290,
        id: "candles",
        toolbar: {
          autoSelected: "pan",
          show: false
        },
        zoom: {
          enabled: false
        }
      },
      plotOptions: {
        candlestick: {
          colors: {
            upward: "#5dd55d",
            downward: "#F15B46"
          }
        }
      },
      xaxis: {
        type: "datetime"
      }
    };
    this.chartBarOptions = {
      series: [
        {
          name: "Candle",
          color: "#FFF",
          type: "bar",
          data: seriesDataLinear
        },
        {
          name: "MACD 12",
          type: "line",
          data: this.randomizeData(seriesData)
        },
        {
          name: "MACD 62",
          type: "line",
          data: this.randomizeData(seriesData)
        },
        {
          name: "MACD 9",
          type: "line",
          data: this.randomizeData(seriesData)
        }
      ],
      chart: {
        height: 300,
        type: "bar",
        selection: {
          enabled: true,
          xaxis: {
            min: this.period.StartDate,
            max: this.period.EndDate
          },
          fill: {
            color: "#ccc",
            opacity: 0.4
          },
          stroke: {
            color: "#5dd55d"
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
                to: 100000,
                color: "#5dd55d"
              }
            ]
          }
        }
      },
      stroke: {
        width: [-2, 5, 6],
      },
      xaxis: {
        type: "datetime",
        axisBorder: {
          offsetX: 13
        }
      },
      yaxis: {
        labels: {
          show: false
        }
      }
    };
  }
}
