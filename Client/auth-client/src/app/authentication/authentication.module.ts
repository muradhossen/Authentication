import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
 
import { Authentication } from './authentication';
import { Login } from './login/login';
import { Registration } from './registration/registration';

import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: Authentication,
    children: [
      { path: '', component: Login }, 
      { path: 'login', component: Login },
      { path: 'registration', component: Registration },
    ],
  },
];

@NgModule({
  declarations: [Login, Registration],
  imports: [CommonModule, ReactiveFormsModule, RouterModule.forChild(routes), Authentication],
  exports: [Authentication],
})
export class AuthenticationModule {}
