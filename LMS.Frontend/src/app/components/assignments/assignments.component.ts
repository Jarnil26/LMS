import { Component, OnInit } from '@angular/core';
import { AssignmentService } from '../../services/assignment.service';
import { AuthService } from '../../services/auth.service';
import { Assignment } from '../../models/assignment.model';
import { User } from '../../models/auth.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { Course } from '../../models/course.model';

@Component({
  selector: 'app-assignments',
  templateUrl: './assignments.component.html',
  styleUrls: ['./assignments.component.css']
})
export class AssignmentsComponent implements OnInit {
  assignments: Assignment[] = [];
  currentUser: User | null = null;
  loading = true;
  error = '';
  showModal = false;
  assignmentForm: FormGroup;
  myCourses: Course[] = [];

  constructor(
    private assignmentService: AssignmentService,
    private authService: AuthService,
    private courseService: CourseService,
    private fb: FormBuilder
  ) { 
    this.assignmentForm = this.fb.group({
      courseId: ['', Validators.required],
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      dueDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUserValue();
    this.loadAssignments();
    if (this.currentUser?.role === 'Teacher') {
      this.loadMyCourses();
    }
  }

  loadMyCourses(): void {
    this.courseService.getTeacherCourses().subscribe({
      next: (response) => {
        if (response.success) {
          this.myCourses = response.data;
        }
      }
    });
  }

  loadAssignments(): void {
    this.loading = true;
    this.assignmentService.getMyAssignments().subscribe({
      next: (response) => {
        if (response.success) {
          this.assignments = response.data;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load assignments';
        this.loading = false;
      }
    });
  }

  toggleModal(): void {
    this.showModal = !this.showModal;
    if (!this.showModal) {
      this.assignmentForm.reset();
      this.error = '';
    }
  }

  onSubmit(): void {
    if (this.assignmentForm.valid) {
      this.loading = true;
      const { courseId, ...assignmentData } = this.assignmentForm.value;
      this.assignmentService.createAssignment(courseId, assignmentData).subscribe({
        next: (response) => {
          if (response.success) {
            this.toggleModal();
            this.loadAssignments();
          } else {
            this.error = response.message || 'Failed to create assignment';
            this.loading = false;
          }
        },
        error: (err) => {
          this.error = 'An error occurred while creating the assignment';
          this.loading = false;
        }
      });
    }
  }
}
