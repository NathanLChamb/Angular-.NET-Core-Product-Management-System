import { Component, inject } from '@angular/core';
import { OrderService } from '../order-service';
import { Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-checkout',
  imports: [ReactiveFormsModule],
  templateUrl: './checkout.html',
  styleUrl: './checkout.css',
})
export class Checkout {
  private orderService = inject(OrderService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  checkoutForm = this.fb.group({
    shippingAddress: ['', Validators.required]
  });

   submitOrder() {
    if (this.checkoutForm.invalid) {
      return;
    }

    this.orderService.createOrder({
      shippingAddress: this.checkoutForm.value.shippingAddress!}).subscribe(order => {
        this.router.navigate(['/orders', order.id]);
      });
  }
}
