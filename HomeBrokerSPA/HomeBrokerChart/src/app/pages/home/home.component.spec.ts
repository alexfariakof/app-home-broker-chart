import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';
import { HomeComponent } from './home.component';
import { ChartService } from '../../shared/services';
import * as dayjs from 'dayjs';
import { MagazineLuizaHistoryPrice, Period } from '../../shared/interfaces';


describe('Test Unit HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let chartService: ChartService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeComponent],
      providers: [
        { provide: ChartService, useValue: chartService }
      ]
    });

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    chartService = TestBed.inject(ChartService);
    fixture.detectChanges();
  });

  it('should create the component', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should fetch magazineLuizaHistoryPrices on ngOnInit', fakeAsync(() => {
    // Arrange
    const period: Period = {
      StartDate: dayjs().add(-1, 'year'),
      EndDate: dayjs()
    };
    const fakeResponse: MagazineLuizaHistoryPrice[] = [
      { date: dayjs().add(5, 'days'), open: 100, high: 110, low: 90,  close: 105, adjClose: 105, volume: 1000000 },
      { date: dayjs().add(10, 'days'), open: 110, high: 120, low: 100, close: 115, adjClose: 115, volume: 1200000,},
      { date: dayjs().add(20, 'days'), open: 120, high: 130, low: 110, close: 125, adjClose: 125, volume: 1500000,},
    ];
    //spyOn(chartService, 'get').and.returnValue(Promise.resolve(fakeResponse));
    // Act
    component.period = period;
    component.ngOnInit();

    //Assert
    fixture.whenStable().then(() => {
      expect(component.magazineLuizaHistoryPrices).toEqual(fakeResponse);
    });
  }));
});
