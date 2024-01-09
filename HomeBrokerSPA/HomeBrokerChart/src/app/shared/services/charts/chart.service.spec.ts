import { TestBed, fakeAsync, flush, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ChartService } from './chart.service';
import { Period } from '../../interfaces';
import * as dayjs from 'dayjs';

describe('Test Unit ChartService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[ChartService] });
  });

  it('should be created', inject([ChartService], (service: ChartService) => {
    expect(service).toBeTruthy();
  }));

  it('should send a get request to the ChartHomeBroker', inject([ChartService, HttpTestingController],  fakeAsync((service: ChartService, httpMock: HttpTestingController) => {
      // Arrange
      const period:Period = {
        StartDate: dayjs().add(-1,'year'),
        EndDate: dayjs()
      }
      const mockResult = [{}];
      const expectedUrl = `${service.routeUrl}?StartDate=${period.StartDate}&EndDate=${period.EndDate}`;

      // Act
      const result = service.get(period.StartDate, period.EndDate);
      flush();

      // Assert
      expect(result).toBeTruthy();
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResult);
      httpMock.verify();
  })));

  it('should send a getSMA request to the ChartHomeBroker', inject([ChartService, HttpTestingController], fakeAsync((service: ChartService, httpMock: HttpTestingController) => {
      //Arrange
      const mockResult= {};
      const expectedUrl = `${service.routeUrl}/GetSMA`;

      // Act
      const result = service.getSMA();
      flush();

      // Assert
      expect(result).toBeTruthy();
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResult);
      httpMock.verify();
  })));

  it('should send a getEMA request to the ChartHomeBroker', inject([ChartService, HttpTestingController], fakeAsync((service: ChartService, httpMock: HttpTestingController) => {
    // Arrange
    const mockResult= {};
    const expectedUrl = `${service.routeUrl}/GetEMA/9`;

    // Act
    const result = service.getEMA(9);
    flush();

    // Assert
    expect(result).toBeTruthy();
    const req = httpMock.expectOne(expectedUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockResult);
    httpMock.verify();
  })));

});
