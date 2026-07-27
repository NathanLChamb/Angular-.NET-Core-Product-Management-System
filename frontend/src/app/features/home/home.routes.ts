import { Routes } from "@angular/router";
import { Home } from "./home/home";

export const HomeRoutes: Routes = [
    {path: '', component: Home, pathMatch: 'full'}
]