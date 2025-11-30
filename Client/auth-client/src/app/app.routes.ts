import { Routes } from '@angular/router';

export const routes: Routes = [
	{
		path: 'auth',
		loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule),
	},
	{
		path: 'product',
		loadChildren: () => import('./product/product.module').then(m => m.ProductModule),
	},
];
