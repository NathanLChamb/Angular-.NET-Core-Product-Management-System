import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../auth/auth-service';
import { inject } from '@angular/core';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  
  const authService = inject(AuthService);

  return next(req).pipe(
    catchError((error) => {
      console.error('HTTP error:', error)
      let message = 'Something went wrong'

      if (error.status === 0) {
        message = 'Cannot connect to server'
      } else if (error.status === 404) {
        message = error.error?.message ?? "Resource not found"
      } else if (error.status === 400) {
        message = error.error?.message ?? "Bad Request"
      } else if (error.status === 401 
        && !req.url.includes('/auth/login') &&
        !req.url.includes('/auth/register')
      ) {
        authService.logout();
        return throwError(() => error);
      }
      else if (error.status === 401) {
        message = error.error?.message ?? 'Invalid email or password.';
      }

      alert(message)
      return throwError(() => error)
    })
  )
}