export interface ReadCartDto {
  id: number;
  items: ReadCartItemDto[];
  totalPrice: number;
}

export interface ReadCartItemDto {
  id: number;
  productVariantId: number;
  productName: string;
  optionValues: ReadOptionValueFromCartDto[];
  unitPrice: number;
  quantity: number;
  totalPrice: number;
}

export interface ReadOptionValueFromCartDto {
  value: string;
}

export interface AddCartItemDto {
  productVariantId: number;
  quantity: number;
}

export interface UpdateCartItemDto {
  quantity: number;
}