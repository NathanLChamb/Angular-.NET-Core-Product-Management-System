import { inject, Injectable } from '@angular/core';
import { Environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from '../../shared/models/paged-result';
import { CreateCategoryDto, ReadCategoryDto, UpdateCategoryDto } from './models';
import { PaginationParams } from '../../shared/models/pagination-params';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private apiUrl = `${Environment.apiBaseUrl}/category`
  private http = inject(HttpClient)

  public GetAllCategories(params: PaginationParams): Observable<PagedResult<ReadCategoryDto>> {
    return this.http.get<PagedResult<ReadCategoryDto>>(this.apiUrl, {
      params: {
        pageNumber: params.pageNumber,
        pageSize: params.pageSize
      }
    })
  }

  public GetCategoryById(id: number): Observable<ReadCategoryDto> {
    return this.http.get<ReadCategoryDto>(`${this.apiUrl}/${id}`)
  }

  public CreateCategory(dto: CreateCategoryDto): Observable<ReadCategoryDto> {
    return this.http.post<ReadCategoryDto>(this.apiUrl, dto)
  }

  public UpdateCategory(id: number, dto: UpdateCategoryDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto)
  }

  public DeleteCategory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
  }
}
