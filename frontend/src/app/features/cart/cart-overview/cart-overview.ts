import { Component, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { CartService } from '../cart-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart-overview',
  imports: [RouterLink],
  templateUrl: './cart-overview.html',
  styleUrl: './cart-overview.css',
})
export class CartOverview {
  cartService = inject(CartService);

  get itemCount(): number {
    return this.cart.value()?.items.reduce((s, i) => s + i.quantity, 0) ?? 0;
  }

  cart = rxResource({
    stream: () => this.cartService.GetCart()
  });

  updateQuantity(productVariantId: number, quantity: number) {
    this.cartService.UpdateItemQuantity(productVariantId, { quantity }).subscribe(() => 
      this.cart.reload()
    );
  }

  removeItem(productVariantId: number) {
    this.cartService.RemoveItem(productVariantId).subscribe(() => 
      this.cart.reload());
  }

  clearCart() {
    this.cartService.ClearCart().subscribe(() => 
      this.cart.reload());
  }
}
