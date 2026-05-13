import { Component, computed, inject, signal } from '@angular/core';
import { ProductService } from '../product-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-product-list',
  imports: [RouterLink, DatePipe],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList {
  private productService = inject(ProductService)
  protected pageNumber = signal(1)
  protected pageSize = signal(5)
  expandedProductIds = new Set<number>()

  protected products = rxResource({
    params: () => ({
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize()
    }),
    stream: ({params}) => this.productService.GetAllProducts(params)
  })

  protected totalPages = computed(() => {
    const data = this.products.value()
    if (!data) return 0

    return Math.ceil(data.totalCount / this.pageSize())
  })

  protected toggle(productId: number) {
    if (this.expandedProductIds.has(productId)) {
      this.expandedProductIds.delete(productId)
    } else {
      this.expandedProductIds.add(productId)
    }
  }

  protected isExpanded(productId: number) {
    return this.expandedProductIds.has(productId)
  }

  protected previousPage() {
    if (this.pageNumber() > 1) {
      this.pageNumber.update(p => p - 1)
    }
  }

  protected nextPage() {
    if (this.pageNumber() < this.totalPages()) {
      this.pageNumber.update(p => p + 1)
    }
  }

  handleDelete(id: number) {
    this.productService.DeleteProduct(id).subscribe({
      next: () => this.products.reload()
    })
  }
}
