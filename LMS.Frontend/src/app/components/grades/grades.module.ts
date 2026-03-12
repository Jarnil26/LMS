import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { GradesComponent } from './grades.component';

@NgModule({
  declarations: [
    GradesComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: GradesComponent }
    ])
  ]
})
export class GradesModule { }
