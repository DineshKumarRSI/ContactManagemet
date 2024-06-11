import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { ErrorResponse } from '../interface/error-response';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((errorResponse: HttpErrorResponse) => {
        switch(errorResponse.status){
          case 0:
            alert("Something wrong at API");
            break;
          default:
            if (errorResponse.error) {

              var responseError:ErrorResponse = errorResponse.error;
              var error = responseError.title + "! \n";

              if(responseError.error ){
                for (let index = 0; index < responseError.error.length; index++) {
                  error = responseError.error[index] + "\n";
                }
              }

              alert(error);
            }
            break;
        }
        
        return throwError(() => errorResponse.error);
      })
    )
  }
}
