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
  period: Period;

  constructor(public chartService: ChartService) { }

  async ngOnInit(): Promise<void> {
    this.period = {
      StartDate: dayjs().add(-1, 'year'),
      EndDate: dayjs()
    }
    this.magazineLuizaHistoryPrices = await this.chartService.get(this.period.StartDate, this.period.EndDate);
  }
  title = 'Home Broke Magazione Luiza';
}
