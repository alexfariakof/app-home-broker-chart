import { Component, OnInit } from '@angular/core';
import { ChartService } from './shared/services/api/chart.service';
import { MagazineLuizaHistoryPrice, Period } from './shared/interfaces';
import * as dayjs from 'dayjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public magazineLuizaHistoryPrices?: MagazineLuizaHistoryPrice[];

  constructor(public chartService:ChartService) {}

  async ngOnInit():  Promise<void>  {
   const period:Period = {
    StartDate: dayjs().add(-1,'year'),
    EndDate: dayjs()
   }
    this.magazineLuizaHistoryPrices = await this.chartService.get(period.StartDate, period.EndDate);
  }
  title = 'Chart Home Broker';
}

