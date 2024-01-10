import * as dayjs from 'dayjs';

import { PeriodObservable } from './../../observables/period/period.observable';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-period-filter',
  templateUrl: './period.filter.component.html',
  styleUrls: ['./period.filter.component.css']
})
export class PeriodFilterComponent implements OnInit{
  StartDate: string = dayjs().add(-365,'days').format("YYYY-MM-DD");
  EndDate: string = dayjs().format("YYYY-MM-DD");


  constructor(public periodObservable: PeriodObservable) {

  }

  ngOnInit(): void {
    this.StartDate = this.periodObservable.startDate;
    this.EndDate = this.periodObservable.endDate;

  }

  onChangePeriod: Function = () => {
    this.periodObservable.period = {
      StartDate:  dayjs(this.StartDate),
      EndDate: dayjs(this.EndDate)
    };
  };


}
