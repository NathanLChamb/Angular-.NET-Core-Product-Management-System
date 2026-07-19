import { Routes } from "@angular/router";
import { Login } from "./login/login";

export const LoginRoutes: Routes = [
    {path: '', component: Login, pathMatch: 'full'},
]