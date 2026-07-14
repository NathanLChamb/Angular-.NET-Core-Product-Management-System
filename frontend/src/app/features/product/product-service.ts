import { inject, Injectable } from '@angular/core';
import { Environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from '../../shared/models/paged-result';
import { CreateProductDto, ProductSearchFilter, ReadProductDto, UpdateProductDto } from './models';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = `${Environment.apiBaseUrl}/product`
  private http = inject(HttpClient)

  public GetAllProducts(params: ProductSearchFilter): Observable<PagedResult<ReadProductDto>> {
    console.log('service received:', params);
    return this.http.get<PagedResult<ReadProductDto>>(this.apiUrl, {
      params: {
        search: params.search ?? '',
        CategoryIds: params.categoryIds,
        OptionIds: params.optionIds,
        Sort: params.sort,
        pageNumber: params.pageNumber,
        pageSize: params.pageSize
      }
    })
  }

  public GetProductById(id: number): Observable<ReadProductDto> {
    return this.http.get<ReadProductDto>(`${this.apiUrl}/${id}`)
  }

  public CreateProduct(dto: CreateProductDto): Observable<ReadProductDto> {
    return this.http.post<ReadProductDto>(this.apiUrl, dto)
  }

  public UpdateProduct(id: number, dto: UpdateProductDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto)
  }

  public DeleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
  }
}
