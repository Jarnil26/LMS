import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { GradingComponent } from './components/grading/grading.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule) },
  { path: 'register', loadChildren: () => import('./components/register/register.module').then(m => m.RegisterModule) },
  { 
    path: 'dashboard', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./components/dashboard/dashboard.module').then(m => m.DashboardModule) 
  },
  { 
    path: 'courses', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./components/courses/courses.module').then(m => m.CoursesModule) 
  },
  { 
    path: 'assignments', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./components/assignments/assignments.module').then(m => m.AssignmentsModule) 
  },
  {
    path: 'grading/:id',
    canActivate: [AuthGuard],
    component: GradingComponent
  },
  { 
    path: 'submissions', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./components/submissions/submissions.module').then(m => m.SubmissionsModule) 
  },
  { 
    path: 'grades', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./components/grades/grades.module').then(m => m.GradesModule) 
  },
  { 
    path: 'profile', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./components/profile/profile.module').then(m => m.ProfileModule) 
  },
  { path: 'access-denied', loadChildren: () => import('./components/access-denied/access-denied.module').then(m => m.AccessDeniedModule) },
  { path: '**', redirectTo: '/dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
