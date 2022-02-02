import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { CatalogComponent } from './catalog/catalog.component';
import { CategoriesComponent } from './categories/categories.component';
import { CounterComponent } from './counter/counter.component';

const routes: Routes = [
  { path: '', redirectTo: '/catalog', pathMatch: 'full' },
  { path: 'catalog', component: CatalogComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'counter', component: CounterComponent },
  { path: 'account', component: AccountComponent}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
