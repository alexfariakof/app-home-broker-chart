import { LineChartComponent } from './line.chart.component';
import { ComponentFixture, TestBed, fakeAsync, flush, tick } from '@angular/core/testing';
import { ChartService } from '../../../services';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgApexchartsModule } from 'ng-apexcharts';
import { FormsModule } from '@angular/forms';
import { seriesData } from '../chart.options/mock.chart.data';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
import * as dayjs from 'dayjs';
import { ChartCommonOptions } from '../chart.options';

describe('Test Unit LineChartComponent', () => {
  let component: LineChartComponent;
  let fixture: ComponentFixture<LineChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LineChartComponent],
      imports: [HttpClientTestingModule, BrowserModule, CommonModule, NgApexchartsModule, FormsModule],
      providers: [ ChartService, PeriodStartDateObservable, PeriodEndDateObservable ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LineChartComponent);
    component = fixture.componentInstance;
    component.chartOptions = {
      series: [
        {
          name: "SMA",
          color: "#000",
          data: seriesData
        },
        {
          name: "EMA 9",
          data: seriesData
        },
        {
          name: "EMA 12",
          data: seriesData
        },
        {
          name: "EMA 26",
          data: seriesData
        },
      ],
      chart: {
        height: 260,
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
          colors: ["#f3f3f3", "transparent"],
          opacity: 0.5
        }
      },
      xaxis: {
        labels: {
          show: false,
        },
      },
    };
    fixture.detectChanges();
  });

  it('should create', () => {
    // Arrange & Act
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(30, 'days').format("YYYY-MM-DD");
    component.ngOnInit();

    // Assert
    expect(component).toBeTruthy();
  });

  it('should create Empty Chart ', () => {
    // Arrange & Act
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(2, 'days').format("YYYY-MM-DD")
    component.ngOnInit();

    // Assert
    expect(component).toBeTruthy();
  });

  it('should initialize component successfully', fakeAsync(() => {
    // Arrange
    const smaData = { values: [1, 2, 3] };
    const ema9Data = { values: [4, 5, 6] };
    const ema12Data = { values: [7, 8, 9] };
    const ema26Data = { values: [10, 11, 12] };
    const startDate = dayjs().format("YYYY-MM-DD");
    const endDate = dayjs().add(30, 'days').format("YYYY-MM-DD");

    spyOn(component.chartService, 'getSMA').and.returnValue(Promise.resolve(smaData));
    spyOn(component.chartService, 'getEMA')
    .withArgs(9, startDate, endDate).and.returnValue(Promise.resolve(ema9Data))
    .withArgs(12, startDate, endDate).and.returnValue(Promise.resolve(ema12Data))
    .withArgs(26, startDate, endDate).and.returnValue(Promise.resolve(ema26Data));

    // Act
    component.obsStartDate.startDate = startDate;
    component.obsEndDate.endDate = endDate;
    flush();
    component.initializeComponent();
    tick();

    // Assert
    expect(component.chartService.getSMA).toHaveBeenCalled();
    expect(component.chartService.getSMA).toHaveBeenCalledTimes(1);
    expect(component.chartService.getEMA).toHaveBeenCalled();
    expect(component.chartService.getEMA).toHaveBeenCalledTimes(3);
    expect(component.chartOptions).toBeDefined();
  }));
});
