import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { HomeModule } from './pages/home/home.module';
import {  LineChartModule, MacdChartModule, PeriodFilterComponent } from './shared/components';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

describe('Test Unit AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent, NavMenuComponent],
      imports: [HttpClientModule, RouterTestingModule, HomeModule, LineChartModule, MacdChartModule, PeriodFilterComponent]

    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should have as title Home Broker Chart', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Home Broker Chart');
  });
});
