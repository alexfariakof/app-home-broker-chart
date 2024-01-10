import { Injectable } from '@angular/core';
import * as dayjs from 'dayjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PeriodEndDateObservable {
  private _endDate: BehaviorSubject<string>;

  constructor() {
    const storedEndDate:string | null = localStorage.getItem('endDate');
    const initialEndDate = storedEndDate || dayjs().format("YYYY-MM-DD");
    this._endDate = new BehaviorSubject<string>(initialEndDate);
  }

  get endDate$() {
    return this._endDate.asObservable();
  }


  get endDate(): string {
    return this._endDate.getValue();
  }

  set endDate(value: string) {
    this._endDate.next(value);
    localStorage.setItem('endDate', value);
  }
}
