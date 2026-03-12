import { Component, OnInit } from '@angular/core';
import { AssignmentService } from '../../services/assignment.service';
import { AuthService } from '../../services/auth.service';
import { Submission } from '../../models/assignment.model';

@Component({
  selector: 'app-grades',
  templateUrl: './grades.component.html',
  styleUrls: ['./grades.component.css']
})
export class GradesComponent implements OnInit {
  gradedSubmissions: Submission[] = [];
  loading = true;
  error = '';
  gpa = 0;

  constructor(
    private assignmentService: AssignmentService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.loadGrades();
  }

  loadGrades(): void {
    this.loading = true;
    this.assignmentService.getMySubmissions().subscribe({
      next: (response) => {
        if (response.success) {
          this.gradedSubmissions = (response.data as Submission[]).filter(s => s.isGraded);
          this.calculateGPA();
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load grades';
        this.loading = false;
      }
    });
  }

  calculateGPA(): void {
    if (this.gradedSubmissions.length === 0) return;
    const total = this.gradedSubmissions.reduce((acc, curr) => acc + (curr.grade || 0), 0);
    this.gpa = total / this.gradedSubmissions.length;
  }
}
