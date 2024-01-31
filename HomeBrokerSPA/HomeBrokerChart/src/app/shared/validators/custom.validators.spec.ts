import { FormControl, FormGroup } from '@angular/forms';
import { CustomValidators } from './custom.validators';

describe('CustomValidators', () => {
  it('should return null for a valid date range', () => {
    const formGroup = new FormGroup({
      filterStart: new FormControl('2024-01-01'),
      filterEnd: new FormControl('2024-01-10'),
    });

    const result = CustomValidators.dateRange(formGroup);

    expect(result).toBeNull();
  });

  it('should return error for an invalid date range (end date before start date)', () => {
    const formGroup = new FormGroup({
      filterStart: new FormControl('2024-01-10'),
      filterEnd: new FormControl('2024-01-01'),
    });

    const result = CustomValidators.dateRange(formGroup);

    expect(result).toEqual({ invalidDateRange: true, message: 'A data final deve ser maior ou igual à data inicial.' });
  });

  it('should return error for a date range with less than 5 days interval', () => {
    const formGroup = new FormGroup({
      filterStart: new FormControl('2024-01-01'),
      filterEnd: new FormControl('2024-01-04'),
    });

    const result = CustomValidators.dateRange(formGroup);

    expect(result).toEqual({ invalidDateRange: true, message:'O intervalo entre as datas deve ser de pelo menos 5 dias.' });
  });

  it('should return error for a start date earlier than "2011-05-02"', () => {
    const formGroup = new FormGroup({
      filterStart: new FormControl('2011-04-30'),
      filterEnd: new FormControl('2011-05-10'),
    });

    const result = CustomValidators.dateRange(formGroup);

    expect(result).toEqual({ invalidDateRange: true, message: 'A data não deve ser anterior a 02/05/2011.' });
  });

  it('should return true for a valid period using IsValidPeriod', () => {
    const isValid = CustomValidators.IsValidPeriod('2024-01-01', '2024-01-10');

    expect(isValid).toBe(true);
  });

  it('should return false for an invalid period using IsValidPeriod (end date before start date)', () => {
    const isValid = CustomValidators.IsValidPeriod('2024-01-10', '2024-01-01');

    expect(isValid).toBe(false);
  });

  it('should return false for an invalid period using IsValidPeriod (less than 5 days interval)', () => {
    const isValid = CustomValidators.IsValidPeriod('2024-01-01', '2024-01-04');

    expect(isValid).toBe(false);
  });

  it('should return false for an invalid period using IsValidPeriod (start date earlier than "2011-05-02")', () => {
    const isValid = CustomValidators.IsValidPeriod('2011-04-30', '2011-05-10');

    expect(isValid).toBe(false);
  });

});
