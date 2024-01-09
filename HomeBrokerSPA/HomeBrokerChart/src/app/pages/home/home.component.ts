import { Component } from '@angular/core';
import { MagazineLuizaHistoryPrice, Period } from '../../shared/interfaces';
import { ChartService } from '../../shared/services';
import * as dayjs from 'dayjs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public magazineLuizaHistoryPrices?: MagazineLuizaHistoryPrice[];

  constructor(public chartService: ChartService) { }

  async ngOnInit(): Promise<void> {
    const period: Period = {
      StartDate: dayjs().add(-1, 'year'),
      EndDate: dayjs()
    }
    this.magazineLuizaHistoryPrices = await this.chartService.get(period.StartDate, period.EndDate);
  }
  title = 'Home Broke Magazione Luiza';
}
