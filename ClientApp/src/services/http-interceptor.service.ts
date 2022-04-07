import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {
  session;
  token;
  

  constructor(
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    
    this.session = sessionStorage.getItem(
      'oidc.user:https://localhost:44358:mgSolutions'
    );
    this.token = JSON.parse(this.session);


    // if (this.token != null) {
    //   request = request.clone({
    //     headers: request.headers.set('Authorization', 'Bearer ' + this.token),
    //   });
    // } else {
    //   window.sessionStorage.clear();
    //   console.log('gese');
    //   this.router.navigateByUrl("/", { skipLocationChange: true }).then(() => {
    //     this.router.navigate(["/login"]);
    //   });
    // }

    // if (!request.headers.has('Content-Type')) {
    //   request = request.clone({
    //     headers: request.headers.set('Content-Type', 'application/json'),
    //   });
    // }

    // request = request.clone({
    //   headers: request.headers.set('Accept', 'application/json'),
    // });

    // return next.handle(request).pipe(
    //   map((event: HttpEvent<any>) => {
    //     if (event instanceof HttpResponse) {
    //       console.log('event--->>>', event);
    //     }
    //     return event;
    //   })
    // );

    if (this.token != null) {
      request = request.clone({
        setHeaders: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
          Authorization: `Bearer ${this.token['access_token']}`,
        },
      });
    } 
    
    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        console.log('HttpErrorResponse', error);
        if (error.status === 401 || error.ok === false) {
          window.sessionStorage.clear();
          window.location.href = '/login';
        }
        return throwError(error);
      }),
    );
  }
  
}
