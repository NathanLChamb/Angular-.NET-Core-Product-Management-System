import { Component, computed, inject, input, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../product-service';
import { ProductOptionValueImageDto, ReadProductDto } from '../models';

@Component({
  selector: 'app-product-images',
  imports: [ReactiveFormsModule],
  templateUrl: './product-images.html',
  styleUrl: './product-images.css',
})
export class ProductImages {
  private productService = inject(ProductService);
  private fb = inject(FormBuilder);

  product = input.required<ReadProductDto>();
  imagesChanged = output<void>();
  protected selectedOptionValues = signal<Map<number, number>>(new Map());

  protected imageForm = this.fb.nonNullable.group({
    url: ['', Validators.required],
    isDefault: [false]
  });

  protected images = computed(() =>
    [...(this.product().productOptionValueImages ?? [])]
      .sort((a, b) => a.displayOrder - b.displayOrder)
  );

  protected getOptionValues(optionId: number) {

    const values = this.product()
      .productVariants
      .flatMap(variant => variant.optionValues)
      .filter(value => value.optionId === optionId);

    return [...new Map(
      values.map(value => [value.id, value])
    ).values()];
  }

  protected isSelected(optionId: number, valueId: number): boolean {
    return this.selectedOptionValues().get(optionId) === valueId;
  }

  protected selectOptionValue(optionId: number, valueId: number) {
    this.selectedOptionValues.update(current => {
      const updated = new Map(current);
      updated.set(optionId, valueId);
      return updated;
    });
  }

  protected getSelectedOptionValueIds(): number[] {
    return [...this.selectedOptionValues().values()];
  }

  protected addImage() {
    if (this.imageForm.invalid) {
      return;
    }

    const optionValueIds = this.getSelectedOptionValueIds();

    if (optionValueIds.length === 0) {
      return;
    }

    const dto = {
      url: this.imageForm.controls.url.value,
      isDefault: this.imageForm.controls.isDefault.value,
      optionValueIds
    };

    this.productService
      .AddProductOptionValueImage(this.product().id, dto)
      .subscribe({
        next: () => {
          this.imageForm.reset({
            url: '',
            isDefault: false
          });

          this.selectedOptionValues.set(new Map());

          this.imagesChanged.emit();
        }
      });
  }

  protected deleteImage(imageId: number) {

    this.productService
      .DeleteProductOptionValueImage(
        this.product().id,
        imageId
      )
      .subscribe({
        next: () => {
          this.imagesChanged.emit();
        }
      });
  }

  protected getImageOptionValues(image: ProductOptionValueImageDto) {
    return image.optionValueIds.map(id => {
      for (const variant of this.product().productVariants) {
        const value = variant.optionValues.find(
          optionValue => optionValue.id === id
        );
        if (value) {
          return value;
        }
      }

      return null;
    }).filter(value => value !== null);
  }

}
