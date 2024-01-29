import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PeriodStartDateObservable } from './../../observables/period/period.startDate.observable';
import { PeriodEndDateObservable } from '../../observables';
import { CustomValidators } from '../../validators';
@Component({
  selector: 'app-period-filter',
  standalone: true,
  templateUrl: './period.filter.component.html',
  styleUrls: ['./period.filter.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class PeriodFilterComponent implements OnInit{
  periodForm:FormGroup;

  constructor(
    private fb: FormBuilder,
    public obsStartDate: PeriodStartDateObservable,
    public obsEndDate: PeriodEndDateObservable)
  {
    this.periodForm = this.fb.group(
      {
        filterStart: [obsStartDate.startDate, [Validators.required, CustomValidators.dateRange]],
        filterEnd: [obsEndDate.endDate, [Validators.required, CustomValidators.dateRange]],
      },
      {
        validators: [CustomValidators.dateRange],
      }
    );


  }
  ngOnInit(): void {}

  onUpdateClick: Function = ():void => {
    if (this.periodForm.valid)
      location.reload();
  }
}
