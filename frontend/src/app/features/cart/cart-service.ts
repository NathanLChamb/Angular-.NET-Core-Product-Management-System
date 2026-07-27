import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { AddCartItemDto, ReadCartDto, UpdateCartItemDto } from './models';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = `${Environment.apiBaseUrl}/cart`
  private http = inject(HttpClient)

  public GetCart(): Observable<ReadCartDto> {
    return this.http.get<ReadCartDto>(this.apiUrl);
  }

  public AddItem(dto: AddCartItemDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/items`, dto);
  }

  public UpdateItemQuantity(productVariantId: number, dto: UpdateCartItemDto): Observable<ReadCartDto> {
    return this.http.put<ReadCartDto>(`${this.apiUrl}/items/${productVariantId}`, dto);
  }

  public RemoveItem(productVariantId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/items/${productVariantId}`);
  }

  public ClearCart(): Observable<void> {
    return this.http.delete<void>(this.apiUrl);
  }
}
