import { TestBed } from '@angular/core/testing';

import { ErrorInterceptor } from './error.interceptor';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HTTP_INTERCEPTORS, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs';

describe('ErrorInterceptor', () => {
  let httpMock: HttpTestingController;
  let httpClient: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        {
          provide: HTTP_INTERCEPTORS,
          useClass: ErrorInterceptor,
          multi: true
        }
      ]
    });

    httpClient = TestBed.inject(HttpClient);
    httpMock = TestBed.inject(HttpTestingController);

  });

  afterEach(() => {
    httpMock.verify();
  });
 
  it('should handle HTTP error without response error', () => {
   var spyAlert =  spyOn(window, 'alert');
    httpClient.get('/test').pipe(
      catchError((error: HttpErrorResponse) => {
        expect(error.status).toBe(500);
        expect(window.alert).not.toHaveBeenCalled();
        return throwError(() => error);
      })
    ).subscribe({
      next: () => fail('should have failed with status 500 error'),
      error: () => { /* handle error */ }
    });
 
    const req = httpMock.expectOne('/test');
    req.flush(null, { status: 500, statusText: 'Internal Server Error' });

  });
});
