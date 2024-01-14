import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgApexchartsModule } from 'ng-apexcharts';
import { ChartService } from '../../../services';
import { CandleChartComponent } from './candle.chart.component';
import { seriesData } from '../chart.options';
import * as dayjs from 'dayjs';
import { IMagazineLuizaHistoryPrice } from 'src/app/shared/interfaces';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';

describe('Test Unit CandleChartComponent', () => {
  let component: CandleChartComponent;
  let fixture: ComponentFixture<CandleChartComponent>;
  let chartService: ChartService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CandleChartComponent ],
      imports: [HttpClientTestingModule, BrowserModule, CommonModule, NgApexchartsModule, FormsModule],
      providers: [ ChartService, PeriodStartDateObservable,   PeriodEndDateObservable ]
    })
    .compileComponents();
    fixture = TestBed.createComponent(CandleChartComponent);
    component = fixture.componentInstance;
    chartService = TestBed.inject(ChartService);
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

  it('should randomize the "y" values in the data array', () => {
    // Arrange
    const originalData = [{ x: 1, y: 10 }, { x: 2, y: [20, 30, 40] }, { x: 3, y: 50 }];

    // Act
    const randomizedData = component.randomizeData(originalData);

    // Assert
    randomizedData.forEach((item, index) => {
      if (Array.isArray(item.y)) {
        item.y.forEach((value: number) => {
          // Assert
          expect(value).toBeGreaterThanOrEqual(1);
          expect(value).toBeLessThanOrEqual(100);
        });
      } else {
        // Assert
        expect(item.y).toBeGreaterThanOrEqual(1);
        expect(item.y).toBeLessThanOrEqual(100);
      }

      // Assert
      expect(item.x).toEqual(originalData[index].x);
    });
  });

  it('should handle empty data array', () => {
    // Arrange
    const originalData: any[] = [];

    // Act
    const randomizedData = component.randomizeData(originalData);

    // Assert
    expect(randomizedData).toEqual([]);
  });

  it('should fetch magazineLuizaHistoryPrices on ngOnInit', waitForAsync(() => {
    // Arrange
    const fakeResponse: IMagazineLuizaHistoryPrice[] = [
      { date: dayjs(), open: 100, high: 110, low: 90, close: 105, adjClose: 105, volume: 1000000 },
      { date: dayjs().add(1, 'day'), open: 110, high: 120, low: 100, close: 115, adjClose: 115, volume: 1200000 },
      { date: dayjs().add(2, 'days'), open: 120, high: 130, low: 110, close: 125, adjClose: 125, volume: 1500000 },
    ];
    spyOn(chartService, 'get').and.returnValue(Promise.resolve(fakeResponse));

    // Act
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(2, 'days').format("YYYY-MM-DD")
    component.ngOnInit();

    // Assert
    fixture.whenStable().then(() => {
      expect(component.magazineLuizaHistoryPrices).toEqual(fakeResponse);
    });
  }));


});
