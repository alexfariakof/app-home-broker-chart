import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MacdChartComponent } from './macd.chart.component';

describe('MacdChartComponent', () => {
  let component: MacdChartComponent;
  let fixture: ComponentFixture<MacdChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MacdChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MacdChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
