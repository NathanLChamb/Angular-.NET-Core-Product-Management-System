import { Component, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { ProductService } from '../../product/product-service';
import { CartService } from '../../cart/cart-service';
import { RouterLink } from '@angular/router';
import { ProductFilterService } from '../../product/product-filter-service';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  private cartService = inject(CartService);
  private productService =inject(ProductService);
  private filterService = inject(ProductFilterService);

   protected products = rxResource({
    params: () => this.filterService.filter(),
    stream: ({ params }) => this.productService.GetAllProducts(params)
  }); 

  addToCart(productVariantId: number) {
    this.cartService.AddItem({
        productVariantId,
        quantity: 1
      })
      .subscribe();
  }

  protected previousPage() {
    this.filterService.filter.update(f => ({
      ...f,
      pageNumber: Math.max(1, f.pageNumber - 1)
    })) 
  }

  protected nextPage() {
    this.filterService.filter.update(f => ({
      ...f,
      pageNumber: f.pageNumber + 1
    }))
  }
}

