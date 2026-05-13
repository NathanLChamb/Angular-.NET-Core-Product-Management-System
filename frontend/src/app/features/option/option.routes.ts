import { Routes } from "@angular/router";
import { OptionList } from "./option-list/option-list";
import { OptionForm } from "./option-form/option-form";

export const OptionRoutes: Routes = [
    {path: '', component: OptionList, pathMatch: 'full'},
    {path: 'create', component: OptionForm},
    {path: 'edit/:id', component: OptionForm}
]