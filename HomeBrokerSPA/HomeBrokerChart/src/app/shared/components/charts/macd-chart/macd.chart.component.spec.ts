import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MacdChartComponent } from './macd.chart.component';
import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PeriodStartDateObservable, PeriodEndDateObservable } from '../../../../shared/observables';
import { ChartService } from '../../../../shared/services';
import { mockMACD } from '../chart.options';
import * as dayjs from 'dayjs';

describe('MacdChartComponent', () => {
  let component: MacdChartComponent;
  let fixture: ComponentFixture<MacdChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MacdChartComponent ],
      imports: [HttpClientTestingModule, BrowserModule, CommonModule, NgApexchartsModule, FormsModule],
      providers: [ ChartService, PeriodStartDateObservable, PeriodEndDateObservable ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MacdChartComponent);
    component = fixture.componentInstance;
    component.chartMacdOptions ={
      series: [
        {
          name: "Histograma",
          type: "bar",
          data:  mockMACD.histogram,
        },
        {
          name: "MACD Line",
          type: "line",
          data: mockMACD.macdLine
        },
        {
          name: "Signal",
          type: "line",
          data: mockMACD.signal
        },
      ],
      chart: {
        height: 100,
        type: "bar",
        selection: {
          enabled: true,
          xaxis: {
            min:  dayjs().add(-1, 'year').format("YYYY-MM-DD"),
            max: dayjs().format("YYYY-MM-DD")
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
        categories: [2001, 2004],

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
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
