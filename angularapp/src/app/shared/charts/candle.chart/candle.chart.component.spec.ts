import { SeriesData } from './../../interfaces/SeriesData';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CandleChartComponent } from './candle.chart.component';
import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgApexchartsModule } from 'ng-apexcharts';
import { ChartService } from '../../services';
import { seriesData } from '../../chart.options';

describe('CandleChartComponent', () => {
  let component: CandleChartComponent;
  let fixture: ComponentFixture<CandleChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CandleChartComponent ],
      imports: [HttpClientTestingModule, BrowserModule, CommonModule, NgApexchartsModule, FormsModule],
      providers: [ChartService]
    })
    .compileComponents();
    fixture = TestBed.createComponent(CandleChartComponent);
    component = fixture.componentInstance;
    component.chartBarOptions ={
      series: [
        {
          name: "Candle",
          color: "#FFF",
          type: "bar",
          data: [0]
        },
        {
          name: "MACD 12",
          type: "line",
          data: seriesData
        },
        {
          name: "MACD 62",
          type: "line",
          data: seriesData
        },
        {
          name: "MACD 9",
          type: "line",
          data: seriesData
        }
      ],
      chart: {
        height: 300,
        type: "bar",
        selection: {
          enabled: true,
          xaxis: {
            min: 0,
            max: 1000
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
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
