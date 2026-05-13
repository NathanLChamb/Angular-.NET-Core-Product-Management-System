import { Routes } from "@angular/router";
import { CategoryList } from "./category-list/category-list";
import { CategoryForm } from "./category-form/category-form";

export const CategoryRoutes: Routes = [
    { path: '', component: CategoryList, pathMatch: 'full' },
    { path: 'create', component: CategoryForm },
    { path: 'edit/:id', component: CategoryForm }
]