import { Component } from '@angular/core';
import * as dayjs from 'dayjs';
import { IMagazineLuizaHistoryPrice } from '../../shared/interfaces';
import { PeriodStartDateObservable, PeriodEndDateObservable } from '../../shared/observables';
import { ChartService } from '../../shared/services';
import { CustomValidators } from 'src/app/shared/validators';

@Component({
  selector: 'app-acoes',
  templateUrl: './acoes.component.html',
  styleUrls: ['./acoes.component.css']
})
export class AcoesComponent {
  public magazineLuizaHistoryPrices?: IMagazineLuizaHistoryPrice[];
  constructor(public chartService: ChartService, public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) { }

  async ngOnInit(): Promise<void> {
    if (CustomValidators.IsValidPeriod(this.obsStartDate.startDate.toString(), this.obsEndDate.endDate.toString()))
      this.magazineLuizaHistoryPrices = await this.chartService.get(this.obsStartDate.startDate, this.obsEndDate.endDate);
  }

  formatCustomDate(date: any): string {
    return dayjs(date).locale('pt-Br').format('DD/MM/YYYY');
  }
}
