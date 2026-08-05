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