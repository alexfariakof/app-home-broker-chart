import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MdbFormsModule } from "mdb-angular-ui-kit/forms";
import { PeriodStartDateObservable } from './../../observables/period/period.startDate.observable';
import { PeriodEndDateObservable } from '../../observables';
import { CustomValidators } from '../../validators';

@Component({
  selector: 'app-period-filter',
  standalone: true,
  templateUrl: './period.filter.component.html',
  styleUrls: ['./period.filter.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MdbFormsModule ],

})
export class PeriodFilterComponent implements OnInit {
  periodForm: FormGroup;

  constructor(
    private builder: FormBuilder,
    public obsStartDate: PeriodStartDateObservable,
    public obsEndDate: PeriodEndDateObservable) {

      this.periodForm = this.builder.group(
        {
          filterStart: new FormControl(obsStartDate.startDate, Validators.required),
          filterEnd: new FormControl(obsEndDate.endDate, Validators.required),
        },
        {
          validators: [CustomValidators.dateRange],
        }
      );

    this.periodForm.valueChanges.subscribe(form => {
      this.obsStartDate.startDate = form.filterStart;
      this.obsEndDate.endDate = form.filterEnd;
    });
  }

  ngOnInit(): void { }

  onUpdateClick: Function = (): void => {
    if (this.periodForm.valid)
      location.reload();
  }
}
