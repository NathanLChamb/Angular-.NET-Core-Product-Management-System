import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/auth/auth-service';
import { Router } from '@angular/router';
import { passwordMatchValidator } from '../../../shared/validators/match-password.validator';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  errorMessage = '';

  registerForm = this.fb.nonNullable.group(
  {
    displayName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [
      Validators.required,
      Validators.minLength(8)
    ]],
    confirmPassword: ['', Validators.required]
  },
  {
    validators: passwordMatchValidator()
  }
);

  onSubmit(): void {

  if (this.registerForm.invalid) {
    return;
  }

  const { confirmPassword, ...request } = this.registerForm.getRawValue();

  this.errorMessage = '';

  this.authService.register(request)
    .subscribe({
      next: () => this.router.navigate(['/category']),
      error: err => {
        console.error(err);
        this.errorMessage = 'Unable to register.';
      }
    });
}
}
