import { Routes } from "@angular/router";
import { ProductList } from "./product-list/product-list";
import { ProductForm } from "./product-form/product-form";
import { authGuard } from "../../core/guards/auth-guard";
import { adminGuard } from "../../core/guards/admin-guard";
import { ProductDetails } from "./product-details/product-details";

export const ProductRoutes: Routes = [
    {path: '', component: ProductList, pathMatch: 'full'},
    { path: ':id', component: ProductDetails },
    {path: 'create', component: ProductForm, canActivate: [authGuard, adminGuard]},
    {path: 'edit/:id', component: ProductForm, canActivate: [authGuard, adminGuard]}
]