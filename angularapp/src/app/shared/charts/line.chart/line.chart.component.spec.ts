import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LineChartComponent } from './line.chart.component';
import { ChartService } from '../../services';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgApexchartsModule } from 'ng-apexcharts';
import { seriesData } from '../../chart.options/ohlc';
import { from } from 'rxjs';

describe('Line.ChartComponent', () => {
  let component: LineChartComponent;
  let fixture: ComponentFixture<LineChartComponent>;
  let chartService: ChartService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LineChartComponent],
      imports: [HttpClientTestingModule, BrowserModule, CommonModule, NgApexchartsModule, FormsModule],
      providers: [ChartService]
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
    chartService = TestBed.inject(ChartService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
