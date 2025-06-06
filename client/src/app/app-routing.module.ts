import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Trang chủ' } },
  { path: 'Cửa Hàng', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule) },
  { path: 'Giỏ Hàng', loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule) },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
