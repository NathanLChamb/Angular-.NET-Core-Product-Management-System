import { Routes } from "@angular/router";
import { Register } from "./register/register";

export const RegisterRoutes: Routes = [
    {path: '', component: Register, pathMatch: 'full'}
]