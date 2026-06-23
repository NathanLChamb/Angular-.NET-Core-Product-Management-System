import { Routes } from '@angular/router';

export const routes: Routes = [
    {path: '', loadChildren: () =>
        import('./features/category/category.routes').then((m) => m.CategoryRoutes)
    },
    {path: 'category', loadChildren: () =>
        import('./features/category/category.routes').then((m) => m.CategoryRoutes)
    },
    {path: 'option', loadChildren: () =>
        import('./features/option/option.routes').then((m) => m.OptionRoutes)
    },
    {path: 'product', loadChildren: () =>
        import('./features/product/product.routes').then((m) => m.ProductRoutes)
    }
];
