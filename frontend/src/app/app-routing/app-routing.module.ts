import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexComponent } from '../index/index.component';
import { CategoryComponent } from '../category/category.component';
import { CategoryDetailsComponent } from '../category-details/category-details.component';
import { UserComponent } from '../user/user.component';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { VendorComponent } from '../vendor/vendor.component';
import { VendorDetailsComponent } from '../vendor-details/vendor-details.component';

const routes: Routes = [
    { path: '', redirectTo: '/index', pathMatch: 'full' },
    { path: 'index', component: IndexComponent },
    { path: 'category', component: CategoryComponent },
    { path: 'category/:id', component: CategoryDetailsComponent },
    { path: 'user', component: UserComponent },
    { path: 'user/:id', component: UserDetailsComponent },
    { path: 'vendor', component: VendorComponent },
    { path: 'vendor/:id', component: VendorDetailsComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }