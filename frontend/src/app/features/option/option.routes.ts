import { Routes } from "@angular/router";
import { OptionList } from "./option-list/option-list";
import { OptionForm } from "./option-form/option-form";
import { authGuard } from "../../core/guards/auth-guard";
import { adminGuard } from "../../core/guards/admin-guard";

export const OptionRoutes: Routes = [
    {path: '', component: OptionList, pathMatch: 'full'},
    {path: 'create', component: OptionForm, canActivate: [authGuard, adminGuard]},
    {path: 'edit/:id', component: OptionForm, canActivate: [authGuard, adminGuard]}
]