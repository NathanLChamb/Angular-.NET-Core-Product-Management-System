import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateOrderDto, ReadOrderDto } from './models';
import { HttpClient } from '@angular/common/http';
import { Environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = `${Environment.apiBaseUrl}/orders`;
  private http = inject(HttpClient);

  getMyOrders(): Observable<ReadOrderDto[]> {
    return this.http.get<ReadOrderDto[]>(this.apiUrl);
  }

  getOrderById(id: number): Observable<ReadOrderDto> {
    return this.http.get<ReadOrderDto>(`${this.apiUrl}/${id}`);
  }

  createOrder(dto: CreateOrderDto): Observable<ReadOrderDto> {
    return this.http.post<ReadOrderDto>(this.apiUrl, dto);
  }

  cancelOrder(id: number): Observable<ReadOrderDto> {
    return this.http.post<ReadOrderDto>(`${this.apiUrl}/${id}/cancel`, {});
  }

  getAllOrders(): Observable<ReadOrderDto[]> {
    return this.http.get<ReadOrderDto[]>( `${this.apiUrl}/admin`);
  }

  updateOrderStatus(id: number, status: number): Observable<ReadOrderDto> {
    return this.http.put<ReadOrderDto>(`${this.apiUrl}/${id}/status`, { status });
  }
}
