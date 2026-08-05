import { Component, inject } from '@angular/core';
import { OrderService } from '../order-service';
import { rxResource } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-admin-orders',
  imports: [],
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
    'Pending',
    'Processing',
    'Shipped',
    'Delivered',
    'Cancelled'
  ];

  updateStatus(id:number, status:string) {
    this.orderService.updateOrderStatus(id,status).subscribe(() => {
      this.orders.reload();
    });
  }
}
