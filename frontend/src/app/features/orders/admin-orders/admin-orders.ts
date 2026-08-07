import { Component, inject } from '@angular/core';
import { OrderService } from '../order-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { OrderStatus } from '../models';

@Component({
  selector: 'app-admin-orders',
  imports: [RouterLink],
  templateUrl: './admin-orders.html',
  styleUrl: './admin-orders.css',
})
export class AdminOrders {
  private orderService = inject(OrderService);

  orders = rxResource({
    stream: () =>
      this.orderService.getAllOrders()
  });

  statuses = [
    { value: OrderStatus.Pending, label: 'Pending' },
    { value: OrderStatus.Processing, label: 'Processing' },
    { value: OrderStatus.Shipped, label: 'Shipped' },
    { value: OrderStatus.Delivered, label: 'Delivered' },
    { value: OrderStatus.Cancelled, label: 'Cancelled' }
  ];

  updateStatus(id: number, status: number) {
  this.orderService.updateOrderStatus(id, status).subscribe(() => {
    this.orders.reload();
  });
}
}
