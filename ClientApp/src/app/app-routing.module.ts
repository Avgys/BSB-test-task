import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CatalogComponent } from './catalog/catalog.component';
import { CategoriesComponent } from './categories/categories.component';
import { CounterComponent } from './counter/counter.component';

// import { DashboardComponent } from './dashboard/dashboard.component';
// import { HeroesComponent } from './heroes/heroes.component';
// import { HeroDetailComponent } from './hero-detail/hero-detail.component';

const routes: Routes = [
  { path: '', redirectTo: '/catalog', pathMatch: 'full' },
  { path: 'catalog', component: CatalogComponent },
  // { path: 'detail/:id', component: HeroDetailComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'counter', component: CounterComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
