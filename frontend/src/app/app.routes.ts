import { Routes } from '@angular/router';

export const routes: Routes = [
    {path: '', loadChildren: () =>
        import('./features/category/category.routes').then((m) => m.CategoryRoutes)
    },
    {path: 'register', loadChildren: () =>
        import('./features/auth/register.routes').then((m) => m.RegisterRoutes)
    },
    {path: 'login', loadChildren: () =>
        import('./features/auth/login.routes').then((m) => m.LoginRoutes)
    },
    {path: 'category', loadChildren: () =>
        import('./features/category/category.routes').then((m) => m.CategoryRoutes)
    },
    {path: 'option', loadChildren: () =>
        import('./features/option/option.routes').then((m) => m.OptionRoutes)
    },
    {path: 'product', loadChildren: () =>
        import('./features/product/product.routes').then((m) => m.ProductRoutes)
    },
    {path: '**', redirectTo: 'login'}
];
