import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';
import { ChartCandleOptions, seriesData } from '../../chart.options';
import { Period, SeriesDataLinear  } from '../../interfaces';
import { ChartService } from '../../services/api/chart.service';
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


  randomizeData = (originalData: any[]) => {
    return originalData.map(item => {
      const newItem = { ...item };
      if (Array.isArray(newItem.y)) {
        newItem.y = newItem.y.map((value: any) => Math.random() * (100 - 1) + 1);
      }

      return newItem;
    });
  }

  constructor(public chartService:ChartService) {}

  async ngOnInit(): Promise<void> {
    const period:Period = {
      StartDate: dayjs().add(-365,'days'),
      EndDate: dayjs()
    }

    const magazineLuizaHistoryPrices = await this.chartService.get(period.StartDate, period.EndDate);
    const seriesDataLinear: SeriesDataLinear[] = magazineLuizaHistoryPrices?.map(item => ({
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
            min: period.StartDate,
            max: period.EndDate
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
