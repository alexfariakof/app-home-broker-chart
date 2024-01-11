import { Injectable } from '@angular/core';
import * as dayjs from 'dayjs';
import { Dayjs } from 'dayjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PeriodEndDateObservable {
  private _endDate: BehaviorSubject<string | Dayjs>;

  constructor() {
    const storedEndDate:string | Dayjs | null = localStorage.getItem('endDate');
    const initialEndDate = storedEndDate || dayjs().format("YYYY-MM-DD");
    this._endDate = new BehaviorSubject<string | Dayjs>(initialEndDate);
  }

  get endDate$() {
    return this._endDate.asObservable();
  }


  get endDate(): string | Dayjs {
    return this._endDate.getValue();
  }

  set endDate(value: string | Dayjs) {
    this._endDate.next(value);
    localStorage.setItem('endDate', value.toString());
  }
}
