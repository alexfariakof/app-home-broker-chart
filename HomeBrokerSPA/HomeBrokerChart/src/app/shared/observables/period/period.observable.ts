import { Injectable } from '@angular/core';
import * as dayjs from 'dayjs';
import { BehaviorSubject } from 'rxjs';
import { Period } from '../../interfaces';

@Injectable({
  providedIn: 'root'
})
export class PeriodObservable {
  private _period: BehaviorSubject<Period>;

  constructor() {
    const storedPeriod:Period = localStorage.getItem('period') as unknown  as Period;
    const initialPeriod = storedPeriod || { StartDate: dayjs().add(-2, 'year'),  EndDate: dayjs() } as Period;
    this._period = new BehaviorSubject<Period>(initialPeriod);
  }

  get period$() {
    return this._period.asObservable();
  }

  get startDate(): string {
    return this._period.getValue().StartDate.format("YYYY-MM-DD");
  }


  get endDate(): string {
    return this._period.getValue().EndDate.format("YYYY-MM-DD");
  }


  get period(): Period {
    return this._period.getValue();
  }


  set period(value: Period) {
    this._period.next(value);
    localStorage.setItem('period', JSON.stringify(value));
  }
}
