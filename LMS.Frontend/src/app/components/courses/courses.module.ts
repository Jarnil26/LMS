import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { CoursesComponent } from './courses.component';

@NgModule({
  declarations: [
    CoursesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: CoursesComponent }
    ])
  ]
})
export class CoursesModule { }
