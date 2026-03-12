import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { AssignmentService } from '../../services/assignment.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/auth.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  currentUser: User | null = null;
  userRole: string = '';
  stats = {
    totalCourses: 0,
    totalStudents: 0,
    pendingAssignments: 0,
    completedSubmissions: 0
  };
  upcomingAssignments: any[] = [];
  recentCourses: any[] = [];
  loading = true;

  constructor(
    private courseService: CourseService,
    private assignmentService: AssignmentService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUserValue();
    this.userRole = this.currentUser?.role || '';
    this.loadDashboardData();
  }

  loadDashboardData() {
    if (this.userRole === 'Teacher') {
      this.loadTeacherDashboard();
    } else if (this.userRole === 'Student') {
      this.loadStudentDashboard();
    } else {
      this.loadAdminDashboard();
    }
  }

  loadTeacherDashboard() {
    this.courseService.getTeacherCourses().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.recentCourses = response.data.slice(0, 3);
          this.stats.totalCourses = response.data.length;
        }
        this.loading = false;
      }
    });
  }

  loadStudentDashboard() {
    this.courseService.getStudentCourses().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.recentCourses = response.data.slice(0, 3);
          this.stats.totalCourses = response.data.length;
        }
        this.loading = false;
      }
    });
  }

  loadAdminDashboard() {
    this.courseService.getTeacherCourses().subscribe({ // Using teacher courses as a proxy for global or implement dedicated
      next: (response) => {
        if (response.success) {
          this.stats.totalCourses = response.data.length;
        }
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }
}
