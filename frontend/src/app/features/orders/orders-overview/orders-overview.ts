import { Component, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { OrderService } from '../order-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-orders-overview',
  imports: [RouterLink],
  templateUrl: './orders-overview.html',
  styleUrl: './orders-overview.css',
})
export class OrdersOverview {
  private orderService = inject(OrderService);

  orders = rxResource({
    stream: () =>
      this.orderService.getMyOrders()
  });
}
