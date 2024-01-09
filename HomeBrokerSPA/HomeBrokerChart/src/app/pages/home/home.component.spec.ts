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
});
