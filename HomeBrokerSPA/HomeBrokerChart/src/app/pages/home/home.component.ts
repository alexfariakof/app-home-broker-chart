import { Component } from '@angular/core';
import { IMagazineLuizaHistoryPrice  } from '../../shared/interfaces';
import { ChartService } from '../../shared/services';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public magazineLuizaHistoryPrices?: IMagazineLuizaHistoryPrice[];
  constructor(public chartService: ChartService, public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) { }

  async ngOnInit(): Promise<void> {
    this.magazineLuizaHistoryPrices = await this.chartService.get(this.obsStartDate.startDate, this.obsEndDate.endDate);
  }
}
