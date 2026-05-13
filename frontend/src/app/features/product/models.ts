import { ReadCategoryDto } from "../category/models";
import { ReadOptionValueDto } from "../option/models";

export interface ReadProductDto {
    id: number;
    name: string;
    description: string;
    createdAt: string;
    updatedAt: string;
    categories: ReadCategoryDto[];
    options: ReadOptionFromProductDto[];
    productVariants: ReadProductVariantDto[]
}

export interface ReadOptionFromProductDto {
    id: number,
    name: string
}
export interface ReadProductVariantDto {
    id: number;
    sku: string;
    price: number;
    stockQuantity: number;
    createdAt: string;
    updatedAt: string;
    optionValues: ReadOptionValueDto[]
}

export interface CreateProductDto {
    name: string;
    description: string;
    categoryIds: number[];
    optionIds: number[];
    productVariants: CreateProductVariantDto[];
}

export interface CreateProductVariantDto {
    sku: string;
    price: number;
    stockQuantity: number;
    optionValueIds: number[];
}

export interface UpdateProductDto {
    name: string;
    description: string;
    categoryIds: number[];
    optionIds: number[];
    productVariants: UpdateProductVariantDto[]
}

export interface UpdateProductVariantDto {
    id?: number;
    sku: string;
    price: number;
    stockQuantity: number;
    optionValueIds: number[];
}