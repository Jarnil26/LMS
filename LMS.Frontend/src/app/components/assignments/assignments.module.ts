import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AssignmentsComponent } from './assignments.component';

@NgModule({
  declarations: [
    AssignmentsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: AssignmentsComponent }
    ])
  ]
})
export class AssignmentsModule { }
