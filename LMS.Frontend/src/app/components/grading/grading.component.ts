import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssignmentService } from '../../services/assignment.service';
import { Submission, GradeSubmissionRequest } from '../../models/assignment.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-grading',
  templateUrl: './grading.component.html',
  styleUrls: ['./grading.component.css']
})
export class GradingComponent implements OnInit {
  assignmentId: number = 0;
  submissions: Submission[] = [];
  selectedSubmission: Submission | null = null;
  loading = true;
  error = '';
  gradeForm: FormGroup;
  gradingMode = false;

  constructor(
    private route: ActivatedRoute,
    private assignmentService: AssignmentService,
    private fb: FormBuilder
  ) {
    this.gradeForm = this.fb.group({
      grade: ['', [Validators.required, Validators.min(0), Validators.max(100)]],
      feedback: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.assignmentId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadSubmissions();
  }

  loadSubmissions(): void {
    this.loading = true;
    this.assignmentService.getAssignmentSubmissions(this.assignmentId).subscribe({
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

  selectSubmission(submission: Submission): void {
    this.selectedSubmission = submission;
    this.gradingMode = true;
    this.gradeForm.patchValue({
      grade: submission.grade || '',
      feedback: submission.feedback || ''
    });
  }

  cancelGrading(): void {
    this.selectedSubmission = null;
    this.gradingMode = false;
  }

  onSubmitGrade(): void {
    if (this.gradeForm.valid && this.selectedSubmission) {
      this.loading = true;
      const request: GradeSubmissionRequest = this.gradeForm.value;
      this.assignmentService.gradeSubmission(this.assignmentId, this.selectedSubmission.id, request).subscribe({
        next: (response) => {
          if (response.success) {
            this.loadSubmissions();
            this.cancelGrading();
          } else {
            this.error = response.message || 'Failed to submit grade';
          }
          this.loading = false;
        },
        error: (err) => {
          this.error = 'An error occurred while grading';
          this.loading = false;
        }
      });
    }
  }
}
