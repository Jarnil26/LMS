import { Component, OnInit } from '@angular/core';
import { AssignmentService } from '../../services/assignment.service';
import { AuthService } from '../../services/auth.service';
import { Submission } from '../../models/assignment.model';
import { User } from '../../models/auth.model';

@Component({
  selector: 'app-submissions',
  templateUrl: './submissions.component.html',
  styleUrls: ['./submissions.component.css']
})
export class SubmissionsComponent implements OnInit {
  submissions: Submission[] = [];
  currentUser: User | null = null;
  loading = true;
  error = '';

  constructor(
    private assignmentService: AssignmentService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUserValue();
    this.loadSubmissions();
  }

  loadSubmissions(): void {
    this.loading = true;
    this.assignmentService.getMySubmissions().subscribe({
      next: (response) => {
        if (response.success) {
          this.submissions = response.data;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load submissions';
        this.loading = false;
      }
    });
  }
}
