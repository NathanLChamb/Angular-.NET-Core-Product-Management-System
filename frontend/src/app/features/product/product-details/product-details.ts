import { Component, inject, input, signal } from '@angular/core';
import { ProductService } from '../product-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { ProductSearchFilter, ProductSort } from '../models';
import { EMPTY } from 'rxjs';
import { CartService } from '../../cart/cart-service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-details',
  imports: [],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails {
  private productService = inject(ProductService)
  private cartService = inject(CartService)
  private snackBar = inject(MatSnackBar)

  protected ProductSort = ProductSort;
  protected id = input<string | undefined>()
  protected filter = signal<ProductSearchFilter>({
    search: '',
    categoryIds: [],
    optionIds: [],
    sort: ProductSort.Default,
    pageNumber: 1,
    pageSize: 5
  })

  protected product = rxResource({
    params: () => {
      const id = this.id();
      return id ? { id: Number(id) } : null;
    },
    stream: ({ params }) => {
      if (!params) return EMPTY;
      return this.productService.GetProductById(params.id);
    }
  });

  addToCart(productVariantId: number) {
    this.cartService.AddItem({
        productVariantId,
        quantity: 1,
      }).subscribe(() => {
        this.snackBar.open('Added to cart', 'Close', {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'bottom',
        });
      });
  }
}
