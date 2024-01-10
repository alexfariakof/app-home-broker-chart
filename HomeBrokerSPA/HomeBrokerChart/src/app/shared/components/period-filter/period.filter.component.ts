import { PeriodStartDateObservable } from './../../observables/period/period.startDate.observable';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PeriodEndDateObservable } from '../../observables';
@Component({
  selector: 'app-period-filter',
  standalone: true,
  templateUrl: './period.filter.component.html',
  styleUrls: ['./period.filter.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class PeriodFilterComponent implements OnInit{
  constructor(public obsStartDate: PeriodStartDateObservable, public obsEndDate: PeriodEndDateObservable) { }
  ngOnInit(): void {}

  onUpdateClick: Function = ():void => {
    location.reload();
  }
}
