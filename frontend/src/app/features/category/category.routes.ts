import { Routes } from "@angular/router";
import { CategoryList } from "./category-list/category-list";
import { CategoryForm } from "./category-form/category-form";
import { authGuard } from "../../core/guards/auth-guard";
import { adminGuard } from "../../core/guards/admin-guard";

export const CategoryRoutes: Routes = [
    { path: '', component: CategoryList, pathMatch: 'full' },
    { path: 'create', component: CategoryForm, canActivate: [authGuard, adminGuard] },
    { path: 'edit/:id', component: CategoryForm, canActivate: [authGuard, adminGuard] }
]