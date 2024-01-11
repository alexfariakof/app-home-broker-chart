import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import * as dayjs from 'dayjs';
import { AcoesComponent } from './acoes.component';
import { IMagazineLuizaHistoryPrice } from '../../shared/interfaces';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
import { ChartService } from '../../shared/services';

describe('AcoesComponent', () => {
  let component: AcoesComponent;
  let fixture: ComponentFixture<AcoesComponent>;
  let chartService: ChartService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AcoesComponent ],
      imports: [HttpClientTestingModule],
      providers: [
        ChartService,
        PeriodStartDateObservable,
        PeriodEndDateObservable,
      ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcoesComponent);
    component = fixture.componentInstance;
    chartService = TestBed.inject(ChartService);
    fixture.detectChanges();
  });

  it('should create', () => {
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
    component.obsStartDate.startDate = dayjs().format("YYYY-MM-DD");
    component.obsEndDate.endDate = dayjs().add(2, 'days').format("YYYY-MM-DD")
    component.ngOnInit();

    // Assert
    fixture.whenStable().then(() => {
      expect(component.magazineLuizaHistoryPrices).toEqual(fakeResponse);
    });
  }));

  it('should format the date correctly', () => {
    // Arrange
    const exampleDate = '2024-01-11T00:00:00';

    // Act
    const result = component.formatCustomDate(exampleDate);

    // Assert
    expect(result).toBe('11/01/2024');
  });

  it('should return an empty string for an invalid date', () => {
    // Arrange
    const invalidDate = 'data_invalida';

    // Act
    const result = component.formatCustomDate(invalidDate);

    // Assert
    expect(result).toBe('Invalid Date');
  });

});
