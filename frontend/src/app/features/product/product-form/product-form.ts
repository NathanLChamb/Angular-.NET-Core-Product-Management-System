import { Component, computed, effect, inject, input } from '@angular/core';
import { ProductService } from '../product-service';
import { CategoryService } from '../../category/category-service';
import { OptionService } from '../../option/option-service';
import { Router } from '@angular/router';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { rxResource, toSignal } from '@angular/core/rxjs-interop';
import { catchError, EMPTY, finalize, startWith, tap } from 'rxjs';
import { ReadProductDto } from '../models';

type ProductFormGroup = FormGroup<{
  name: FormControl<string>;
  description: FormControl<string>;
  categoryIds: FormControl<number[]>;
  optionIds: FormControl<number[]>;
  productVariants: FormArray<ProductVariantForm>;
}>
type ProductVariantForm = FormGroup<{
  id: FormControl<number | null>;
  sku: FormControl<string>;
  price: FormControl<number>;
  stockQuantity: FormControl<number>;
  optionValueIds: FormControl<number[]>;
}>
type VariantCombination = {
  optionValueIds: number[]
}
@Component({
  selector: 'app-product-form',
  imports: [ReactiveFormsModule],
  templateUrl: './product-form.html',
  styleUrl: './product-form.css',
})
export class ProductForm {
  private productService = inject(ProductService)
  private categoryService = inject(CategoryService)
  private optionService = inject(OptionService)
  private router = inject(Router)
  private fb = inject(FormBuilder)
  protected id = input<string | undefined>()
  protected isEditMode = computed(() => !!this.id())
  private isSeeding = false
  get name() { return this.form.controls.name }
  get description() { return this.form.controls.description }
  get optionIds() { return this.form.controls.optionIds}
  get productVariants() { return this.form.controls.productVariants }
  
  protected form: ProductFormGroup = this.fb.nonNullable.group({
    name: this.fb.nonNullable.control(''),
    description: this.fb.nonNullable.control(''),
    categoryIds: this.fb.nonNullable.control<number[]>([]),
    optionIds: this.fb.nonNullable.control<number[]>([]),
    productVariants: this.fb.array<ProductVariantForm>([])
  })

  protected product = rxResource({
    params: () => {
      const id = this.id()
      return id ? { id: Number(id) } : null
    },
    stream: ({params}) => {
      if (!params) return EMPTY
      this.form.disable()

      return this.productService.GetProductById(params.id).pipe(
        tap(product => {
          this.isSeeding = true
          try { 
            this.seedForm(product) 
          } finally { 
            this.isSeeding = false 
          }
        }),
        catchError(error => {
          if (error.status === 404) {
            this.router.navigate(['/product'])
          }
          return EMPTY
        }),
        finalize(() => this.form.enable())
      )
    }
  })

  protected categories = rxResource({
    params: () => ({
      pageNumber: 1,
      pageSize: 100
    }),
    stream: ({params}) => this.categoryService.GetAllCategories(params)
  })

  protected options = rxResource({
    params: () => ({
      pageNumber: 1,
      pageSize: 100
    }),
    stream: ({params}) => this.optionService.GetAllOptions(params)
  })

  protected seedForm(product: ReadProductDto) {
    this.form.patchValue({
      name: product.name,
      description: product.description,
      categoryIds: product.categories.map(c => c.id),
      optionIds: product.options.map(o => o.id)
    })

    this.productVariants.clear()
    product.productVariants.forEach(variant => {
      this.productVariants.push(this.fb.group({
        id: this.fb.control<number | null>(variant.id),
        sku: this.fb.nonNullable.control(variant.sku),
        price: this.fb.nonNullable.control(variant.price),
        stockQuantity: this.fb.nonNullable.control(variant.stockQuantity),
        optionValueIds: this.fb.nonNullable.control(variant.optionValues.map(ov => ov.id))
      }))
    })
  }

  protected selectedOptionIds = toSignal(
    this.form.controls.optionIds.valueChanges.pipe(
      startWith(this.optionIds.value)
    ), {initialValue: this.optionIds.value}
  )

  protected selectedOptions = computed(() => {
    const optionIds = this.selectedOptionIds()
    const allOptions = this.options.value()?.items ?? []

    return allOptions.filter(existing => optionIds.includes(existing.id))
  })

  cartesianProduct<T>(arrays: T[][]): T[][] {
    return arrays.reduce<T[][]>(
      (acc, curr) => acc.flatMap(a =>
        curr.map(c => [...a, c])
      ), [[]]
    )
  }

  protected variants = computed(() => {
    const selectedOptions = this.selectedOptions()

    const valueGroups = selectedOptions.map(
      option => option.optionValues
    )

    const combinations = this.cartesianProduct(valueGroups)

    return combinations.map(combination => ({
      optionValueIds: combination.map(v => v.id)
    }))
  })

  private syncVariantForm(variants: VariantCombination[]) {
    const existingMap = new Map(
      this.productVariants.controls.map(control => [
        this.variantKey({
          optionValueIds: control.controls.optionValueIds.value
        }),
        control
      ])
    )

    const nextControls: ProductVariantForm[] = []

    for (const variant of variants) {
      const key = this.variantKey(variant)

      const existing = existingMap.get(key)

      if (existing) {
        nextControls.push(existing)
      } else {
        nextControls.push(
          this.fb.group({
            id: this.fb.control<number | null>(null),
            sku: this.fb.nonNullable.control(''),
            price: this.fb.nonNullable.control(0),
            stockQuantity: this.fb.nonNullable.control(0),
            optionValueIds: this.fb.nonNullable.control(
              variant.optionValueIds
            )
          })
        )
      }
    }

    this.form.setControl(
      'productVariants',
      this.fb.array(nextControls)
    )
  }

  constructor() {
    effect(() => {
      if (this.isSeeding) return
      const options = this.options.value()

      if (!options) return

      const selectedOptionIds =
        this.form.controls.optionIds.value

      if (
        selectedOptionIds.length > 0 &&
        this.variants().length === 0
      ) {
        return
      }

      const variants = this.variants()

      const currentKeys = this.productVariants.controls.map(control =>
        this.variantKey({
          optionValueIds:
            control.controls.optionValueIds.value
        })
      )

      const newKeys = variants.map(v =>
        this.variantKey(v)
      )

      const changed =
        currentKeys.length !== newKeys.length ||
        currentKeys.some((k, i) => k !== newKeys[i])

      if (changed) {
        this.syncVariantForm(variants)
      }
    })
  }

  private variantKey(v: VariantCombination) {
    return [...v.optionValueIds].sort().join('-')
  }

  toggleCategory(id: number) {
    const current = this.form.controls.categoryIds.value

    const next = current.includes(id)
      ? current.filter(x => x !== id)
      : [...current, id]

    this.form.controls.categoryIds.setValue(next)
  }

  toggleOption(id: number) {
    const current = this.form.controls.optionIds.value

    const next = current.includes(id)
      ? current.filter(x => x !== id)
      : [...current, id]

    this.form.controls.optionIds.setValue(next)
  }

  optionValueMap = computed(() => {
    const map = new Map<number, string>()

    for (const opt of this.selectedOptions()) {
      for (const val of opt.optionValues) {
        map.set(val.id, `${opt.name} - ${val.value}`)
      }
    }

    return map
  })

  protected onSubmit() {
    if (this.form.invalid) return

    const raw = this.form.getRawValue()
    const dto = {
      name: raw.name.trim(),
      description: raw.description.trim(),
      categoryIds: raw.categoryIds,
      optionIds: raw.optionIds,
      productVariants: raw.productVariants
        .filter(v => v.sku && v.sku.trim() !== '')
        .map(v => ({
          id: v.id ?? undefined,
          sku: v.sku.trim(),
          price: Number(v.price),
          stockQuantity: Number(v.stockQuantity),
          optionValueIds: v.optionValueIds
        }))
    }

    if (this.isEditMode()) {
      this.productService.UpdateProduct(Number(this.id()), dto)
        .subscribe({
          next: () => this.router.navigate(['/product'])
        })
    } else {
      this.productService.CreateProduct(dto)
        .subscribe({
          next: () => this.router.navigate(['/product'])
        })
    }
  }
}
