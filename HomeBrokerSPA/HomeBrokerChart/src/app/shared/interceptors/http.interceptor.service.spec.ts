import { TestBed, fakeAsync, flush, inject, tick } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomInterceptor } from './http.interceptor.service';
import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';

describe('CustomInterceptor', () => {
  let interceptor: CustomInterceptor;
  let modalService: NgbModal;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CustomInterceptor, NgbModal],
    });

    interceptor = TestBed.inject(CustomInterceptor);
    modalService = TestBed.inject(NgbModal);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', inject([CustomInterceptor], (service: CustomInterceptor) => {
    expect(service).toBeTruthy();
  }));

  it('should show loader on intercept', fakeAsync(() => {
    const openSpy = spyOn(modalService, 'open').and.returnValue({ result: Promise.resolve() } as any);
    // Não estou conseguindo valida a execução do close
    const dismissAllSpy = spyOn(modalService, 'dismissAll');

    const next: HttpHandler = {
      handle: (request: HttpRequest<any>): Observable<HttpEvent<any>> => {
        return of({} as HttpEvent<any>);
      }
    };

    interceptor.intercept({} as HttpRequest<any>, next).subscribe(() => {
      expect(openSpy).toHaveBeenCalled();
    });
  }));
});
