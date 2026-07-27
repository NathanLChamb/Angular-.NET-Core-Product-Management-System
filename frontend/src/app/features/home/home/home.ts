import { Component, inject, signal } from '@angular/core';
import { ProductSearchFilter, ProductSort } from '../../product/models';
import { rxResource } from '@angular/core/rxjs-interop';
import { ProductService } from '../../product/product-service';
import { CartService } from '../../cart/cart-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  private cartService = inject(CartService);
  private productService =inject(ProductService);

  protected filter = signal<ProductSearchFilter>({
    search: '',
    categoryIds: [],
    optionIds: [],
    sort: ProductSort.Default,
    pageNumber: 1,
    pageSize: 12
  });

  protected products = rxResource({
    params: () => this.filter(),
    stream: ({ params }) => this.productService.GetAllProducts(params)
  });

  addToCart(productVariantId: number) {
    this.cartService.AddItem({
        productVariantId,
        quantity: 1
      })
      .subscribe();
  }

  protected updateSearch(search: string) {
    this.filter.update(f => ({
      ...f,
      search,
      pageNumber: 1
    }));
  }

  protected updateSort(sort: ProductSort) {
    this.filter.update(f => ({
      ...f,
      sort,
      pageNumber: 1
    }));
  }

  protected previousPage() {
    this.filter.update(f => ({
      ...f,
      pageNumber: Math.max(1, f.pageNumber - 1)
    })) 
  }

  protected nextPage() {
    this.filter.update(f => ({
      ...f,
      pageNumber: f.pageNumber + 1
    }))
  }
}
