import { inject, Injectable } from '@angular/core';
import { Environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PaginationParams } from '../../shared/models/pagination-params';
import { Observable } from 'rxjs';
import { PagedResult } from '../../shared/models/paged-result';
import { CreateOptionDto, ReadOptionDto, UpdateOptionDto } from './models';

@Injectable({
  providedIn: 'root',
})
export class OptionService {
  private apiUrl = `${Environment.apiBaseUrl}/option`
  private http = inject(HttpClient)

  public GetAllOptions(params: PaginationParams): Observable<PagedResult<ReadOptionDto>> {
    return this.http.get<PagedResult<ReadOptionDto>>(this.apiUrl, {
      params: {
        pageNumber: params.pageNumber,
        pageSize: params.pageSize
      }
    })
  }

  public GetOptionById(id: number): Observable<ReadOptionDto> {
    return this.http.get<ReadOptionDto>(`${this.apiUrl}/${id}`)
  }

  public CreateOption(dto: CreateOptionDto): Observable<ReadOptionDto> {
    return this.http.post<ReadOptionDto>(this.apiUrl, dto)
  }

  public UpdateOption(id: number, dto: UpdateOptionDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto)
  }

  public DeleteOption(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
  }
}
