import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import * as dayjs from 'dayjs';
import { HomeComponent } from './home.component';
import { ChartService } from '../../shared/services';
import { PeriodStartDateObservable, PeriodEndDateObservable } from '../../shared/observables';
import { IMagazineLuizaHistoryPrice } from '../../shared/interfaces';
import { LineChartModule, MacdChartModule } from 'src/app/shared/components';

describe('Test Unit HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let chartService: ChartService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeComponent],
      imports: [HttpClientTestingModule, LineChartModule, MacdChartModule ],
      providers: [ ChartService, PeriodStartDateObservable, PeriodEndDateObservable ],
    });

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    chartService = TestBed.inject(ChartService);
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(30, 'days').format("YYYY-MM-DD")
  });

  it('should create HomeComponent', () => {
    expect(component).toBeTruthy();
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
    component.ngOnInit();

    // Assert
    fixture.whenStable().then(() => {
      expect(component.magazineLuizaHistoryPrices).toEqual(fakeResponse);
    });
  }));
});
