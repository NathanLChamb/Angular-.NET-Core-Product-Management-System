import { Component, computed, inject, input } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../product-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { EMPTY } from 'rxjs';

@Component({
  selector: 'app-product-images',
  imports: [ReactiveFormsModule],
  templateUrl: './product-images.html',
  styleUrl: './product-images.css',
})
export class ProductImages {
   private productService = inject(ProductService);
  private fb = inject(FormBuilder);

  productId = input.required<number>();

  protected imageForm = this.fb.group({
    url: ['', Validators.required],
    displayOrder: [0, Validators.required]
  });

  protected product = rxResource({
    params: () => {
      const id = this.productId();

      return id ? { id } : null;
    },

    stream: ({ params }) => {
      if (!params) {
        return EMPTY;
      }

      return this.productService.GetProductById(params.id);
    }
  });


  protected sortedImages = computed(() => {

    const product = this.product.value();

    if (!product) {
      return [];
    }

    return [...product.productImages].sort(
      (a, b) => a.displayOrder - b.displayOrder
    );

  });


  protected addImage() {

    if (this.imageForm.invalid) {
      return;
    }

    const productId = this.productId();

    const dto = this.imageForm.getRawValue();

    this.productService
      .AddProductImage(productId, {
        url: dto.url!,
        displayOrder: dto.displayOrder ?? 0
      })
      .subscribe(() => {

        this.imageForm.reset({
          url: '',
          displayOrder: 0
        });

        this.product.reload();

      });

  }


  protected deleteImage(imageId: number) {

    const productId = this.productId();

    this.productService
      .DeleteProductImage(productId, imageId)
      .subscribe(() => {

        this.product.reload();

      });

  }
}
