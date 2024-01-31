import { ComponentFixture, TestBed, fakeAsync, flush, tick } from '@angular/core/testing';
import { MacdChartComponent } from './macd.chart.component';
import { CommonModule } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PeriodStartDateObservable, PeriodEndDateObservable } from '../../../../shared/observables';
import { ChartService } from '../../../../shared/services';
import * as dayjs from 'dayjs';
import { mockMACD } from '../chart.options';
import { IMagazineLuizaHistoryPrice } from 'src/app/shared/interfaces';

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
    // Arrange & Act
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(30, 'days').format("YYYY-MM-DD")
    component.ngOnInit();

    // Assert
    expect(component).toBeTruthy();
  });

  it('should create With Empty Chart', () => {
    // Arrange & Act
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(2, 'days').format("YYYY-MM-DD");
    component.ngOnInit();

    // Assert
    expect(component).toBeTruthy();
  });

  it('should initialize component successfully', fakeAsync(() => {
    // Arrange
    const startDate = dayjs().format("YYYY-MM-DD");
    const endDate = dayjs().add(10, 'days').format("YYYY-MM-DD");

    const historyPrices: any[] = [
      {
        date: dayjs(),
        open: 100,
        high: 120,
        low: 90,
        close: 110,
        adjClose: 108,
        volume: 100000,
      },
      {
        date: dayjs().add(15, 'days'),
        open: 110,
        high: 130,
        low: 95,
        close: 120,
        adjClose: 118,
        volume: 110000,
      },
      {
        date: dayjs().add(30, 'days'),
        open: 110,
        high: 130,
        low: 95,
        close: 120,
        adjClose: 118,
        volume: 110000,
      },
    ];
    const macdData = { histogram: [1, 2, 3], macdLine: [4, 5, 6], signal: [7, 8, 9] };

    spyOn(component.chartService, 'get')
    .withArgs(startDate, endDate)
    .and.returnValue(Promise.resolve(historyPrices));
    spyOn(component.chartService, 'getMACD')
    .withArgs(startDate, endDate).and.returnValue(Promise.resolve(macdData));

    // Act
    component.obsStartDate.startDate = startDate;
    component.obsEndDate.endDate = endDate;
    component.initializeComponent();
    flush();
    tick();

    // Assert
    expect(component.chartService.get).toHaveBeenCalled();
    expect(component.chartService.get).toHaveBeenCalledTimes(1);
    expect(component.chartService.getMACD).toHaveBeenCalled();
    expect(component.chartService.getMACD).toHaveBeenCalledTimes(1);
    expect(component.chartMacdOptions).toBeDefined();
  }));
});
