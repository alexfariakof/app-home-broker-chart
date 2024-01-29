import { AbstractControl, ValidationErrors } from '@angular/forms';

export class CustomValidators {
  static dateRange(control: AbstractControl): ValidationErrors | null {
    const startDate = control.get('filterStart')?.value;
    const endDate = control.get('filterEnd')?.value;
    if (startDate && endDate && new Date(endDate) < new Date(startDate)) {
      return { dateRange: 'A data final deve ser maior ou igual à data inicial' };
    }

    // Verificar se o intervalo entre as datas é pelo menos 5 dias
    if (startDate && endDate && CustomValidators.calculateDateDifference(startDate, endDate) <= 5) {
      return { dateDifference: 'O intervalo entre as datas deve ser de pelo menos 5 dias' };
    }

    // Verificar se a data não é anterior a "2011-05-02"
    const minDate = new Date('2011-05-02');
    if (startDate && new Date(startDate) < minDate) {
      return { minDate: 'A data não deve ser anterior a 02/05/2011' };
    }

    return null;
  }

  static IsValidPeriod(startDate: string, endDate:string): boolean {
    // Verificar se a data final é maior ou igual à data inicial
    if (startDate && endDate && new Date(endDate) < new Date(startDate)) {
      return false;
    }

    // Verificar se o intervalo entre as datas é pelo menos 5 dias
    if (startDate && endDate && CustomValidators.calculateDateDifference(startDate, endDate) <= 5) {
      return false;
    }
    // Verificar se a data não é anterior a "2011-05-02"
    const minDate = new Date('2011-05-02');
    if (startDate && new Date(startDate) < minDate) {
      return false;
    }

    return true;
  }
  private static calculateDateDifference(startDate: string, endDate: string): number {
    const diffInMs = new Date(endDate).getTime() - new Date(startDate).getTime();
    return Math.floor(diffInMs / (1000 * 60 * 60 * 24));
  }
}
