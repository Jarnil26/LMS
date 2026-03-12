import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AccessDeniedComponent } from './access-denied.component';

@NgModule({
  declarations: [AccessDeniedComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{ path: '', component: AccessDeniedComponent }])
  ]
})
export class AccessDeniedModule { }
