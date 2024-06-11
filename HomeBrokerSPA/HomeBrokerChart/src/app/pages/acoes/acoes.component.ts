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

  downloadHistory = async () => {
    try {
      const blob = await this.chartService.downloadHistory(this.obsStartDate.startDate.toString(), this.obsEndDate.endDate.toString());
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `historico_${this.obsStartDate.startDate.toString()}_${this.obsEndDate.endDate.toString()}.xlsx`;
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
    }
    catch (error) {
      console.error('Erro ao fazer o download do arquivo Excel:', error);
    }
  }

  formatCustomDate(date: any): string {
    return dayjs(date).locale('pt-Br').format('DD/MM/YYYY');
  }
}
