import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
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
      }

      alert(message)
      return throwError(() => error)
    })
  )
}