import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventListComponent } from './event-list/event-list.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { AccountComponent } from './account/account.component';

const routes: Routes = [
  { path: '', redirectTo: '/events', pathMatch: 'full' },
  { path: 'events', component: EventListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'account', component: AccountComponent },
  { path: 'registration', component: RegistrationComponent },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
