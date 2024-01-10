import { Injectable } from '@angular/core';
import * as dayjs from 'dayjs';
import { Dayjs } from 'dayjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PeriodStartDateObservable {
  private _startDate: BehaviorSubject<string | Dayjs>;

  constructor() {
    const storedStartDate:string | Dayjs | null = localStorage.getItem('startDate');
    const initialStartDate = storedStartDate || dayjs().add(-365,'days').format("YYYY-MM-DD");
    this._startDate = new BehaviorSubject<string | Dayjs>(initialStartDate);
  }

  get startDate$() {
    return this._startDate.asObservable();
  }


  get startDate(): string | Dayjs {
    return this._startDate.getValue();
  }

  set startDate(value: string | Dayjs) {
    this._startDate.next(value);
    localStorage.setItem('startDate', value.toString());
  }
}
