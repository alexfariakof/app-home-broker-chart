import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgApexchartsModule } from 'ng-apexcharts';
import { CandleChartModule } from './shared/charts/candle.chart/candle.chart.component.module';
import { LineChartModule } from './shared/charts/line.chart/line.chart.component.module';
import { ChartService } from './shared/services';

describe('AppComponent Unit Test ', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppComponent ],
      imports: [ BrowserModule, HttpClientModule, LineChartModule, CandleChartModule, NgApexchartsModule  ],
      providers: [ChartService]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Chart Home Broker'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Chart Home Broker');
  });

});
