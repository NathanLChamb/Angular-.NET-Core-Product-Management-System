import { Component, computed, effect, inject, input, signal } from '@angular/core';
import { ProductService } from '../product-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { ProductOptionValueImageDto, ProductSearchFilter, ProductSort, ReadProductVariantDto } from '../models';
import { EMPTY } from 'rxjs';
import { CartService } from '../../cart/cart-service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-details',
  imports: [],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails {
  private productService = inject(ProductService);
  private cartService = inject(CartService);
  private snackBar = inject(MatSnackBar);

  protected id = input<string | undefined>();

  protected ProductSort = ProductSort;

  protected selectedOptions = signal<Map<number, number>>(new Map());

  protected filter = signal<ProductSearchFilter>({
    search: '',
    categoryIds: [],
    optionIds: [],
    sort: ProductSort.Default,
    pageNumber: 1,
    pageSize: 5
  });

  protected isSelected(
    optionId: number,
    valueId: number
  ): boolean {
    return this.selectedOptions().get(optionId) === valueId;
  }

  protected selectOption(
    optionId: number,
    valueId: number
  ) {
    this.selectedOptions.update(current => {
      const updated = new Map(current);
      updated.set(optionId, valueId);
      return updated;
    });
  }

  protected selectedVariant = computed(() => {
    const product = this.product.value();

    if (!product) {
      return null;
    }

    const selected = this.selectedOptions();

    return product.productVariants.find(variant => {

      if (variant.optionValues.length !== selected.size) {
        return false;
      }

      return variant.optionValues.every(optionValue =>
        selected.get(optionValue.optionId) === optionValue.id
      );

    }) ?? null;
  });
  protected selectedOptionValueImages = computed(() => {

    const variant = this.selectedVariant();

    if (!variant) {
      return [];
    }

    const variantOptionValueIds =
      variant.optionValues.map(v => v.id);

    return (
      this.product.value()?.productOptionValueImages
        .filter(image =>
          image.optionValueIds.every(id =>
            variantOptionValueIds.includes(id)
          )
        )
        .sort(
          (a, b) =>
            a.displayOrder - b.displayOrder
        ) ?? []
    );
  });

  protected getOptionValues(optionId: number) {

    const product = this.product.value();

    if (!product) {
      return [];
    }

    const values = product.productVariants
      .flatMap(v => v.optionValues)
      .filter(v => v.optionId === optionId);

    return [
      ...new Map(
        values.map(v => [v.id, v])
      ).values()
    ];
  }

  protected getValueImage(
    optionId: number,
    valueId: number
  ) {

    const product = this.product.value();

    if (!product) {
      return undefined;
    }

    return product.productOptionValueImages
      .find(image =>
        image.optionValueIds.length === 1 &&
        image.optionValueIds.includes(valueId)
      )
      ?.url;
  }

  protected product = rxResource({
    params: () => {
      const id = this.id();

      return id
        ? { id: Number(id) }
        : null;
    },

    stream: ({ params }) => {

      if (!params) {
        return EMPTY;
      }

      return this.productService
        .GetProductById(params.id);
    }
  });

  constructor() {

    effect(() => {

      const product = this.product.value();

      if (
        !product ||
        this.selectedOptions().size > 0
      ) {
        return;
      }

      const firstVariant =
        product.productVariants[0];

      if (!firstVariant) {
        return;
      }

      const selections =
        new Map<number, number>();

      for (
        const optionValue
        of firstVariant.optionValues
      ) {
        selections.set(
          optionValue.optionId,
          optionValue.id
        );
      }

      this.selectedOptions.set(selections);
    });
  }

  protected addToCart(
    productVariantId: number
  ) {

    this.cartService.AddItem({
      productVariantId,
      quantity: 1,
    }).subscribe(() => {

      this.snackBar.open(
        'Added to cart',
        'Close',
        {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'bottom',
        }
      );

    });
  }
}
