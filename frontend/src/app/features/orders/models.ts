export interface ReadOrderDto {
    id: number;
    orderNumber: string;
    totalPrice: number;
    shippingAddress: string;
    status: string;
    orderDate: string;
    items: ReadOrderItemDto[];
}

export interface ReadOrderItemDto {
    id: number;
    productName: string;
    sku: string;
    priceAtPurchase: number;
    quantity: number;
    totalPrice: number;
}

export interface CreateOrderDto {
    shippingAddress: string;
}

export enum OrderStatus {
  Pending = 0,
  Processing = 1,
  Shipped = 2,
  Delivered = 3,
  Cancelled = 4
}