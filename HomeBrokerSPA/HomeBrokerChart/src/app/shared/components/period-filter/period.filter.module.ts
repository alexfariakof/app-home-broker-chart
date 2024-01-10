import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { PeriodFilterComponent } from "./period.filter.component";
import { PeriodObservable } from "../../observables";

@NgModule({
  declarations: [PeriodFilterComponent],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatIconModule, MatInputModule,
            MatFormFieldModule, MatNativeDateModule, MatDatepickerModule],
  providers: [PeriodObservable],
  exports: [PeriodFilterComponent]
})
export class PeriodFilterModule {}
