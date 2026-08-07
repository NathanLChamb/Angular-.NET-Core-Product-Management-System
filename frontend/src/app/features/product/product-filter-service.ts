import { Injectable, signal } from '@angular/core';
import { ProductSearchFilter, ProductSort } from './models';

@Injectable({
  providedIn: 'root',
})
export class ProductFilterService {
  filter = signal<ProductSearchFilter>({
    search: '',
    categoryIds: [],
    optionIds: [],
    sort: ProductSort.Default,
    pageNumber: 1,
    pageSize: 12
  });

  updateSearch(search: string) {
    this.filter.update(f => ({
      ...f,
      search,
      pageNumber: 1
    }));
  }

  updateSort(sort: ProductSort) {
    this.filter.update(f => ({
      ...f,
      sort,
      pageNumber: 1
    }));
  }

  reset() {
    this.filter.set({
      search: '',
      categoryIds: [],
      optionIds: [],
      sort: ProductSort.Default,
      pageNumber: 1,
      pageSize: 12
    });
  }
}
