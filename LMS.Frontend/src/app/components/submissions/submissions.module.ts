import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { SubmissionsComponent } from './submissions.component';

@NgModule({
  declarations: [
    SubmissionsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: SubmissionsComponent }
    ])
  ]
})
export class SubmissionsModule { }
