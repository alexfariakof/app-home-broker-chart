import { Component } from '@angular/core';
import { IMagazineLuizaHistoryPrice  } from '../../shared/interfaces';
import { ChartService } from '../../shared/services';
import { PeriodStartDateObservable, PeriodEndDateObservable } from 'src/app/shared/observables';
import { CustomValidators } from 'src/app/shared/validators';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public magazineLuizaHistoryPrices?: IMagazineLuizaHistoryPrice[];
  constructor(public chartService: ChartService, public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) { }

  async ngOnInit(): Promise<void> {
    if (CustomValidators.IsValidPeriod(this.obsStartDate.startDate.toString(), this.obsEndDate.endDate.toString()))
      this.magazineLuizaHistoryPrices = await this.chartService.get(this.obsStartDate.startDate, this.obsEndDate.endDate);
  }
}
