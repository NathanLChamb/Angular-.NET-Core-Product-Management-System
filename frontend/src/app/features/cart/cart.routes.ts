import { Routes } from "@angular/router";
import { CartOverview } from "./cart-overview/cart-overview";

export const CartRoutes: Routes = [
    {path: '', component: CartOverview, pathMatch: 'full'}
]