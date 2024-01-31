import { TestBed, fakeAsync, flush, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import * as dayjs from 'dayjs';
import { ChartService } from './chart.service';
import { IPeriod } from '../../interfaces';

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
      const period:IPeriod = {
        StartDate: dayjs().add(-1,'year'),
        EndDate: dayjs()
      }
      const mockResult = [{}];
      const expectedUrl = `${service.routeUrl}/${period.StartDate}/${period.EndDate}`;

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
      const period:IPeriod = {
        StartDate: dayjs().add(-1,'year'),
        EndDate: dayjs()
      }
      const expectedUrl = `${service.routeUrl}/getsma/${period.StartDate}/${period.EndDate}`;

      // Act
      const result = service.getSMA(period.StartDate, period.EndDate);
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
    const period:IPeriod = {
      StartDate: dayjs().add(-1,'year'),
      EndDate: dayjs()
    }
    const expectedUrl = `${service.routeUrl}/getema/9/${period.StartDate}/${period.EndDate}`;

    // Act
    const result = service.getEMA(9, period.StartDate,period.EndDate);
    flush();

    // Assert
    expect(result).toBeTruthy();
    const req = httpMock.expectOne(expectedUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockResult);
    httpMock.verify();
  })));


  it('should send a getMACD request to the ChartHomeBroker', inject([ChartService, HttpTestingController], fakeAsync((service: ChartService, httpMock: HttpTestingController) => {
    // Arrange
    const mockResult= {};
    const period:IPeriod = {
      StartDate: dayjs().add(-1,'year'),
      EndDate: dayjs()
    }
    const expectedUrl = `${service.routeUrl}/getmacd/${period.StartDate}/${period.EndDate}`;

    // Act
    const result = service.getMACD(period.StartDate,period.EndDate);
    flush();

    // Assert
    expect(result).toBeTruthy();
    const req = httpMock.expectOne(expectedUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockResult);
    httpMock.verify();
  })));

});
