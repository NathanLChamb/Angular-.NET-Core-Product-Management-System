import { Routes } from "@angular/router";
import { ProductList } from "./product-list/product-list";
import { ProductForm } from "./product-form/product-form";

export const ProductRoutes: Routes = [
    {path: '', component: ProductList, pathMatch: 'full'},
    {path: 'create', component: ProductForm},
    {path: 'edit/:id', component: ProductForm}
]